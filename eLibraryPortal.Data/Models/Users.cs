using eLibraryPortal.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eLibraryPortal.Data.Models
{
    public class Users : IdentityUser<long>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public byte[] ProfileImage { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Role UserRole { get; set; }
        public Status UserStatus { get; set; }
    }
}
