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

        public IActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostCreateBook(Book book, IFormFile BookImage, IFormFile FileAthachment)
        {
            try
            {
                if (book.BookName != null || book.BookEdition != null)
                {

                    var status = await _adminFunc.SaveBook(book, BookImage, FileAthachment);
                    if (status)
                    {
                        //Successful
                        var showMessage = new AlertMessage
                        {
                            Title = "ADD BOOK MESSAGE",
                            Message = "Book has been added successfully!",
                            MessageType = MessageType.SuccessMessage
                        };
                        Message = JsonConvert.SerializeObject(showMessage);
                        return RedirectToAction("CreateBook");
                    }
                    else
                    {
                        //Not Successful
                        var showMessage = new AlertMessage
                        {
                            Title = "ADD BOOK MESSAGE",
                            Message = "Book was not successfully added!",
                            MessageType = MessageType.ErrorMessage
                        };
                        Message = JsonConvert.SerializeObject(showMessage);
                    }
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
            var result = _adminFunc.GetBookList();
            return View(result);
        }
        public IActionResult EditBook(int Id)
        {
            var result = _adminFunc.GetBook2Edit(Id);
            return View(result);
        }

        public async Task<IActionResult> PostEditBook(Book book, IFormFile BookImage, IFormFile FileAthachment)
        {
            try
            {
                if(book.BookName != null || book.BookEdition != null)
                {
                    var status = await _adminFunc.PostEditBook(book, BookImage, FileAthachment);

                    if (status)
                    {
                        //Successful
                        var showMessage = new AlertMessage
                        {
                            Title = "EDIT BOOK MESSAGE",
                            Message = "Book has been edited successfully!",
                            MessageType = MessageType.SuccessMessage
                        };
                        Message = JsonConvert.SerializeObject(showMessage);
                        return RedirectToAction("BookList");
                    }
                    else
                    {
                        //Not Successful
                        var showMessage = new AlertMessage
                        {
                            Title = "EDIT BOOK MESSAGE",
                            Message = "Book was not successfully edited!",
                            MessageType = MessageType.ErrorMessage
                        };
                        Message = JsonConvert.SerializeObject(showMessage);
                    }
                }

                return RedirectToAction("BookList");
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