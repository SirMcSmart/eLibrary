using eLibraryPortal.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eLibraryPortal.Core.Interface
{
    public interface IAdminFunction
    {
        List<BookSuggestion> GetBookSuggestions();
        Task<bool> PostCreateUsers(Users user, IFormFile ProfileImage);
        List<Users> GetUsersList();
        Users GetUser2Edit(long Id);
        Task<bool> PostEditUsers(Users user, IFormFile ProfileImage);
        Task<string> SaveBook(Book book, IFormFile BookImage, IFormFile FileAthachment);
        List<Book> GetBookList();
        Book GetBook2Edit(int Id);

        Task<bool> PostEditBook(Book book, IFormFile BookImage, IFormFile FileAthachment);



    }
}
