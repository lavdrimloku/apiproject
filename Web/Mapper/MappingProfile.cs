using AutoMapper;
using Data;
using Data.Privileges;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repository;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Web.Models;
using Web.Models.ProjectsVM;
using Web.Models.ProjectTaksVM;
using Web.Models.StatusVM;
using Web.Models.Userss;

namespace Web.Mapper
{
    public class MappingProfile : Profile
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MappingProfile(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public MappingProfile()
        {

            // Add as many of these lines as you need to map your objects
            CreateMap<IdentityRole, RolesViewModel>().ForMember(d => d.Name, opt => opt.MapFrom(c => c.NormalizedName));
            CreateMap<ApplicationRole, RolesViewModel>().ForMember(d => d.Name, opt => opt.MapFrom(c => c.NormalizedName));

            CreateMap<AppUser, UsersViewModel>();
            CreateMap<AppUser, User4TokenViewModel>();
            CreateMap<AddUser, AppUser>();
            CreateMap<UpdateUser, AppUser>();
            CreateMap<ChangePasswordModel, AppUser>();
            CreateMap<IdentityRole, ApplicationRole>().ReverseMap();


            //Projects
            CreateMap<Project, ProjectViewModels>();
            CreateMap<AddProjectViewModels, Project>();
            CreateMap<UpdateProjectViewModels, Project>();   
            
            //ProjectsTasks
            CreateMap<ProjectTask, ProjectTaskViewModel>();
            CreateMap<AddProjectTaskViewModel, ProjectTask>();
            CreateMap<UpdateProjectTaskViewModel, ProjectTask>();

            //Status
            CreateMap<Status, StatusViewModel>();
            CreateMap<AddStatusViewModel, Status>();
            CreateMap<UpdateStatusViewModel, Status>();

        }
    }
}
