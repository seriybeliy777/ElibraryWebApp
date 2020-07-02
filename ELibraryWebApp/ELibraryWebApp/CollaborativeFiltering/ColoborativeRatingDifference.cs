using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibraryWebApp.CollaborativeFiltering
{
    public class ColoborativeRatingDifference : Dictionary<string, ColoborativeRating>
    {
        private string GetKey(int id1, int id2)
        {
            return (id1 < id2) ? id1 + "/" + id2 : id2 + "/" + id1;
        }

        public bool Contains(int id1, int id2)
        {
            return this.Keys.Contains<string>(GetKey(id1, id2));
        }

        public ColoborativeRating this[int id1, int id2]
        {
            get
            {
                return this[this.GetKey(id1, id2)];
            }
            set { this[this.GetKey(id1, id2)] = value; }
        }
    }
}