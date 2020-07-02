using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELibraryWebApp.Models
{
    public class FilterModel
    {
        [Display(Name = "Поиск")]
        public string Search { get; set; }
        
        [Display(Name = "Страна")]
        public int CountryId { get; set; }

        [Display(Name = "Жанр")]
        public int GenreId { get; set; }

        [Display(Name = "Мин. рейтинг")]
        public double Rating { get; set; } 

        public FilterModel()
        {
            Rating = 0;
        }
    }
}