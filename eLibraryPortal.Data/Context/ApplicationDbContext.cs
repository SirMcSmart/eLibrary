using eLibraryPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eLibraryPortal.Data.Context
{
    public class ApplicationDbContext
    {
        public ApplicationDbContext() 
        {
                
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookAttachement> BookAttachements { get; set; }
        public DbSet<BookHistory> BookHistories { get; set; }
        public DbSet<BookSuggestion> BookSuggestions { get; set; }
    }
}
