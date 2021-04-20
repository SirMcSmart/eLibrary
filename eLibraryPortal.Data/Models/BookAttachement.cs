using System;
using System.Collections.Generic;
using System.Text;

namespace eLibraryPortal.Data.Models
{
    public class BookAttachement
    {
        public int Id { get; set; }
        public int bookId { get; set; }
        public string BookDesc    { get; set; }
        public string BookName { get; set; }
        public string BookExt { get; set; }
        public string BookFilePath { get; set; }
        public string BookNameUrl { get; set; }
        public string uploadedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        //To be deleted
        public string BookDesc1 { get; set; }
        public string BookName1 { get; set; }
    }
}
