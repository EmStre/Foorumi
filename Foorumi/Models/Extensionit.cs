using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Foorumi.Models
{
    public partial class Kayttaja
    {
        [IgnoreDataMember]
        public string jwt { get; set; } = "";
    }

    public partial class Alue
    {
        public bool NakeekoKayttaja(Kayttaja k)
        {
            if (!this.rajoitettu) // Ei rajoitettu alue, näytetään kaikille
                return true;

            // Tarkastetaan onko käyttäjällä lukuoikeus
            return this.Kayttajatasot.Where(kt => kt.kayttajataso_id == k.kayttajataso_id || k.kayttajataso_id == 1).Any();

        }
    }
}