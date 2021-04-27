using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eLibraryPortal.Core.Interface;
using eLibraryPortal.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eLibraryPortal.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminFunction _adminFunc;
        public AdminController(IAdminFunction adminFun)
        {
            _adminFunc = adminFun;
        }

        public IActionResult LandingPage()
        {
            return View();
        }
        public IActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostCreateBook(Book book, IFormFile BookImage , IFormFile FileAthachment)
        {
            try
            {
                if (book.BookName != null || book.BookEdition != null)
                {
                    var status = await _adminFunc.SaveBook( book, BookImage , FileAthachment);
                }
                return View("CreateBook");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}