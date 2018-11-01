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
    public class LangatController : ApiController
    {
        private FoorumiModel db = new FoorumiModel();

        // GET: api/Lanka
        public IHttpActionResult GetLanka(int id)
        {
            Kayttaja k = Kirjautuminen.HaeKirjautuminen(Request.Headers.Authorization?.ToString().Substring(7) ?? null);

            Lanka lanka = db.Langat.Find(id);

            if (lanka == null)
            {
                return NotFound();
            }

            if (!lanka.Alueet.NakeekoKayttaja(k))
            {
                return Unauthorized();
            }
            var data = new { lanka, viestit = lanka.Viestit };
            return Ok(new { k.jwt, data });
        }

        // PUT: api/Lanka/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLanka(int id, Lanka lanka)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lanka.lanka_id)
            {
                return BadRequest();
            }

            Kayttaja k = Kirjautuminen.HaeKirjautuminen(Request.Headers.Authorization?.ToString().Substring(7) ?? null);

            Lanka originalLanka = db.Langat.Find(lanka.lanka_id);

            if (originalLanka == null)
            {
                return NotFound();
            }

            if (originalLanka.Kayttajat.kayttaja_id == k.kayttaja_id)
            {
                // Tämä on oma lanka
                if (!k.Kayttajatasot.o_lankaOmaMuokkaa)
                {
                    return Unauthorized();
                }
            }
            else
            {
                // Tämä on muiden lanka
                if (!k.Kayttajatasot.o_lankaMuokkaa)
                {
                    return Unauthorized();
                }
            }

            originalLanka.otsikko = lanka.otsikko;
            originalLanka.alue_id = lanka.alue_id;

            if (originalLanka.otsikko.Length < 1)
            {
                return BadRequest("Otsikko on liian olematon");
            }
            if (originalLanka.otsikko.Length > 50)
            {
                return BadRequest("Otsikko on liian pitkä");
            }


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LankaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(k.jwt);
        }

        // POST: api/Lanka
        [ResponseType(typeof(Lanka))]
        public IHttpActionResult PostLanka(Lanka lanka)
        {
            Kayttaja k = Kirjautuminen.HaeKirjautuminen(Request.Headers.Authorization?.ToString().Substring(7) ?? null);

            lanka.kayttaja_id = k.kayttaja_id;
            lanka.aika = DateTime.Now;
            Alue alue = db.Alueet.Find(lanka.alue_id);

            if (!k.Kayttajatasot.o_lankaLisaa)
            {
                return Unauthorized();
            }

            if (alue == null)
            {
                return BadRequest("Aluetta ei löytynyt");
            }

            if (!alue.NakeekoKayttaja(k))
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (lanka.otsikko.Length < 1)
            {
                return BadRequest("Otsikko on liian olematon");
            }
            if (lanka.otsikko.Length > 50)
            {
                return BadRequest("Otsikko on liian pitkä");
            }


            db.Langat.Add(lanka);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { k.jwt, id = lanka.lanka_id }, lanka);
        }

        // DELETE: api/Lanka/5
        [ResponseType(typeof(Lanka))]
        public IHttpActionResult DeleteLanka(int id)
        {
            Kayttaja k = Kirjautuminen.HaeKirjautuminen(Request.Headers.Authorization?.ToString().Substring(7) ?? null);

            if (!k.OnKirjautunut)
            {
                return Unauthorized();
            }

            Lanka lanka = db.Langat.Find(id);
            if (lanka == null)
            {
                return NotFound();
            }

            if (lanka.Kayttajat.kayttaja_id == k.kayttaja_id)
            {
                // Tämä on oma lanka
                if (!k.Kayttajatasot.o_lankaOmaPoista)
                {
                    return Unauthorized();
                }
            }
            else
            {
                // Tämä on muiden lanka
                if (!k.Kayttajatasot.o_lankaPoista)
                {
                    return Unauthorized();
                }
            }

            db.Langat.Remove(lanka);
            db.SaveChanges();

            return Ok(k.jwt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LankaExists(int id)
        {
            return db.Langat.Count(e => e.lanka_id == id) > 0;
        }
    }
}