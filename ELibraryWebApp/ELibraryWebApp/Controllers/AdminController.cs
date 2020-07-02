using ELibraryWebApp.Database;
using ELibraryWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELibraryWebApp.Controllers
{
    public class AdminController : Controller
    {
        ELibraryContext context = new ELibraryContext();
        public FilterModel FilterModel
        {
            get { return (FilterModel)Session["FilterModel"]; }
            set { Session["FilterModel"] = value; }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Books()
        {
            AdminBooksViewModel viewModel = new AdminBooksViewModel
            {
                Books = context.Books.ToList(),
                Genres = context.Genres.ToList(),
                Countries = context.Countries.ToList(),
                CreateBookModel = new CreateBookModel
                {
                     Year = 1500,
                     PageCount = 100
                },
                FilterModel = FilterModel ?? new FilterModel()
            };

            viewModel.Genres.ForEach(genre =>
            {
                viewModel.CreateBookModel.CheckedGenreObjects.Add(
                    new CheckedGenreObject
                    {
                        Id = genre.Id,
                        Checked = false
                    });
            });

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public PartialViewResult Filter(FilterModel filterModel)
        {
            FilterModel = filterModel;

            string search = null;
            if (filterModel.Search != null)
            {
                search = filterModel.Search.ToLower();
            }
            List<Book> books = context.Books.Where(b => 
                (search == null || b.Name.ToLower().Contains(search) || b.Description.ToLower().Contains(search) || b.Author.ToLower().Contains(search)) &&
                (filterModel.CountryId == -1 || b.CountryId == filterModel.CountryId) &&
                (filterModel.GenreId == -1 || b.Genres.Any(g => g.Id == filterModel.GenreId)) &&
                (filterModel.Rating == 0 || b.Ratings.Average(r => r.Value) >= filterModel.Rating)).ToList();
            return PartialView("BooksPartial", books);
        }
        
        public ActionResult ToggleTheme(string url)
        {
            if(Session["isDark"] == null)
            {
                Session["isDark"] = true;
            }
            Session["isDark"] = !(bool)Session["isDark"];

            return Redirect(url);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreateBook(CreateBookModel createBookModel)
        {
            try
            {
                List<Genre> genres = createBookModel.CheckedGenreObjects.Where(genre => genre.Checked).Select(genre => context.Genres.First(g => g.Id == genre.Id)).ToList();

                Book book = new Book
                {
                    Author = createBookModel.Author,
                    PageCount = createBookModel.PageCount,
                    Year = createBookModel.Year,
                    CountryId = createBookModel.CountryId,
                    Description = createBookModel.Description,
                    Name = createBookModel.Name,
                    PurchaseUrl = createBookModel.PurchaseUrl,
                    Genres = genres
                };

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(createBookModel.File.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/BookImages"), fileName);
                createBookModel.File.SaveAs(path);

                book.ImageUrl = "../Content/BookImages/" + fileName;

                context.Books.Add(book);
                context.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Books");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public PartialViewResult RemoveBook(int id)
        {
            Book book = context.Books.First(b => b.Id == id);
            context.Books.Remove(book);
            context.SaveChanges();

            return PartialView("BooksPartial", context.Books.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Genres()
        {
            AdminGenresViewModel viewModel = new AdminGenresViewModel
            {
                Genres = context.Genres.ToList(),
                CreateGenreModel = new Genre()
            };

            return View(viewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public PartialViewResult CreateGenre(Genre genre)
        {
            try
            {
                context.Genres.Add(genre);
                context.SaveChanges();
            }
            catch
            {

            }

            return PartialView("GenresPartial", context.Genres.ToList());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public PartialViewResult RemoveGenre(int id)
        {
            Genre genre = context.Genres.First(g => g.Id == id);
            context.Genres.Remove(genre);
            context.SaveChanges();

            return PartialView("GenresPartial", context.Genres.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Countries()
        {
            AdminCountriesViewModel viewModel = new AdminCountriesViewModel
            {
                Countries = context.Countries.ToList(),
                CreateCountryModel = new Country()
            };

            return View(viewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public PartialViewResult CreateCountry(Country country)
        {
            try
            {
                context.Countries.Add(country);
                context.SaveChanges();
            }
            catch
            {

            }

            return PartialView("CountriesPartial", context.Countries.ToList());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public PartialViewResult RemoveCountry(int id)
        {
            Country country = context.Countries.First(g => g.Id == id);
            context.Countries.Remove(country);
            context.SaveChanges();

            return PartialView("CountriesPartial", context.Countries.ToList());
        }
    }
}