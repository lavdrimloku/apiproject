
using AutoMapper;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Services.DTOs;
using Services.JwtService;
using Services.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Areas.Identity.Pages.Account;
using Web.Models.Userss;

namespace Web.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : Controller
    {
        public readonly IConfiguration _configuration;
        public readonly IUsersService _usersService;
        public readonly IMapper _mapper;
        private readonly UserManager<Data.AppUser> _userManager;
        private readonly SignInManager<Data.AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public TokenController(IConfiguration configuration,
            IUsersService userService,
            UserManager<Data.AppUser> userManager,
            SignInManager<Data.AppUser> signInManager,
            ILogger<LoginModel> logger, IMapper mapper)
        {
            _configuration = configuration;
            _usersService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }


        //[HttpGet]
        //public IActionResult Get()
        //{
        //    InputModel _userData = new InputModel { userName = "admin@admin.com", Password = "123456" };

        //    if (_userData != null && _userData.userName != null && _userData.Password != null)
        //    {

        //        AppUser user = _usersService.FindByUserName(_userData.userName);

        //        if (user == null)
        //        {
        //            return BadRequest("Invalid credentials");
        //        }

        //        var jwt = new JwtService(_configuration);
        //        var token = "Bearer " + jwt.GenerateSecurityToken(user.Email);
        //        return Ok(new { token = token });

        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}


        [HttpGet]
        //  [Route("[controller]/param/{userName}/{password}")]
       // public IActionResult Get(ApiInputModel _userData)
        public IActionResult GetToken(  string userName, string password) 
        {

             InputModel _userData = new InputModel { userName = userName, Password = password };

            if (_userData != null && _userData.userName != null && _userData.Password != null)
            {
                var result = _signInManager.PasswordSignInAsync(_userData.userName, _userData.Password, true, lockoutOnFailure: false).Result;
                if (result.Succeeded)
                {
                    var user = _mapper.Map<User4TokenViewModel>(_usersService.FindByUserName(_userData.userName));
                    string roleName = _usersService.GetRoleIdByUserId(_usersService.FindByUserName(_userData.userName));
                    if (roleName == null)
                    {
                        return BadRequest("This user doesn't have role");
                    }
                    user.RoleId = _usersService.GetRoleIdByRoleName(roleName);
                    user.RoleName = roleName;

                    if (user == null)
                    {
                        return BadRequest("Invalid credentials");
                    }

                    if (user.active)
                    {
                        var jwt = new JwtService(_configuration);
                        var token = "Bearer " + jwt.GenerateSecurityToken(user);
                        return Ok(new { token = token });
                    }
                    else
                    {
                        return BadRequest("This user is not active!");
                    }
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost]
        //  [Route("[controller]/param/{userName}/{password}")]
        public IActionResult Get(ApiInputModel _userData)
       // public IActionResult Get([FromBody] string userName, string password) 
        {

           // InputModel _userData = new InputModel { userName = userName, Password = password };

            if (_userData != null && _userData.userName != null && _userData.password != null)
            {
                var result = _signInManager.PasswordSignInAsync(_userData.userName, _userData.password, true, lockoutOnFailure: false).Result;
                if (result.Succeeded)
                {
                    var user = _mapper.Map<User4TokenViewModel>(_usersService.FindByUserName(_userData.userName));
                    string roleName = _usersService.GetRoleIdByUserId(_usersService.FindByUserName(_userData.userName));
                    if (roleName == null)
                    {
                        return BadRequest("This user doesn't have role");
                    }
                    user.RoleId = _usersService.GetRoleIdByRoleName(roleName);
                    user.RoleName = roleName;

                    if (user == null)
                    {
                        return BadRequest("Invalid credentials");
                    }

                    if (user.active)
                    {
                        var jwt = new JwtService(_configuration);
                        var token = "Bearer " + jwt.GenerateSecurityToken(user);
                        return Ok(new { token = token });
                    }
                    else
                    {
                        return BadRequest("This user is not active!");
                    }
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
