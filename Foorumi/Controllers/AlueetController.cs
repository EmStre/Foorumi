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
    public class AlueetController : ApiController
    {
        private FoorumiModel db = new FoorumiModel();

        // GET: api/Alueet
        public IQueryable<Alue> GetAlueet()
        {
            return db.Alueet;
        }

        // GET: api/Alueet/5
        [ResponseType(typeof(Lanka))]
        public IHttpActionResult GetAlue(int id)
        {
            Alue alue = db.Alueet.Find(id);
            if (alue == null)
            {
                return NotFound();
            }

            return Ok(alue.Langat);
        }

        // PUT: api/Alueet/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlue(int id, Alue alue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alue.alue_id)
            {
                return BadRequest();
            }

            db.Entry(alue).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlueExists(id))
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

        // POST: api/Alueet
        [ResponseType(typeof(Alue))]
        public IHttpActionResult PostAlue(Alue alue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alueet.Add(alue);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = alue.alue_id }, alue);
        }

        // DELETE: api/Alueet/5
        [ResponseType(typeof(Alue))]
        public IHttpActionResult DeleteAlue(int id)
        {
            Alue alue = db.Alueet.Find(id);
            if (alue == null)
            {
                return NotFound();
            }

            db.Alueet.Remove(alue);
            db.SaveChanges();

            return Ok(alue);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlueExists(int id)
        {
            return db.Alueet.Count(e => e.alue_id == id) > 0;
        }
    }
}