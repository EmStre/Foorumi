namespace Foorumi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Viestit")]
    public partial class Viesti
    {
        [Key]
        public int viesti_id { get; set; }

        public int kayttaja_id { get; set; }

        public int lanka_id { get; set; }

        [Required]
        [StringLength(50)]
        public string otsikko { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string viesti { get; set; }

        public DateTime aika { get; set; }

        public virtual Kayttaja Kayttajat { get; set; }

        public virtual Lanka Langat { get; set; }
    }
}
