using ELibraryWebApp.Database;
using ELibraryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibraryWebApp.CollaborativeFiltering
{
    public class CollaborativeFilter
    {
        public ColoborativeRatingDifference _DiffMarix = new ColoborativeRatingDifference();
        public HashSet<int> _Items = new HashSet<int>();

        private void AddRatings(IDictionary<int, float> filterRatings)
        {
            foreach (var item1 in filterRatings)
            {
                int item1Id = item1.Key;
                float item1Rating = item1.Value;
                _Items.Add(item1.Key);

                foreach (var item2 in filterRatings)
                {
                    if (item2.Key <= item1Id) continue; // Eliminate redundancy
                    int item2Id = item2.Key;
                    float item2Rating = item2.Value;

                    ColoborativeRating ratingDiff;
                    if (_DiffMarix.Contains(item1Id, item2Id))
                    {
                        ratingDiff = _DiffMarix[item1Id, item2Id];
                    }
                    else
                    {
                        ratingDiff = new ColoborativeRating();
                        _DiffMarix[item1Id, item2Id] = ratingDiff;
                    }

                    ratingDiff.Value += item1Rating - item2Rating;
                    ratingDiff.Freq += 1;
                }
            }
        }

        private IDictionary<int, float> Recomend(IDictionary<int, float> userRatings)
        {
            Dictionary<int, float> Recomendations = new Dictionary<int, float>();
            foreach (var itemId in this._Items)
            {
                if (userRatings.Keys.Contains(itemId)) continue; // User has rated this item, just skip it

                ColoborativeRating itemRating = new ColoborativeRating();

                foreach (var userRating in userRatings)
                {
                    if (userRating.Key == itemId) continue;
                    int inputItemId = userRating.Key;
                    if (_DiffMarix.Contains(itemId, inputItemId))
                    {
                        ColoborativeRating diff = _DiffMarix[itemId, inputItemId];
                        itemRating.Value += diff.Freq * (userRating.Value + diff.AverageValue * ((itemId < inputItemId) ? 1 : -1));
                        itemRating.Freq += diff.Freq;
                    }
                }
                Recomendations.Add(itemId, itemRating.AverageValue);
            }
            return Recomendations;
        }

        public static List<RecommendBook> GetRecomendations(ELibraryContext context, string userId)
        {
            Dictionary<string, Dictionary<int, float>> dRatings = new Dictionary<string, Dictionary<int, float>>();
            List<Rating> ratings = context.Ratings.ToList();
            var userRating = dRatings[userId] = new Dictionary<int, float>();

            ratings.ForEach(r =>
            {
                if (!dRatings.ContainsKey(r.UserId))
                {
                    dRatings[r.UserId] = new Dictionary<int, float>();
                }
                dRatings[r.UserId][r.BookId] = (float)r.Value;
            });

            Dictionary<string, float> diff = new Dictionary<string, float>();

            for (int i = 0; i < dRatings.Count; ++i)
            {
                float sum = 0;
                int count = 0;
                for (int j = 0; j < userRating.Count; ++j)
                {
                    var item = userRating.ElementAt(j);
                    float rat = userRating[item.Key];

                    if (dRatings.ElementAt(i).Value.ContainsKey(item.Key))
                    {
                        float rat2 = dRatings.ElementAt(i).Value[item.Key];
                        sum += Math.Abs(rat - rat2);
                        count++;
                    }
                }
                if (count >= 3)
                {
                    diff[dRatings.ElementAt(i).Key] = sum / userRating.Count;
                }
            }

            CollaborativeFilter collaborativeFilter = new CollaborativeFilter();

            int userCount = 0;
            for (int i = 0; i < dRatings.Count; ++i)
            {
                if (diff.ContainsKey(dRatings.ElementAt(i).Key) && diff[dRatings.ElementAt(i).Key] <= 2)
                {
                    collaborativeFilter.AddRatings(dRatings.ElementAt(i).Value);
                    userCount++;
                }
            }

            if(userCount < 3)
            {
                return new List<RecommendBook>();
            }

            IDictionary<int, float> Recomendations = collaborativeFilter.Recomend(userRating);

            return context.Books.ToList().Where(b => Recomendations.ContainsKey(b.Id)).Select(b => new RecommendBook { Book = b, Rating = Recomendations[b.Id] }).Where(r => r.Rating >= 6).ToList();
        }
    }
}