using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ApplicationRole : IdentityRole<string>, Data.Privileges.IAuditTrail<string>
    {
        public ApplicationRole()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
