namespace JobSpecDB.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Specs : DbContext
    {
        public Specs()
            : base("name=Specs")
        {
        }

        public virtual DbSet<jobspec> jobspecs { get; set; }
        public virtual DbSet<jobspecword> jobspecwords { get; set; }
        public virtual DbSet<word> words { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<jobspec>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<jobspec>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<jobspec>()
                .Property(e => e.source)
                .IsUnicode(false);

            modelBuilder.Entity<jobspec>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<jobspec>()
                .Property(e => e.spectext)
                .IsUnicode(false);

            modelBuilder.Entity<word>()
                .Property(e => e.word1)
                .IsUnicode(false);

            modelBuilder.Entity<word>()
                .Property(e => e.skillName)
                .IsUnicode(false);
        }*/
    }
}
