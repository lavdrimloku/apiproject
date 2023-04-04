using Data;
using Microsoft.AspNetCore.Identity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    public interface IUsersService
    {
        AppUser GetById(string Id);
        List<AppUser> GetByIds(List<string> Ids);
        Task<AppUser> FindByName(string UserName);
        AppUser FindByUserName(string Username);
        //Task<AppUser> FindByEmail(string Email);
        AppUser FindByEmail(AppUser user);

        Task<AppUser> FindByEmail(string email);

        List<AppUser> GetAllUsers();
        List<AppUser> GetAllUsersByUsername(string username = "");
        bool Create(AppUser AppUser);
        bool Update(AppUser appUser);

        bool ChangePassword(AppUser user, string currentPassword, string newPassword);

        bool AddToRoles(AppUser user, List<string> roles);
        bool AddToRole(AppUser user, string role);
        IEnumerable<ApplicationRole> GetAllRoles();
        ApplicationRole GetRoleByUserId(string UserId);
        string GetRoleNameByUserId(string UserId);
        ApplicationRole GetRoleByRoleName(string RoleName);
        bool IsInRole(AppUser user, string roleName);
        bool RemoveFromRoles(AppUser user, string role);
        List<string> GetRolesByUserId(AppUser user);

        string GetRoleByUserId(AppUser user);
        IQueryable<AppUser> GetAllAsQuerable(string searchName);

        bool AddRole(ApplicationRole Role);
        bool UpdateRole(ApplicationRole Role);
        ApplicationRole GetRoleById(string Id);
        ApplicationRole GetRoleByName(string Id);
        string GetRoleIdByUserId(AppUser user);
        string GetRoleIdByRoleName(string RoleName);

    }
}
