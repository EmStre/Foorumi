namespace Foorumi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("Viestit")]
    public partial class Viesti
    {
        [DataMember]
        [Key]
        public int viesti_id { get; set; }

        [IgnoreDataMember]
        public int kayttaja_id { get; set; }

        [DataMember]
        public int lanka_id { get; set; }

        [DataMember]
        [Required]
        [StringLength(50)]
        public string otsikko { get; set; }

        [DataMember]
        [Column(TypeName = "ntext")]
        [Required]
        public string viesti { get; set; }

        [DataMember]
        public DateTime aika { get; set; }

        [DataMember(Name = "Kirjoittaja")]
        public virtual Kayttaja Kayttajat { get; set; }

        [IgnoreDataMember]
        public virtual Lanka Langat { get; set; }
    }
}
