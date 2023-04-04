using AutoMapper;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models.Userss;

namespace Web.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/users")]
    [ApiController]
    public class ApiUsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public ApiUsersController(IMapper mapper, IUsersService usersService)
        {
            _mapper = mapper;
            _usersService = usersService;
        }


        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            try
            {
                var parameters = _mapper.Map<UsersViewModel>(_usersService.GetById(Id.ToString()));
                return Ok(parameters);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }

        }
        [HttpGet()]

        public IActionResult Get()
        {
            try
            {
                var allUsers = _usersService.GetAllUsers();
                List<UserAdminRowViewModel> models = new List<UserAdminRowViewModel>();
                allUsers.ToList().ForEach(u =>
                {
                    UserAdminRowViewModel ua = new UserAdminRowViewModel()
                    {
                        Id = u.Id,
                        email = u.Email,
                        userName = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        active = u.active,
                        RoleName = this._usersService.GetRolesByUserId(u).Count() > 0 ? this._usersService.GetRolesByUserId(u)[0].ToString() : "",
                    };
                    models.Add(ua);
                });

                return Ok(models);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }

        }


        [HttpPost]
        [Route("add")]
        public IActionResult Add(AddApiUser model)
        {
            try
            {

                if (this._usersService.FindByUserName(model.UserName) != null)
                {
                    return BadRequest("THIS USER EXISTS!");
                }

                if (this._usersService.FindByEmail(model.Email).Result != null)
                {
                    return BadRequest("THIS EMAIL EXISTS!");
                }

                var mappedState = _mapper.Map<AppUser>(model);
                mappedState.PasswordHash = (new PasswordHasher<AppUser>()).HashPassword(mappedState, model.UserPassword);
                mappedState.NormalizedUserName = model.UserName;
                mappedState.NormalizedEmail = model.Email;
                mappedState.PhoneNumberConfirmed = true;
                mappedState.EmailConfirmed = true;
                _usersService.Create(mappedState);
                _usersService.AddToRole(mappedState, model.RoleName);
                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(UpdateApiUser model)
        {
            try
            {
                var user = _usersService.FindByUserName(model.UserName);

                if (user == null)
                {
                    return BadRequest("USER DOES NOT EXIST!");
                }

                var mappedState = _mapper.Map<UpdateApiUser, AppUser>(model, user);
                _usersService.Update(mappedState);

                string roleName = this._usersService.GetRoleIdByUserId(this._usersService.FindByUserName(mappedState.UserName));

                if (!string.IsNullOrEmpty(model.RoleName))
                {
                    if (roleName != null)
                    {
                        this._usersService.RemoveFromRoles(mappedState, roleName);
                    }
                    this._usersService.AddToRole(mappedState, model.RoleName);
                }
                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }


        [HttpGet]
        [Route("roles")]
        public IActionResult Roles()
        {
            var model = _mapper.Map<List<RolesViewModel>>(_usersService.GetAllRoles());
            return Ok(model);
        }


        [HttpPost]
        [Route("changepassword")]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (model.oldPassword != null && model.newPassword != null && model.confirmNewPassword != null)
            {
                var user = _usersService.GetById(userid);

                var checkOldPasswordHash = (new PasswordHasher<AppUser>()).VerifyHashedPassword(user, user.PasswordHash, model.oldPassword).ToString().ToLower();

                if (checkOldPasswordHash != "success")
                {
                    return BadRequest("Old password is incorrect!");
                }

                if (model.oldPassword.Trim() == model.newPassword.Trim())
                {
                    return BadRequest("Cannot usign old password!");
                }

                if (model.newPassword.Trim() != model.confirmNewPassword.Trim())
                {
                    return BadRequest("Pasword doesn't match!");
                }

                if (user != null)
                {
                    user.PasswordHash = (new PasswordHasher<AppUser>()).HashPassword(user, model.newPassword);
                    var result = _usersService.Update(user);
                    if (result)
                    {

                        return Ok("Account password has been changed successfully!");
                    }
                    else
                    {
                        return BadRequest("Something went wrong!");
                    }
                }
            }
            return BadRequest();
        }
    }
}
