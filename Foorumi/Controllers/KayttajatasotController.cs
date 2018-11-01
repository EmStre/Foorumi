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
    public class KayttajatasotController : ApiController
    {
        private FoorumiModel db = new FoorumiModel();

        // GET: api/Kayttajatasot
        public IHttpActionResult GetKayttajatasot()
        {
            Kayttaja k = Kirjautuminen.HaeKirjautuminen(Request.Headers.Authorization?.ToString().Substring(7) ?? null);
            if (!k.OnKirjautunut)
                return Unauthorized();

            return Ok(db.Kayttajatasot);
        }

        // GET: api/Kayttajatasot/5
        [ResponseType(typeof(Kayttajataso))]
        public IHttpActionResult GetKayttajataso(int id)
        {
            Kayttaja k = Kirjautuminen.HaeKirjautuminen(Request.Headers.Authorization?.ToString().Substring(7) ?? null);
            if (!k.OnKirjautunut)
                return Unauthorized();

            Kayttajataso kayttajataso = db.Kayttajatasot.Find(id);
            if (kayttajataso == null)
            {
                return NotFound();
            }

            return Ok(kayttajataso);
        }

        //// PUT: api/Kayttajatasot/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutKayttajataso(int id, Kayttajataso kayttajataso)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != kayttajataso.kayttajataso_id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(kayttajataso).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!KayttajatasoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Kayttajatasot
        //[ResponseType(typeof(Kayttajataso))]
        //public IHttpActionResult PostKayttajataso(Kayttajataso kayttajataso)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Kayttajatasot.Add(kayttajataso);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = kayttajataso.kayttajataso_id }, kayttajataso);
        //}

        //// DELETE: api/Kayttajatasot/5
        //[ResponseType(typeof(Kayttajataso))]
        //public IHttpActionResult DeleteKayttajataso(int id)
        //{
        //    Kayttajataso kayttajataso = db.Kayttajatasot.Find(id);
        //    if (kayttajataso == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Kayttajatasot.Remove(kayttajataso);
        //    db.SaveChanges();

        //    return Ok(kayttajataso);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KayttajatasoExists(int id)
        {
            return db.Kayttajatasot.Count(e => e.kayttajataso_id == id) > 0;
        }
    }
}