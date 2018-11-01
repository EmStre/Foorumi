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
using Foorumi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Foorumi.Controllers
{
    public class LoginController : ApiController
    {
        private FoorumiModel db = new FoorumiModel();


        // GET: api/Login/5
        [HttpPost]
        public IHttpActionResult Login([FromBody]dynamic value)
        {

            string usr = value.usr?.Value;
            string pwd = value.pwd?.Value;

            Kayttaja kayttaja = db.Kayttajat.Where(k => k.nimimerkki.ToLower() == usr.ToLower()).SingleOrDefault();

            if (kayttaja == null)
            {
                // Käyttäjänimellä ei löytynyt käyttäjää, palautetaan error
                return NotFound();
            }

            // Käyttäjä löytyi, tarkastetaan salasana
            if(!Kirjautuminen.ValidoiSalasana(pwd, kayttaja.hash))
            {
                // Salasana ei täsmää
                return NotFound();
            }

            kayttaja.Sessio = Kirjautuminen.GeneroiSessioAvain(); // Generoidaan uusi sessioavain
            db.SaveChanges(); // tallennetaan muutos

            string jwt = Kirjautuminen.GeneroiJwtString(kayttaja.Sessio);



            return Ok( new {
                jwt,
                kayttaja.kayttaja_id,
                kayttaja.kuvaus,
                kayttaja.nimimerkki,
                kayttaja.email,
                kayttaja.Kayttajatasot
            });
        }
        //// POST: api/Login
        //[ResponseType(typeof(Kayttaja))]
        //public IHttpActionResult PostKayttaja(Kayttaja kayttaja)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    //db.Kayttajat.Add(kayttaja);
        //    //db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = kayttaja.kayttaja_id }, kayttaja);
        //}

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