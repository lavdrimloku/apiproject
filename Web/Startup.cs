using AutoMapper;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Repository;
using Services.Logs;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Web.DI;
using Web.Mapper;
using Web.Providers;

namespace OA_Web
{
    public class Startup
    {
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) //load base settings
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true) //load local settings
            //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true) //load environment settings
                 .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public class CustomSwaggerFilter : Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, Swashbuckle.AspNetCore.SwaggerGen.DocumentFilterContext context)
            {
                var nonMobileRoutes = swaggerDoc.Paths
                    .Where(x => !x.Key.ToLower().Contains("public"))
                    .ToList();
                nonMobileRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
            }
        }
        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

     

            services.AddSwaggerGen(c =>
            {
              //  c.DocumentFilter<CustomSwaggerFilter>();
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Tasks_Project", Version = "v2" });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer" 
                            }
                        },
                        new string[] {}
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);


            });

            services.AddHttpContextAccessor();
            services.AddTokenAuthentication(Configuration);
            var MaxFileLength = Configuration.GetValue<int>("MySettings:MaxFileLengthInMB");

            //  services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Documents")));
            services.Configure<FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = 1024 * 1024 * MaxFileLength;//1MB

            });

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 1024 * 1024 * MaxFileLength;//1MB;
            });
            services.Configure<Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 1024 * 1024 * MaxFileLength;//1MB;
            });


            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<ILog, LogNLog>();


            //services.AddDbContext<ApplicationContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());


            services.AddIdentity<AppUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
                       // services.AddDefaultIdentity<IdentityUser>()
                       .AddEntityFrameworkStores<ApplicationContext>()
                       .AddDefaultTokenProviders();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUserRepository<>), typeof(UserRepository<>));

            services.AddDependencies();

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                                                                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                                            ).AddRazorRuntimeCompilation();
            services.AddRazorPages();



            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            // Named Policy
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: MyAllowSpecificOrigins,
            //                      policy =>
            //                      {
            //                          policy.WithOrigins("http://localhost:3000",
            //                                              "*").AllowAnyHeader()
            //                                      .AllowAnyMethod();
            //                      });
            //});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILog logger)
        {
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "webapplication5");
                c.InjectStylesheet("/swagger/custom.css");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
               app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseFileServer(new FileServerOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Documents")),
            //    RequestPath = "/StaticFiles",
            //    EnableDirectoryBrowsing = true

            //});

            app.UseCors("AllowOrigin");
            app.UseCors(MyAllowSpecificOrigins); // allow credentials
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            // app.UseSession();

            //app.UseCors(x => x
            //.AllowAnyMethod()
            //.AllowAnyHeader()
            //.SetIsOriginAllowed(origin => true) // allow any origin
            //.AllowCredentials()); // allow credentials

         

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            app.UseRequestLocalization(options.Value);
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{lang:lang}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{*catchall}",
                    defaults: new { controller = "Home", action = "RedirectToDefaultLanguage", lang = "sq" }
                    );
                endpoints.MapRazorPages();
            });
        }
    }
}
