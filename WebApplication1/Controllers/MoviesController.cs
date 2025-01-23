using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using X.PagedList;
using X.PagedList.Mvc.Core;
public class MoviesController : Controller
{
    private readonly AppDbContext _context;

    public MoviesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? page)
    {
        int pageSize = 20;  // Liczba elementów na stronie
        int pageNumber = page ?? 1;  // Domyślnie strona 1

        var movies = await _context.Movies.ToListAsync();
        return View(movies.ToPagedList(pageNumber, pageSize));
    }
}