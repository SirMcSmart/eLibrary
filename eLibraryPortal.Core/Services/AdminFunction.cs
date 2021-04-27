using eLibraryPortal.Core.Interface;
using eLibraryPortal.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eLibraryPortal.Core.Services
{
    public class AdminFunction : IAdminFunction
    {
        public AdminFunction()
        {

        }

        public async Task<bool> SaveBook(Book book, IFormFile BookImage, IFormFile FileAthachment)
        {
            try
            {
                Book bookItem = new Book()
                {
                    BookName = book.BookName,
                    BookEdition = book.BookEdition,
                    ISBN = book.ISBN,
                    BookAuthor = book.BookAuthor,
                    Categories = book.Categories,


                };

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
       


    }
}
