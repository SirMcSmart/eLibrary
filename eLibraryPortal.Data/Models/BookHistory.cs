using System;
using System.Collections.Generic;
using System.Text;

namespace eLibraryPortal.Data.Models
{
    public class BookHistory
    {
        public int Id { get; set; }
        public int bookId { get; set; }
        public int UserId { get; set; }
        public DateTime BookUploadDate { get; set; }
    }
}
