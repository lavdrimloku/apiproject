using AutoMapper;
using Data;
using Data.Privileges;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services.Privileges;
using Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Infrastructure;
using Web.Models.RolesAndPrivileges;

namespace Web.Controllers
{

    [Authorize(Roles = "Administrator")]
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class RolesController : BaseController
    {
        private readonly IUsersService _userService;
        private readonly IActionRootService _actionRootService;
        private readonly IUserActionRootRestRightService _userActionRootRestRightService;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        public RolesController(IUserActionRootRestRightService userActionRootRestRightService, IActionRootService actionRootService, IUsersService userService, IMapper mapper, IUsersService usersService)
        {
            _userActionRootRestRightService = userActionRootRestRightService;
            _actionRootService = actionRootService;
            _userService = userService;
            _mapper = mapper;
            _usersService = usersService;
        }


        public IActionResult Index()
        {

            if (!User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }

            GetIdentityRolesViewModel model = new GetIdentityRolesViewModel();

            var Roles = _userService.GetAllRoles();

            model.Roles = _mapper.Map<List<GetIdentityRoleViewModel>>(Roles);//krejt rolet


            return View(model);
        }

        public ActionResult Create()
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }

            var Role = new Microsoft.AspNetCore.Identity.IdentityRole();
            return View(Role);
        }
        [HttpPost]
        public ActionResult Create(ApplicationRole role)
        {
            var sharedRes = (Dictionary<string, string>)ViewData["Shared"];

            if (!User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }

            ApplicationRole existsRole = _usersService.GetRoleByName(role.Name);

            if (existsRole != null && existsRole.Name.ToLower().Equals(role.Name.ToLower()))
            {
                ViewBag.Error = $"{sharedRes["NotChangeThisRole"]} - {existsRole.Name}!";
                return View(existsRole);
            }

            bool isAdded = _userService.AddRole(role);

            if (!isAdded)
            {
                ViewBag.Error = $"{sharedRes["NotChangeThisRole"]} - {existsRole.Name}!";
                return View(existsRole);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(ApplicationRole Role)
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }

            var rez = _userService.GetRoleById(Role.Id.ToString());

            if (rez.Name.Equals("Administrator"))
            {
                ViewBag.Error = "Nuk mund te ndryshohet Roli Administrator!";
                return View("Edit", rez);
            }


            rez.Name = Role.Name;

            bool isUpdated = _userService.UpdateRole(rez);
            if (!isUpdated)
            {
                ViewBag.Error = "Nuk mund te ndryshohet ky role!";
                return View("Edit", rez);
            }

            return RedirectToAction("Index");
        }


        public ActionResult Edit(Guid Id)
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }

            var rez = _userService.GetRoleById(Id.ToString());

            return View(rez);
        }
        [HttpGet]
        public ActionResult Privilege(string Id)
        {

            if (!User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }
            GetPrivilegesViewModel model = new GetPrivilegesViewModel();
            var complaintTabs = this._actionRootService.GetAll();

            List<GetUserActionRootRestRightViewModel> RoleComplaintTabdb = _mapper.Map<List<GetUserActionRootRestRightViewModel>>(complaintTabs);

            var RoleRights = _userActionRootRestRightService.GetAll(Id).ToList();//privileges by RoleId
            var RoleName = _userService.GetRoleByUserId(Id);

            RoleComplaintTabdb.ForEach(u =>
            {
                var roletab = RoleRights.Where(x => x.ActionRootId == u.Id).FirstOrDefault();
                if (roletab != null)
                {
                    u.CanRead = roletab.CanRead;
                    u.CanWrite = roletab.CanWrite;
                    u.CanDelete = roletab.CanDelete;
                }
            });

            model.RoleId = Id;
            model.RoleName = RoleName != null ? RoleName.Name : "";
            model.ActionRoots = RoleComplaintTabdb;

            return View(model);
        }
        [HttpPost]
        public ActionResult Privilege(GetPrivilegesViewModel model)
        {
            List<UserActionRootRestRight> userComplaintTabRights = _mapper.Map<List<UserActionRootRestRight>>(model.ActionRoots);

            var UserComplaintTabRightDb = _userActionRootRestRightService.GetAll(model.RoleId);
            if (UserComplaintTabRightDb == null || UserComplaintTabRightDb.Count() == 0)
            {
                userComplaintTabRights.ForEach(x =>
                {
                    x.IdentityRoleId = model.RoleId;
                });

                _userActionRootRestRightService.InsertRange(userComplaintTabRights);
            }
            else
            {
                MappViewModelToModel(model, UserComplaintTabRightDb);
                _userActionRootRestRightService.UpdateRange(UserComplaintTabRightDb);
            }

            _userActionRootRestRightService.SaveChanges();

            return RedirectToAction("Privilege", new { Id = model.RoleId });
        }

        private static void MappViewModelToModel(GetPrivilegesViewModel model, IQueryable<UserActionRootRestRight> UserComplaintTabRightDb)
        {
            foreach (var UserComplaintTabDb in UserComplaintTabRightDb)
            {
                var UserComplaintTabViewModel = model.ActionRoots.Where(x => x.ActionRootId == UserComplaintTabDb.ActionRootId).FirstOrDefault();

                if (UserComplaintTabViewModel == null)
                    continue;

                UserComplaintTabDb.CanRead = UserComplaintTabViewModel.CanRead;
                UserComplaintTabDb.CanWrite = UserComplaintTabViewModel.CanWrite;
                UserComplaintTabDb.CanDelete = UserComplaintTabViewModel.CanDelete;
            }
        }
    }
}
