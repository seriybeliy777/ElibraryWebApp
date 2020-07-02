using ELibraryWebApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibraryWebApp.Models
{
    public class RecommendBook
    {
        public Book Book { get; set; }
        public float Rating { get; set; }
    }
}