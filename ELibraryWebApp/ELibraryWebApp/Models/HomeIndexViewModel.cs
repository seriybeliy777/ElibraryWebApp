using ELibraryWebApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibraryWebApp.Models
{
    public class HomeIndexViewModel
    {
        public List<Book> Books { get; set; }
        public List<RecommendBook> Recommendations { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Country> Countries { get; set; }
        public FilterModel FilterModel { get; set; }
    }
}