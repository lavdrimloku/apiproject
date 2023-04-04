using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.Userss
{
    public class IndexUsersViewModel
    {
        public List<RolesViewModel> Roles { get; set; }
        public List<UserAdminRowViewModel> Users { get; set; }

    }

    public class ApiInputModel
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class UsersViewModel
    {
        public string Id { get; set; }
        public string userName { get; set; }
        public bool active { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string UserPassword { get; set; }
        public string RoleId { get; set; }
    }


    public class UserAdminRowViewModel
    {
        public string Id { get; set; }
        public string userName { get; set; }
        public bool active { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string RoleName { get; set; }
        public bool IsTerrain { get; set; }
        public string Lang { get; set; }

    }
    public class AddUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool active { get; set; }
        public string UserPassword { get; set; }
        public int DefaultLanguageId { get; set; }
        public List<string> _Roles { get; set; }

    }
    public class AddApiUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool active { get; set; }
        public string UserPassword { get; set; }
        public string RoleName { get; set; }
    }

    public class UpdateUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool active { get; set; }
        public string UserPassword { get; set; }
        public List<string> _Roles { get; set; }
    }

    public class UpdateApiUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool active { get; set; }
        public string RoleName { get; set; }

    }

    public class ChangePasswordModel
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmNewPassword { get; set; }
    }


    public class RolesViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }


}
