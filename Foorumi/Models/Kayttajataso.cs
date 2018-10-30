namespace Foorumi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Table("Kayttajatasot")]
    [DataContract]
    public partial class Kayttajataso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kayttajataso()
        {
            Kayttajat = new HashSet<Kayttaja>();
            Alueet = new HashSet<Alue>();
        }

        [Key]
        [DataMember]
        public int kayttajataso_id { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember]
        public string nimi { get; set; }

        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kayttaja> Kayttajat { get; set; }

        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alue> Alueet { get; set; }
    }
}
