using BLL.Abstract;
using BLL.Concrete;
using BLL.Mapper;
using DAL.Abstract;
using DAL.Concrete;
using DAL.DataBase;
using DAL.Extension;
using Entities.TableModels.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("localDb"));
            //});

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                
            });

            builder.Services.AddIdentity<AppUser, AppRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.AddScoped<IAccountService, AccountManager>();
            builder.Services.AddScoped<IAccountDAL, AccountDAL>();
            builder.Services.AddScoped<ICustomerService, CustomerManager>();
            builder.Services.AddScoped<ICustomerDAL, CustomerDAL>();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<Map>();
            });

            builder.Services.Configure<IdentityOptions>(cfg =>
            {
                cfg.Password.RequireDigit = true;
                cfg.Password.RequireUppercase = true;
                cfg.Password.RequireLowercase = true;
                cfg.Password.RequireNonAlphanumeric = true;

                cfg.User.RequireUniqueEmail = true;

                cfg.Lockout.MaxFailedAccessAttempts = 5;
                cfg.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 2, 0);
            });

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            builder.Services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/Auth/Login";

                cfg.ExpireTimeSpan = new TimeSpan(5, 0, 0);
                cfg.Cookie.Name = "Name";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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
                            name: "areas",
                            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");

            });

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedDatabaseUser.SeedMembership(app);
            }

            app.Run();
        }
    }
}
