namespace ELibraryWebApp.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            Ratings = new HashSet<Rating>();
            Genres = new HashSet<Genre>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Year { get; set; }
        public int PageCount { get; set; }

        public int CountryId { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string PurchaseUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual Country Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rating> Ratings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
