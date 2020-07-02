using ELibraryWebApp.CollaborativeFiltering;
using ELibraryWebApp.Database;
using ELibraryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ELibraryWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ELibraryContext context = new ELibraryContext();
        public FilterModel FilterModel
        {
            get { return (FilterModel)Session["FilterModel"]; }
            set { Session["FilterModel"] = value; }
        }

        public ActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Books", "Admin");
            }
            return RedirectToAction("Books");
        }

        [Authorize(Roles = "user")]
        public ActionResult Books()
        {
            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {
                Books = context.Books.ToList(),
                Recommendations = CollaborativeFilter.GetRecomendations(context, User.Identity.GetUserId()),
                Genres = context.Genres.ToList(),
                Countries = context.Countries.ToList(),
                FilterModel = FilterModel ?? new FilterModel()
            };

            return View(viewModel);
        }

        [HttpPost]
        public PartialViewResult SetRating(Rating rating)
        {
            Rating existed = context.Ratings.FirstOrDefault(r => r.BookId == rating.BookId && r.UserId == rating.UserId);

            if(existed == null)
            {
                context.Ratings.Add(rating);
            }
            else
            {
                existed.Value = rating.Value;
            }
            context.SaveChanges();

            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {
                Books = context.Books.ToList(),
                Recommendations = CollaborativeFilter.GetRecomendations(context, User.Identity.GetUserId()),
                Genres = context.Genres.ToList(),
                Countries = context.Countries.ToList(),
                FilterModel = FilterModel ?? new FilterModel()
            };

            return PartialView("BooksRecomendationsPartial", viewModel);
        }

        [HttpPost]
        public PartialViewResult RemoveRating(int id)
        {
            Rating rating = context.Ratings.First(r => r.Id == id);
            context.Ratings.Remove(rating);
            context.SaveChanges();

            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {
                Books = context.Books.ToList(),
                Recommendations = CollaborativeFilter.GetRecomendations(context, User.Identity.GetUserId()),
                Genres = context.Genres.ToList(),
                Countries = context.Countries.ToList(),
                FilterModel = FilterModel ?? new FilterModel()
            };

            return PartialView("BooksRecomendationsPartial", viewModel);
        }
    }
}