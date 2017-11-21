namespace DbOps2
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbModel : DbContext
    {
        public DbModel()
            : base("name=DbModel")
        {
        }

        public virtual DbSet<Financial> Financials { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Financial>()
                .Property(e => e.Client)
                .IsUnicode(false);

            modelBuilder.Entity<Financial>()
                .Property(e => e.Photographer)
                .IsUnicode(false);

            modelBuilder.Entity<Financial>()
                .Property(e => e.Paid)
                .IsUnicode(false);

            modelBuilder.Entity<Financial>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Financials)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.ClientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Photos)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.PhotographerId)
                .WillCascadeOnDelete(false);
        }
    }
}
