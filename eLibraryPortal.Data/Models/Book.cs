using eLibraryPortal.Data.Enums;
using eLibraryPortal.Data.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace eLibraryPortal.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName     { get; set; }
        public string BookEdition { get; set; }
        public string ISBN { get; set; }
        public string BookAuthor { get; set; }
        public Category Categories { get; set; }
        public byte[] BookImage { get; set; }
        public byte[] FileAthachment   { get; set; }
        public DateTime PublishedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public string GetBookImage
        {
            get
            {
                if (BookImage != null && BookImage.Length > 0)
                    return ImageHelper.ConvertImage(BookImage);
                return string.Empty;
            }
        }

       
    }
}
