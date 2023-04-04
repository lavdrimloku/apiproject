using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Services.ProjectsServ;
using Services.StatusServ;
using Services.Users;


namespace Web.DI
{
    public static class DependencyConfig
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectTasksService, ProjectTasksService>();
            services.AddScoped<IStatusService, StatusService>();
        }
    }
}

