using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eLibraryPortal.Core.Helper;
using eLibraryPortal.Core.Interface;
using eLibraryPortal.Data.Context;
using eLibraryPortal.Data.Enums;
using eLibraryPortal.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eLibraryPortal.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _adc;
        private readonly IUserFunction _iuf;

        public UserController(ApplicationDbContext adc, IUserFunction iuf)
        {
            _adc = adc;
            _iuf = iuf;
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
                    var IsSuccessful = _iuf.CreateBookSuggestion(bookSuggestion);

                    if (IsSuccessful)
                    {
                        var showMessage = new AlertMessage
                        {
                            Title = "BOOK SUGGESTION",
                            Message = "Book has been Suggested successfully!",
                            MessageType = MessageType.SuccessMessage
                        };
                        Message = JsonConvert.SerializeObject(showMessage);
                    }
                    else
                    {
                        var showMessage = new AlertMessage
                        {
                            Title = "BOOK SUGGESTION",
                            Message = "Book was not successfully suggested!",
                            MessageType = MessageType.ErrorMessage
                        };
                        Message = JsonConvert.SerializeObject(showMessage);
                    }
                    return RedirectToAction("BookSuggestion");
                }


                return RedirectToAction("BookSuggestion");


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public AlertMessage showMessage { get; set; }
        public object Message
        {
            get
            {
                return TempData["swMESSAGE"];
            }
            set
            {
                TempData["swMESSAGE"] = value;
            }

        }
    }    
}