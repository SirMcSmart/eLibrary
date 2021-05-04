using eLibraryPortal.Core.Interface;
using eLibraryPortal.Data.Context;
using eLibraryPortal.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace eLibraryPortal.Core.Services
{
    public class AdminFunction : IAdminFunction
    {
        private readonly ApplicationDbContext _adc;

        public AdminFunction(ApplicationDbContext adc)
        {
            _adc = adc;
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
                if(FileAthachment != null)
                {
                    MemoryStream mc = new MemoryStream();
                    FileAthachment.CopyTo(mc);
                    bookItem.FileAthachment = mc.ToArray();
                    mc.Close();
                    mc.Dispose();
                }
                _adc.Books.Add(bookItem);
                await _adc.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
       


    }
}
