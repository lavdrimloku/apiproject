using Microsoft.EntityFrameworkCore;
using Data;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Data.Privileges;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace Repository
{
    public class ApplicationContext : IdentityDbContext<IdentityUser, ApplicationRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyGlobalFilters<bool>("IsDeleted", false);
            seedData4UserAndRoles(modelBuilder);
        }

        /// <summary>
        /// insertimi per type dhe repost_type
        /// </summary>
        #region Seeds
        private static readonly string
            userAdminId = "9e0568ef-f131-4044-9bd9-8d49d186d278",
            administratorRoleId = "55660013-d101-427f-a62a-f6568b936af1";

        private static readonly Guid StationId = Guid.Parse("C4174FBF-86E8-43C2-850B-0AA659B6C3D6"),
            StationPointId = Guid.Parse("7B0D4B76-AD90-48C1-8DB9-D4984D0E3480");
        private static void seedData4UserAndRoles(ModelBuilder modelBuilder)
        {
            //System.Diagnostics.Debugger.Launch();
            string Roleadminid, adminid;
            Roleadminid = administratorRoleId;



            adminid = userAdminId;
            var user = new AppUser { Id = adminid, FirstName = "Admin", LastName = "ADMIN", Email = "admin@admin.com", NormalizedEmail = "admin@admin.com", UserName = "admin@admin.COM", NormalizedUserName = "admin@admin.com", PhoneNumber = "+111111111111", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D"), active = true };
            user.PasswordHash = (new PasswordHasher<AppUser>()).HashPassword(user, "123456");
            modelBuilder.Entity<AppUser>().HasData(user);

            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole { Name = "Administrator", NormalizedName = "Administrator", Id = Roleadminid, ConcurrencyStamp = adminid });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = adminid, RoleId = Roleadminid });


        }


        #endregion

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        //public override async Task SaveCHangesAsync()
        //{
        //    AddTimestamps();
        //    return await base.SaveChangesAsync();
        //}

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            // var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name) ? HttpContext.Current.User.Identity.Name : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = DateTime.Now;
                    ((BaseEntity)entity.Entity).LastChangedDate = DateTime.Now;
                    ((BaseEntity)entity.Entity).IsDeleted = false;
                    ((BaseEntity)entity.Entity).CreatedById = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                }
                else if (entity.State == EntityState.Modified)
                {
                    ((BaseEntity)entity.Entity).LastChangedDate = DateTime.Now;
                    ((BaseEntity)entity.Entity).LastChangedById = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                }


                // ((BaseEntity)entity.Entity).IsDeleted = false;
            }
        }
    }
    public static class ExtendionMethods
    {
        public static void ApplyGlobalFilters<T>(this ModelBuilder modelBilder, string propertyName, T value)
        {
            foreach (var entityType in modelBilder.Model.GetEntityTypes())
            {
                var foundProperty = entityType.FindProperty(propertyName);

                if (foundProperty != null && foundProperty.ClrType == typeof(T))
                {
                    var newParam = Expression.Parameter(entityType.ClrType);

                    var filter = Expression.
                        Lambda(Expression.Equal(Expression.Property(newParam, propertyName),
                        Expression.Constant(value)), newParam);

                    modelBilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }

            }
        }
    }
}
