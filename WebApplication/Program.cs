using AppRepository;
using Models;

namespace WebAppMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped(typeof(ApplicationDBContext));
            builder.Services.AddScoped(typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(ProductRepo));
            builder.Services.AddScoped(typeof(CategoryRepo));

            var app = builder.Build();

            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}