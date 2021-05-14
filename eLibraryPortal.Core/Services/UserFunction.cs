
using eLibraryPortal.Core.Interface;
using eLibraryPortal.Data.Context;
using eLibraryPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eLibraryPortal.Core.Services
{
    public class UserFunction : IUserFunction
    {
        private readonly ApplicationDbContext _adc;

        public UserFunction(ApplicationDbContext adc)
        {
            _adc = adc;
        }

        public bool CreateBookSuggestion(BookSuggestion bookSuggestion)
        {
            try
            {
                if (bookSuggestion != null)
                {
                    BookSuggestion bs = new BookSuggestion
                    {
                        FullName = "Bill Gates",
                        Email = "victor@gmail",
                        BookName = bookSuggestion.BookName,
                        BookAuthor = bookSuggestion.BookAuthor,
                        SuggestionDate = DateTime.Today
                    };
                    _adc.BookSuggestions.Add(bs);
                     _adc.SaveChanges();

                    return true;
                }


                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
