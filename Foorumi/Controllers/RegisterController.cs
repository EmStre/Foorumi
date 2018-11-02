using Foorumi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Foorumi.Controllers
{
    public class RegisterController : ApiController
    {
        private FoorumiModel db = new FoorumiModel();

        // POST: api/Register
        [ResponseType(typeof(Kayttaja))]
        public IHttpActionResult PostKayttaja([FromBody]dynamic value)
        {
            if ((Kirjautuminen.HaeKirjautuminen(Request.Headers.Authorization?.ToString().Substring(7) ?? null) as Kayttaja).OnKirjautunut)
                return BadRequest("Rekisteröityminen ei ole sallittua kirjautuneena!");

            string nimi, email, pwd;
            try
            {
                nimi = value.nimimerkki?.Value;
                email = value.email?.Value;
                pwd = value.pwd?.Value;
            }
            catch (Exception)
            {
                return BadRequest();
            }

            if (nimi == null | email == null | pwd == null)
            {
                // Jos virhe datassa, palautetaan bad request
                return BadRequest();
            }

            if (db.Kayttajat.Where(k => k.nimimerkki.ToLower() == nimi.ToLower()).Any())
            {
                // Käyttäjätunnus on jo olemassa
                return BadRequest("Käyttäjätunnus on jo olemassa");
            }

            // Lisää logiikkaa rekisteröitymisen tarkastamiseksi

            // Logiikka päätty

            Kayttaja kayttaja = new Kayttaja
            {
                nimimerkki = nimi,
                email = email,
                hash = Kirjautuminen.GeneroiHash(pwd),
                kayttajataso_id = Kirjautuminen.OletusKayttajataso,
                aktiivisuus = DateTime.Now
            };

            db.Kayttajat.Add(kayttaja);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kayttaja.kayttaja_id }, kayttaja);
            //return Ok(kayttaja);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KayttajaExists(int id)
        {
            return db.Kayttajat.Count(e => e.kayttaja_id == id) > 0;
        }
    }
}