namespace Foorumi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("Kayttajat")]
    public partial class Kayttaja
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kayttaja()
        {
            Langat = new HashSet<Lanka>();
            Luetut = new HashSet<Luetut>();
            Viestit = new HashSet<Viesti>();
            Yksityisviestit = new HashSet<Yksityisviesti>();
            Yksityisviestit1 = new HashSet<Yksityisviesti>();
        }

        [Key]
        [DataMember]
        public int kayttaja_id { get; set; }

        public int kayttajataso_id { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember]
        public string email { get; set; }

        [Required]
        [StringLength(20)]
        [DataMember]
        public string nimimerkki { get; set; }

        [Required]
        [StringLength(64)]
        [IgnoreDataMember]
        public string hash { get; set; }

        [Column(TypeName = "image")]
        [DataMember]
        public byte[] kuva { get; set; }

        [StringLength(200)]
        [DataMember]
        public string kuvaus { get; set; }

        [DataMember]
        public DateTime? aktiivisuus { get; set; } = DateTime.Now;

        [DataMember(Name = "Kayttajataso")]
        public virtual Kayttajataso Kayttajatasot { get; set; }

        [IgnoreDataMember]
        public string Sessio { get; set; }

        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lanka> Langat { get; set; }

        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Luetut> Luetut { get; set; }

        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Viesti> Viestit { get; set; }

        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yksityisviesti> Yksityisviestit { get; set; }

        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yksityisviesti> Yksityisviestit1 { get; set; }
    }
}
