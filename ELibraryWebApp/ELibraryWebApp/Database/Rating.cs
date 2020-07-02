namespace ELibraryWebApp.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rating")]
    public partial class Rating
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int BookId { get; set; }

        public double Value { get; set; }

        public virtual Book Book { get; set; }
    }
}
