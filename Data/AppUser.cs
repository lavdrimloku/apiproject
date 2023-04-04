using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data
{
    public class AppUser : IdentityUser, Privileges.IAuditTrail<string>
    {
        public bool active { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
