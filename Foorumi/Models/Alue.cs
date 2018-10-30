namespace Foorumi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Table("Alueet")]
    [DataContract]
    public partial class Alue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alue()
        {
            Langat = new HashSet<Lanka>();
            Kayttajatasot = new HashSet<Kayttajataso>();
        }

        [Key]
        [DataMember]
        public int alue_id { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember]
        public string otsikko { get; set; }

        [Required]
        [StringLength(200)]
        [DataMember]
        public string kuvaus { get; set; }

        [DataMember]
        public bool rajoitettu { get; set; }

        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lanka> Langat { get; set; }
        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kayttajataso> Kayttajatasot { get; set; }
    }
}
