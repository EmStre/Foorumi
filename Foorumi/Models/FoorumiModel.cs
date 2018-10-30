namespace Foorumi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FoorumiModel : DbContext
    {
        public FoorumiModel()
            : base("name=FoorumiDbModel")
        {
        }

        public virtual DbSet<Alue> Alueet { get; set; }
        public virtual DbSet<Kayttaja> Kayttajat { get; set; }
        public virtual DbSet<Kayttajataso> Kayttajatasot { get; set; }
        public virtual DbSet<Lanka> Langat { get; set; }
        public virtual DbSet<Luetut> Luetut { get; set; }
        public virtual DbSet<Viesti> Viestit { get; set; }
        public virtual DbSet<Yksityisviesti> Yksityisviestit { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alue>()
                .HasMany(e => e.Langat)
                .WithRequired(e => e.Alueet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Alue>()
                .HasMany(e => e.Kayttajatasot)
                .WithMany(e => e.Alueet)
                .Map(m => m.ToTable("Lukuoikeudet").MapLeftKey("alue_id").MapRightKey("kayttajataso_id"));

            modelBuilder.Entity<Kayttaja>()
                .Property(e => e.hash)
                .IsUnicode(false);

            modelBuilder.Entity<Kayttaja>()
                .HasMany(e => e.Langat)
                .WithRequired(e => e.Kayttajat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kayttaja>()
                .HasMany(e => e.Luetut)
                .WithRequired(e => e.Kayttajat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kayttaja>()
                .HasMany(e => e.Viestit)
                .WithRequired(e => e.Kayttajat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kayttaja>()
                .HasMany(e => e.Yksityisviestit)
                .WithRequired(e => e.Kayttajat)
                .HasForeignKey(e => e.lahettaja_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kayttaja>()
                .HasMany(e => e.Yksityisviestit1)
                .WithRequired(e => e.Kayttajat1)
                .HasForeignKey(e => e.vastaanottaja_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kayttajataso>()
                .HasMany(e => e.Kayttajat)
                .WithRequired(e => e.Kayttajatasot)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lanka>()
                .HasMany(e => e.Luetut)
                .WithRequired(e => e.Langat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lanka>()
                .HasMany(e => e.Viestit)
                .WithRequired(e => e.Langat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yksityisviesti>()
                .HasMany(e => e.Yksityisviestit1)
                .WithOptional(e => e.Yksityisviestit2)
                .HasForeignKey(e => e.vastaus);
        }
    }
}
