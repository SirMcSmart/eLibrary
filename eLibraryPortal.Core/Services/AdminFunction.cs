using eLibraryPortal.Core.Interface;
using eLibraryPortal.Data.Context;
using eLibraryPortal.Data.Enums;
using eLibraryPortal.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibraryPortal.Core.Services
{
    public class AdminFunction : IAdminFunction
    {
        private readonly ApplicationDbContext _adc;
        private readonly IConfiguration _config;
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IRepository<Users> _UserRepo;

        public AdminFunction(ApplicationDbContext adc, IConfiguration config, UserManager<Users> userManager, RoleManager<UserRole> roleManager, IRepository<Users> UserRepo)
        {
            _adc = adc;
            _config = config;
            _userManager = userManager;
            _roleManager = roleManager;
            _UserRepo = UserRepo;
        }

        public List<BookSuggestion> GetBookSuggestions()
        {
            try
            {
                var bankDetails = (from a in _adc.BookSuggestions
                                   where a.BookName != null
                                   select a).ToList();
                return bankDetails;
            }
            catch (Exception ex)
            {
                throw new Exception("No record Found", ex);
            }
        }
        public async Task<bool> PostCreateUsers(Users user, IFormFile ProfileImage)
        {
            try
            {
                var DPassword = _config.GetValue<string>("eLibrary:DefaultPassword");
                if (!await _roleManager.RoleExistsAsync(user.UserRole.ToString()))
                {
                    throw new Exception(string.Format("Invalid User Role [{0}]", user.UserRole));
                }
                if (user.Id == 0)
                {
                    var eUser = new Users()
                    {
                        Title = user.Title,
                        UserName = user.Email,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        FullName = user.FirstName + " " + user.LastName,
                        Gender = user.Gender,
                        DateOfBirth = user.DateOfBirth,
                        UserRole = user.UserRole,
                        UserStatus = Status.Enable,
                        CreatedBy = " Nancy Bukola",
                        DateCreated = DateTime.Now,
                        Address = user.Address
                    };

                    if (ProfileImage != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        ProfileImage.CopyTo(ms);
                        eUser.ProfileImage = ms.ToArray();
                        ms.Close();
                        ms.Dispose();
                    }
                    var res = await _userManager.CreateAsync(eUser, DPassword);
                    if (!res.Succeeded)
                    {
                        var error = string.Empty;
                        foreach (var err in res.Errors)
                        {
                            error += err + " ";
                        }
                        return false;
                    }
                    await _userManager.AddToRoleAsync(eUser, user.UserRole.ToString());
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<Users> GetUsersList()
        {
            try
            {
                var UsersDetails = (from a in _adc.Users
                                   where a.Email != null
                                   select a).ToList();
                return UsersDetails;
            }
            catch (Exception ex)
            {
                throw new Exception("No record Found", ex);
            }
        }

        public Users GetUser2Edit(long Id)
        {
            try
            {
                var UsersDetails = (from a in _adc.Users
                                    where a.Id == Id && a.IsDeleted == false
                                    select a).FirstOrDefault();
                return UsersDetails;
            }
            catch (Exception ex)
            {
                throw new Exception("No record Found", ex);
            }
        }

        public async Task<bool> PostEditUsers(Users user, IFormFile ProfileImage)
        {
            try
            {
               
                if (!await _roleManager.RoleExistsAsync(user.UserRole.ToString()))
                {
                    throw new Exception(string.Format("Invalid User Role [{0}]", user.UserRole));
                }
                if (user.Id != 0)
                {
                    var resp = _userManager.FindByIdAsync(user.Id.ToString());
                    var eeUser = resp.Result;

                    if(eeUser == null)
                    {
                        throw new Exception("User does not exist");
                    }

                    eeUser.Title = user.Title;
                    eeUser.UserName = user.Email;
                    eeUser.Email = user.Email;
                    eeUser.PhoneNumber = user.PhoneNumber;
                    eeUser.FirstName = user.FirstName;
                    eeUser.LastName = user.LastName;
                    eeUser.FullName = user.FirstName + " " + user.LastName;
                    eeUser.Gender = user.Gender;
                    eeUser.DateOfBirth = user.DateOfBirth;
                    eeUser.UserRole = user.UserRole;
                    eeUser.UserStatus = Status.Enable;
                    eeUser.ModifiedBy = " Nancy Bukola";
                    eeUser.DateModified = DateTime.Now;
                    eeUser.Address = user.Address;

                    

                    if (ProfileImage != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        ProfileImage.CopyTo(ms);
                        eeUser.ProfileImage = ms.ToArray();
                        ms.Close();
                        ms.Dispose();
                    }

                    await _userManager.UpdateAsync(eeUser);
                    await _userManager.AddToRoleAsync(eeUser, user.UserRole.ToString());
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<bool> SaveBook(Book book, IFormFile BookImage, IFormFile FileAthachment)
        {
            try
            {
                var checkBookExist = (from a in _adc.Books where a.BookName == book.BookName select a).FirstOrDefault();

                if(checkBookExist == null)
                {
                    Book bookItem = new Book()
                    {
                        BookName = book.BookName,
                        BookEdition = book.BookEdition,
                        ISBN = book.ISBN,
                        BookAuthor = book.BookAuthor,
                        Categories = book.Categories,
                        PublishedDate = book.PublishedDate,
                        CreatedBy = "VICTOR SEUN",
                        CreatedDate = DateTime.Today,
                        IsDeleted = false
                    };

                    if (BookImage != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        BookImage.CopyTo(ms);
                        bookItem.BookImage = ms.ToArray();
                        ms.Close();
                        ms.Dispose();
                    }
                    if (FileAthachment != null)
                    {                       
                        MemoryStream mc = new MemoryStream();
                        FileAthachment.CopyTo(mc);
                        bookItem.FileAthachment = mc.ToArray();
                        mc.Close();
                        mc.Dispose();
                    }
                    _adc.Books.Add(bookItem);
                    await _adc.SaveChangesAsync();

                    //if (FileAthachment != null)
                    //{
                    //    int bookId = (from a in _adc.Books where a.BookName == book.BookName select a.Id).FirstOrDefault();
                    //    MemoryStream mc = new MemoryStream();
                    //    FileAthachment.CopyTo(mc);
                    //    bookItem.FileAthachment = mc.ToArray();
                    //    mc.Close();
                    //    mc.Dispose();
                    //}
                    return true;
                }
                return false;
            }                
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Book> GetBookList()
        {
            try
            {
                var UsersDetails = (from a in _adc.Books
                                    where a.BookName != null
                                    select a).ToList();
                return UsersDetails;
            }
            catch (Exception ex)
            {
                throw new Exception("No record Found", ex);
            }
        }

        public Book GetBook2Edit(int Id)
        {
            try
            {
                var UsersDetails = (from a in _adc.Books
                                    where a.Id == Id && a.IsDeleted == false
                                    select a).FirstOrDefault();
                return UsersDetails;
            }
            catch (Exception ex)
            {
                throw new Exception("No record Found", ex);
            }
        }

        public async Task<bool> PostEditBook(Book book, IFormFile BookImage, IFormFile FileAthachment)
        {
            try
            {
                var bookDetails = await (from a in _adc.Books where a.Id == book.Id && a.IsDeleted == false  select a).FirstOrDefaultAsync();
                //var resp = _adc.Books.FindAsync(book);

                bookDetails.BookName = book.BookName;
                bookDetails.BookEdition = book.BookEdition;
                bookDetails.ISBN = book.ISBN;
                bookDetails.BookAuthor = book.BookAuthor;
                bookDetails.Categories = book.Categories;
                bookDetails.PublishedDate = book.PublishedDate;
                bookDetails.ModifiedBy = "VICTOR SEUN 44";
                bookDetails.ModifiedDate = DateTime.Today;
                bookDetails.IsDeleted = book.IsDeleted;

                if (BookImage != null)
                {
                    MemoryStream ms = new MemoryStream();
                    BookImage.CopyTo(ms);
                    bookDetails.BookImage = ms.ToArray();
                    ms.Close();
                    ms.Dispose();
                }
                if (FileAthachment != null)
                {
                    MemoryStream mc = new MemoryStream();
                    FileAthachment.CopyTo(mc);
                    bookDetails.FileAthachment = mc.ToArray();
                    mc.Close();
                    mc.Dispose();
                }

                _adc.Books.Update(bookDetails);
               var res = _adc.SaveChanges();

                if(res == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
}
