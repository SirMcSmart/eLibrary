using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eLibraryPortal.Core.Helper;
using eLibraryPortal.Core.Interface;
using eLibraryPortal.Data.Context;
using eLibraryPortal.Data.Enums;
using eLibraryPortal.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace eLibraryPortal.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminFunction _adminFunc;
        private readonly ApplicationDbContext _adc;
        private readonly IConfiguration _config;
        //private readonly UserManager<Users> _userManager;
        //private readonly RoleManager<UserRole> _roleManager;
        //private readonly IRepository<Users> _UserRepo;
        public AdminController(IAdminFunction adminFun, ApplicationDbContext adc, IConfiguration config )
        {
            _adminFunc = adminFun;
            _adc = adc;
            _config = config;
           
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
                    if(status == true)
                    {
                        //Successful
                    }
                    else
                    {
                        //Not Successful
                    }
                }
                else
                {

                }
                return View("CreateBook");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IActionResult BookList()
        {
            return View();
        }

        public IActionResult BookSuggestionList()
        {
            var result = _adminFunc.GetBookSuggestions();
            return View(result);
        }

        public IActionResult CreateUsers()
        {
            return View();
        }

        public async Task<IActionResult> PostCreateUser(Users user, [FromForm]IFormFile ProfileImage)
        {
            try
            {

                    var IsSuccessful = await _adminFunc.PostCreateUsers(user, ProfileImage);

                    if (IsSuccessful)
                    {
                        var showMessage = new AlertMessage
                        {
                            Title = "USER CREATION",
                            Message = "User has been Created successfully!",
                            MessageType = MessageType.SuccessMessage
                        };
                        Message = JsonConvert.SerializeObject(showMessage);
                        return RedirectToAction("CreateUsers");
                    }
                    else
                    {
                        var showMessage = new AlertMessage
                        {
                            Title = "USER CREATION",
                            Message = "User was not successfully Created!",
                            MessageType = MessageType.ErrorMessage
                        };
                        Message = JsonConvert.SerializeObject(showMessage);

                    }
                    return RedirectToAction("CreateUsers");
              
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public IActionResult UsersList()
        {
            var result = _adminFunc.GetUsersList();
            return View(result);
        }

        public IActionResult EditUser(long Id)
        {
            var result = _adminFunc.GetUser2Edit(Id);
            return View(result);
        }
        public async Task<IActionResult> PostEditUser(Users user, [FromForm]IFormFile ProfileImage)
        {
            try
            {
                var IsSuccessful = await _adminFunc.PostEditUsers(user, ProfileImage);

                if (IsSuccessful)
                {
                    var showMessage = new AlertMessage
                    {
                        Title = "EDIT USER",
                        Message = "User has been updated successfully!",
                        MessageType = MessageType.SuccessMessage
                    };
                    Message = JsonConvert.SerializeObject(showMessage);
                    return RedirectToAction("UsersList");
                }
                else
                {
                    var showMessage = new AlertMessage
                    {
                        Title = "EDIT USER",
                        Message = "User was not successfully updated!",
                        MessageType = MessageType.ErrorMessage
                    };
                    Message = JsonConvert.SerializeObject(showMessage);

                }
                return RedirectToAction("UsersList");

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