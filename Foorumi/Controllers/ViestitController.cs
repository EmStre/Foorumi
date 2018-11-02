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
    public class ViestitController : ApiController
    {
        private FoorumiModel db = new FoorumiModel();

        //// GET: api/Viestit
        //public IQueryable<Viesti> GetViestit()
        //{
        //    return db.Viestit;
        //}

        //// GET: api/Viestit/5
        //[ResponseType(typeof(Viesti))]
        //public IHttpActionResult GetViesti(int id)
        //{
        //    Viesti viesti = db.Viestit.Find(id);
        //    if (viesti == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(viesti);
        //}

        //// PUT: api/Viestit/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutViesti(int id, Viesti viesti)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != viesti.viesti_id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(viesti).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ViestiExists(id))
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

        // POST: api/Viestit
        [HttpPost]
        public IHttpActionResult PostViesti([FromBody]dynamic value)
        {
            Kayttaja k = Kirjautuminen.HaeKirjautuminen(Request.Headers.Authorization?.ToString().Substring(7) ?? null);
            if (!k.OnKirjautunut)
            {
                return Unauthorized();
            }

            Viesti viesti = new Viesti();

            try
            {

                viesti.otsikko = value.otsikko.Value;
                viesti.viesti = value.viesti.Value;
                viesti.lanka_id = int.Parse(value.lanka_id.Value);
                viesti.aika = DateTime.Now;
                viesti.kayttaja_id = k.kayttaja_id;

            }
            catch (Exception)
             {
                return BadRequest();
            }
            if (viesti.otsikko == null || viesti.viesti == null || viesti.lanka_id == 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Viestit.Add(viesti);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = viesti.viesti_id }, viesti);
        }

        //// DELETE: api/Viestit/5
        //[ResponseType(typeof(Viesti))]
        //public IHttpActionResult DeleteViesti(int id)
        //{
        //    Viesti viesti = db.Viestit.Find(id);
        //    if (viesti == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Viestit.Remove(viesti);
        //    db.SaveChanges();

        //    return Ok(viesti);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ViestiExists(int id)
        {
            return db.Viestit.Count(e => e.viesti_id == id) > 0;
        }
    }
}