using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Foorumi.Models
{
    public partial class Kayttaja
    {
        [IgnoreDataMember]
        [NotMapped]
        public string jwt { get; set; } = "";

        [IgnoreDataMember]
        [NotMapped]
        public bool OnKirjautunut { get => this.kayttajataso_id != Kirjautuminen.VierasKayttajaTaso; }

        public override string ToString()
        {
            return this.nimimerkki;
        }
    }

    public partial class Viesti
    {
        [DataMember]
        [NotMapped]
        public int? MinuuttiaSitten
        {
            get
            {
                if (this.aika == null)
                {
                    return null;
                }

                TimeSpan erotus = DateTime.Now - (DateTime)this.aika;
                return (int?)erotus.TotalMinutes ?? null;
            }
        }

    }

    public partial class Alue
    {
        public bool NakeekoKayttaja(Kayttaja k)
        {
            if (!this.rajoitettu) // Ei rajoitettu alue, näytetään kaikille
            {
                return true;
            }

            // Tarkastetaan onko käyttäjällä lukuoikeus
            return this.Kayttajatasot.Where(kt => kt.kayttajataso_id == k.kayttajataso_id || k.kayttajataso_id == 1).Any();

        }
        public override string ToString()
        {
            return this.otsikko;
        }

        [DataMember]
        [NotMapped]
        public DateTime? ViimeisinViesti { get => this.Langat.Any() ? this.Langat.Select(v => v.ViimeisinViesti).Max() : (DateTime?)null; }

        [DataMember]
        [NotMapped]
        public int? ViimeisimmastaViestistaMinuutteja
        {
            get
            {
                if (this.ViimeisinViesti == null)
                {
                    return null;
                }

                TimeSpan erotus = DateTime.Now - (DateTime)this.ViimeisinViesti;
                return (int?)erotus.TotalMinutes ?? null;
            }
        }
    }

    public partial class Lanka
    {
        [DataMember]
        [NotMapped]
        public DateTime? ViimeisinViesti { get => this.Viestit.Any() ? this.Viestit.Select(v => v.aika).Max() : (DateTime?)null; }

        [DataMember]
        [NotMapped]
        public int? ViimeisimmastaViestistaMinuutteja
        {
            get
            {
                if (this.ViimeisinViesti == null)
                {
                    return null;
                }

                TimeSpan erotus = DateTime.Now - (DateTime)this.ViimeisinViesti;
                return (int?)erotus.TotalMinutes ?? null;
            }
        }
    }
}