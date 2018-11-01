using Foorumi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Foorumi.Controllers
{
    public class TokentestiController : ApiController

    {
        private FoorumiModel db = new FoorumiModel();

        // GET: api/Alueet

        public IHttpActionResult GetAlueet()
        {
            string jwt = Request.Headers.Authorization?.ToString().Substring(7);
            if (Kirjautuminen.ValidoiJwt(jwt))
            {
                return Ok(jwt);
            } else
            {
                return Unauthorized();
            }

        }
    }
}
