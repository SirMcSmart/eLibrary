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
        Task<bool> PostCreateUsers(Users user, IFormFile ProfileImage);
        List<Users> GetUsersList();
        Users GetUser2Edit(long Id);
        Task<bool> PostEditUsers(Users user, IFormFile ProfileImage);
        Task<bool> SaveBook(Book book, IFormFile BookImage, IFormFile FileAthachment);

        List<BookSuggestion> GetBookSuggestions();
        Task<List<BookSuggestion>> GetBookSuggestions22();
    }
}
