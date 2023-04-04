using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class User4TokenViewModel
    {
        public string Id { get; set; }
        public string userName { get; set; }
        public bool active { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string PasswordHash { get; set; }
    }
}
