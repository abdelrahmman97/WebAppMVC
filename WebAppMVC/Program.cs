using AppRepository;
using Microsoft.Extensions.FileProviders;
using Models;

namespace WebAppMVC
{
    public class Program
    {
        static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder();

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped(typeof(ApplicationDBContext));
            builder.Services.AddScoped(typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(CategoryRepo));
            builder.Services.AddScoped(typeof(ProductRepo));

            WebApplication app = builder.Build();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory() + "/wwwroot"),
                RequestPath = ""

            });

            app.MapControllerRoute("Default", "{Controller=Home}/{Action=Index}/{id?}");

            app.Run();
        }
    }
}