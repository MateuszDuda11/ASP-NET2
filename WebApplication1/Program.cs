using Microsoft.AspNetCore.Mvc;
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

        // Konfiguracja baz danych
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDbContext<MoviesDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Dodanie usług aplikacji
        builder.Services.AddTransient<IContactService, EFContactService>();

        // Walidacja i sesja
        builder.Services.AddRazorPages().AddMvcOptions(options =>
        {
            options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
        });

        builder.Services.AddSession();

        // Obsługa autoryzacji i uwierzytelniania
        builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options =>
        {
            options.LoginPath = "/Account/Login";
        });

        builder.Services.AddAuthorization();

        var app = builder.Build();

        // Automatyczne migracje baz danych
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MoviesDbContext>();
            dbContext.Database.Migrate();
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // Uwierzytelnianie i autoryzacja
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        // Mapowanie routingu
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
