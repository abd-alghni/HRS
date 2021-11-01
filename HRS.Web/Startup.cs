using HRS.Infrastructure.Services;
using HRS.Data;
using HRS.Data.Models;
using HRS.Infrastructure.AutoMapper;
using HRS.Infrastructure.Services.Employees;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using HRS.Infrastructure.Middlewares;
using HRS.Infrastructure.Services.Departments;
using HRS.Infrastructure.Services.Holidays;
using HRS.Infrastructure.Services.Tasks;
using HRS.Infrastructure.Services.Salaries;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.IO;

namespace HRS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HRSDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<Employee, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequireDigit = false;
                config.Password.RequiredLength = 6;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<HRSDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

            services.AddRazorPages();

            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IHolidayService, HolidayService>();
            services.AddTransient<ITaaskService, TaaskService>();
            services.AddTransient<ISalaryService, SalaryService>();
            services.AddTransient<IDashboardService, DashboardService>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseExceptionHandler(opts => opts.UseMiddleware<ExceptionHandler>());

            //FirebaseApp.Create(new AppOptions()
            //{
            //    Credential = GoogleCredential.FromFile(Path.Combine(env.WebRootPath, "hrsweb-9e2c4-firebase-adminsdk-9d1u6-7f100d9a0a.json"))
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
