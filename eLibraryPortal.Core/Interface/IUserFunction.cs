using eLibraryPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eLibraryPortal.Core.Interface
{
    public interface IUserFunction
    {
        bool CreateBookSuggestion(BookSuggestion bookSuggestion);
    }
}
