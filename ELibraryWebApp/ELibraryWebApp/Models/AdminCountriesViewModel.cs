using ELibraryWebApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibraryWebApp.Models
{
    public class AdminCountriesViewModel
    {
        public List<Country> Countries { get; set; }
        public Country CreateCountryModel { get; set; }
    }
}