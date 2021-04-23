using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eLibraryPortal.Data.Models
{
    public class UserRole : IdentityRole<long>
    {
        public string RoleName { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
