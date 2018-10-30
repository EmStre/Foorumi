namespace Foorumi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("Langat")]
    public partial class Lanka
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lanka()
        {
            Luetut = new HashSet<Luetut>();
            Viestit = new HashSet<Viesti>();
        }

        [Key]
        [DataMember]
        public int lanka_id { get; set; }

        [DataMember]
        public int kayttaja_id { get; set; }

        [DataMember]
        public int alue_id { get; set; }

        [DataMember]
        public DateTime aika { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember]
        public string otsikko { get; set; }

        [DataMember]
        public bool lukittu { get; set; }

        [DataMember]
        public bool kiinnitetty { get; set; }

        [IgnoreDataMember]
        public virtual Alue Alueet { get; set; }

        [DataMember]
        public virtual Kayttaja Kayttajat { get; set; }

        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Luetut> Luetut { get; set; }

        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Viesti> Viestit { get; set; }
    }
}
