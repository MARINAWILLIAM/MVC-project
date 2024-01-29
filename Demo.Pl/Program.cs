using Demo.BLL;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Demo.Pl.Helpers;
using Demo.Pl.Mapping_Profiles;
using Demo.Pl.Settings;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Pl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);//create kestral
            #region configure services that allow dependency injection
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            //         services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<IdentityUser>>();

            builder.Services.AddScoped<IunitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IEmailSettings, Emailsettings>();
            builder.Services.AddTransient<ISmsServices, SmsService>();
            builder.Services.AddDbContext<MVCAppG04DbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
               
            }, ServiceLifetime.Transient);
          






            builder.Services.AddAutoMapper(m => m.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(m => m.AddProfile(new RoleProfiler()));
            builder.Services.AddAutoMapper(m => m.AddProfile(new UserProfiler()));
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<MVCAppG04DbContext>()
               .AddDefaultTokenProviders();
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSetting"));
            builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twillio"));
			
			builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "Account/Login";
                    options.AccessDeniedPath = "Home/Error";
                });
			//builder.Services.AddAuthentication(m =>
			//{
			//	m.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
			//	m.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
			//}).AddGoogle(o => {
			//	IConfiguration GoogleAuthSection = builder.Configuration.GetSection("Authentcation:Google");
			//	o.ClientId = GoogleAuthSection["ClientId"];
			//	o.ClientSecret = GoogleAuthSection["ClientSecret"];
			//});
			#endregion
			//make application built and make appl
			var app =builder.Build();
            //hmsak elapp dah w ahot medile ware bt3ati
            #region Configure Http Request PipeLine
            if (app.Environment.IsDevelopment())
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

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();






            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
            #endregion
            app.Run();
        }

        
    }
}
