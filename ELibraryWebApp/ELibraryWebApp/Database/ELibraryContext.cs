namespace ELibraryWebApp.Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ELibraryWebApp.Models;

    public partial class ELibraryContext : IdentityDbContext<ApplicationUser>
    {
        public ELibraryContext()
            : base("ELibraryContext", throwIfV1Schema: false)
        {
        }

        public static ELibraryContext Create()
        {
            return new ELibraryContext();
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Genres)
                .WithMany(e => e.Books)
                .Map(m => m.ToTable("BookGenre").MapLeftKey("BookId").MapRightKey("GenreId"));

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);
        }
    }
}
