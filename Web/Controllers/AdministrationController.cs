using AutoMapper;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Enum;
using Services.Languages;
using Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Infrastructure;
using Web.Models;
using Web.Models.Userss;
using X.PagedList;
//using Microsoft.AspNet.Identity;

namespace Web.Controllers
{
    [Authorize]
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class AdministrationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILanguageService _languageService;
        private readonly IUsersService _usersService;
          
        public AdministrationController(IMapper mapper,
            ILanguageService languageService,
            IUsersService usersService 
            ) 
        {
            _mapper = mapper;
            _languageService = languageService;
            _usersService = usersService;
        }

        
        [Authorize(Roles = "Administrator")]
        [ClaimRequirement(Mode.CanView, ControllerEnum.Administration)]
        public IActionResult Index(int page = 1, string searchName = null)
        {
          

            return View( );

        }

 


       }
}