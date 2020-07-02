using ELibraryWebApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibraryWebApp.Models
{
    public class AdminGenresViewModel
    {
        public List<Genre> Genres { get; set; }
        public Genre CreateGenreModel { get; set; }
    }
}