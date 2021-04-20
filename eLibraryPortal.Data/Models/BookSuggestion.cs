using System;
using System.Collections.Generic;
using System.Text;

namespace eLibraryPortal.Data.Models
{
    public class BookSuggestion
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public DateTime SuggestionDate { get; set; }
    }
}
