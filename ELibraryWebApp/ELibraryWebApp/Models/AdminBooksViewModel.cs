using ELibraryWebApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibraryWebApp.Models
{
    public class AdminBooksViewModel
    {
        public List<Book> Books { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Country> Countries { get; set; }
        public CreateBookModel CreateBookModel { get; set; }
        public FilterModel FilterModel { get; set; }
    }
}