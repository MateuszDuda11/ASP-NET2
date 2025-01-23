using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.Services;

namespace WebApplication1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddTransient<IContactService, EFContactService>();

        // Dodanie MoviesDbContext z SQLite
        builder.Services.AddDbContext<MoviesDbContext>(options => 
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}