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
        Task<bool> SaveBook(Book book, IFormFile BookImage, IFormFile FileAthachment);
    }
}
