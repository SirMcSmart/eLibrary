using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eLibraryPortal.Data.Context;
using eLibraryPortal.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace eLibraryPortal.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _adc;

        public UserController(ApplicationDbContext adc)
        {
            _adc = adc;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BookSuggestion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostBookSuggestion(BookSuggestion bookSuggestion)
        {
            try
            {
                if (bookSuggestion.BookName != null || bookSuggestion.BookAuthor != null)
                {
                    BookSuggestion bs = new BookSuggestion
                    {
                        FullName = "Bill Gates",
                        Email = "victor@gmail",
                        BookName = bookSuggestion.BookName,
                        BookAuthor = bookSuggestion.BookAuthor,
                        SuggestionDate = DateTime.Today
                    };
                    _adc.BookSuggestions.Add(bs);
                    _adc.SaveChanges();

                }

                return RedirectToAction("BookSuggestion");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}