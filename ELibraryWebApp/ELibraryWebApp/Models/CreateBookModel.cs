using ELibraryWebApp.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELibraryWebApp.Models
{
    public class CreateBookModel
    {
        [Required]
        [Display(Name="Название")]
        public string Name { get; set; }

        [Display(Name="Год издания")]
        public int Year { get; set; }
        [Display(Name="Содержание")]
        public int PageCount { get; set; }

        [Display(Name = "Страна")]
        public int CountryId { get; set; }

        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Ссылка на скачивание")]
        public string PurchaseUrl { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Постер")]
        public HttpPostedFileBase File { get; set; }
        [Display(Name = "Жанры")]
        public List<CheckedGenreObject> CheckedGenreObjects { get; set; }

        public CreateBookModel()
        {
            CheckedGenreObjects = new List<CheckedGenreObject>();
        }
    }
}