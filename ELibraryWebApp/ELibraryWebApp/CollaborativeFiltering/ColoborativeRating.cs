using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibraryWebApp.CollaborativeFiltering
{
    public class ColoborativeRating
    {
        public float Value { get; set; }
        public int Freq { get; set; }

        public float AverageValue
        {
            get { return Value / Freq; }
        }
    }
}