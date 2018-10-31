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

namespace Foorumi.Controllers
{
    public class KayttajatController : ApiController
    {
        private FoorumiModel db = new FoorumiModel();




        // GET: api/Kayttajat
        public IQueryable<Kayttaja> GetKayttajat()
        {
            return db.Kayttajat;
        }

        // GET: api/Kayttajat/5
        [ResponseType(typeof(Kayttaja))]
        public IHttpActionResult GetKayttaja(int id)
        {
            Kayttaja kayttaja = db.Kayttajat.Find(id);
            if (kayttaja == null)
            {
                return NotFound();
            }

            return Ok(kayttaja);
        }

        // PUT: api/Kayttajat/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKayttaja(int id, Kayttaja kayttaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kayttaja.kayttaja_id)
            {
                return BadRequest();
            }

            db.Entry(kayttaja).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KayttajaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Kayttajat
        [ResponseType(typeof(Kayttaja))]
        public IHttpActionResult PostKayttaja(Kayttaja kayttaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kayttajat.Add(kayttaja);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kayttaja.kayttaja_id }, kayttaja);
        }

        // DELETE: api/Kayttajat/5
        [ResponseType(typeof(Kayttaja))]
        public IHttpActionResult DeleteKayttaja(int id)
        {
            Kayttaja kayttaja = db.Kayttajat.Find(id);
            if (kayttaja == null)
            {
                return NotFound();
            }

            db.Kayttajat.Remove(kayttaja);
            db.SaveChanges();

            return Ok(kayttaja);
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