using Foorumi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Foorumi.Controllers
{
    public class LogoutController : ApiController
    {

        private FoorumiModel db = new FoorumiModel();

        [HttpGet]
        public IHttpActionResult Logout()
        {
            Kayttaja k = Kirjautuminen.HaeKirjautuminen(Request.Headers.Authorization?.ToString().Substring(7) ?? null);
            if (k.OnKirjautunut)
            {
                Kayttaja kayttaja = db.Kayttajat.Find(k.kayttaja_id);
                kayttaja.Sessio = "";
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
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
