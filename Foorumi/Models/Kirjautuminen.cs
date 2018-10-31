using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Foorumi.Models
{
    public static class Kirjautuminen
    {
        public const int OletusKayttajataso = 2;
        private const int Vanhenemisaika = 20;
        private const string salausAvain = "5KmoimitäkuuluuMKktvkXVt1JV1hAtA3XFzQFqpH3yH3bUcNkHUex6WxREeSqMfq6tn/OCYAej4MFGPTVLtSLJ6NhLWN/NFfcBccED31cu5RgqR"; // SALAINEN salausavain!!!!
        private const string issuer = "Foorumi";
        private const string audience = "Foorumi";

        public static int ValidoiJwt(string tokenString)
        {
            if (tokenString == null)
            {
                return -1;
            }

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(salausAvain));

            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = securityKey
            };



            SecurityToken validatedToken;

            try
            {
                handler.ValidateToken(tokenString, validationParameters, out validatedToken);
            }
            catch (Exception)
            {
                return -1;
            }

            JwtSecurityToken jwt = handler.ReadToken(tokenString) as JwtSecurityToken;
            return int.Parse(jwt.Claims.First(c => c.Type == "kayttaja_id").Value);

        }


        public static string GeneroiJwtString(int kayttaja_id)
        {
            //JwtPayload payload = new JwtPayload {
            //    { "kid", kayttaja_id.ToString() },
            //    { "iat", DateTime.Now.Subtract(new DateTime(1970, 1,1)).TotalMilliseconds },
            //    { "exp", DateTime.Now.AddMinutes(Vanhenemisaika).Subtract(new DateTime(1970, 1,1)).TotalMilliseconds.ToString()
            //     }
            // };

            JwtPayload payload = new JwtPayload(issuer, audience, null, null, DateTime.Now.AddMinutes(Vanhenemisaika)) { { "kayttaja_id", kayttaja_id} };

            JwtSecurityToken token = GeneroiJwtToken(payload);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken secToken = GeneroiJwtToken(payload);
            return handler.WriteToken(secToken);
        }

        private static JwtSecurityToken GeneroiJwtToken(JwtPayload payload)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(salausAvain));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtHeader header = new JwtHeader(credentials);

            JwtSecurityToken secToken = new JwtSecurityToken(header, payload);

            return secToken;
        }

        public static string GeneroiHash(string pwd)
        {
            byte[] suola;
            new RNGCryptoServiceProvider().GetBytes(suola = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(pwd, suola, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(suola, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        public static bool ValidoiSalasana(string pwd, string hashattu)
        {
            byte[] hashBytes = Convert.FromBase64String(hashattu);
            byte[] suola = new byte[16];

            Array.Copy(hashBytes, 0, suola, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(pwd, suola, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false; // Ei täsmää
                }
            }

            return true;
        }

    }
}