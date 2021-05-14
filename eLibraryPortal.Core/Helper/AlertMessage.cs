using eLibraryPortal.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eLibraryPortal.Core.Helper
{
    public class AlertMessage
    {
        public MessageType MessageType { get; set; }
        [StringLength(100)]
        public string Message { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
    }
}
