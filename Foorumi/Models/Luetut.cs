namespace Foorumi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Luetut")]
    public partial class Luetut
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int kayttaja_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int lanka_id { get; set; }

        public DateTime? aika { get; set; } = DateTime.Now;

        public virtual Kayttaja Kayttajat { get; set; }

        public virtual Lanka Langat { get; set; }
    }
}
