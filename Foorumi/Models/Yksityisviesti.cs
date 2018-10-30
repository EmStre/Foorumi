namespace Foorumi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yksityisviestit")]
    public partial class Yksityisviesti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Yksityisviesti()
        {
            Yksityisviestit1 = new HashSet<Yksityisviesti>();
        }

        [Key]
        public int yksityisviesti_id { get; set; }

        public int lahettaja_id { get; set; }

        public int vastaanottaja_id { get; set; }

        public int? vastaus { get; set; }

        [Required]
        [StringLength(50)]
        public string otsikko { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string viesti { get; set; }

        public bool luettu { get; set; }

        public DateTime? aika { get; set; }

        public DateTime? lukuaika { get; set; }

        public virtual Kayttaja Kayttajat { get; set; }

        public virtual Kayttaja Kayttajat1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yksityisviesti> Yksityisviestit1 { get; set; }

        public virtual Yksityisviesti Yksityisviestit2 { get; set; }
    }
}
