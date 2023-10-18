using AppRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore.Proxies;
using Models;
using Microsoft.AspNetCore.Identity;

namespace WebAppMVC
{
    public class Program
    {
        static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder();

            #region DI

            builder.Services.AddDbContext<ApplicationDBContext>(b =>
            {
                b.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<User, IdentityRole>(b =>
            {
                b.Lockout.MaxFailedAccessAttempts = 3;
                b.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                b.User.RequireUniqueEmail = true;
                b.SignIn.RequireConfirmedPhoneNumber = false;
                b.SignIn.RequireConfirmedEmail = false;
                b.SignIn.RequireConfirmedAccount = false;
                b.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(b =>
            {
                b.Password.RequireNonAlphanumeric = false;
                b.Password.RequireUppercase = false;
            });
            builder.Services.ConfigureApplicationCookie(b =>
            {
                b.LoginPath = "/Account/SignIn";

            });

            builder.Services.AddScoped(typeof(ApplicationDBContext));
            builder.Services.AddScoped(typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(CategoryRepo));
            builder.Services.AddScoped(typeof(ProductRepo));
            builder.Services.AddScoped(typeof(AccountRepo));
            builder.Services.AddScoped(typeof(RoleRepo));
            builder.Services.AddScoped(typeof(UserRepo));
            builder.Services.AddControllersWithViews();
            #endregion

            WebApplication app = builder.Build();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory() + "/wwwroot"),
                RequestPath = ""

            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute("Default", "{Controller=Home}/{Action=Index}/{id?}");

            app.Run();
        }
    }
}