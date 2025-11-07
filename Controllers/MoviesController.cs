using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlessingMovies.Data;
using BlessingMovies.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BlessingMovies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MoviesController(ApplicationDbContext context) => _context = context;

        // GET: Movies
        public async Task<IActionResult> Index(string searchString, int? searchYear)
        {
            var movies = from m in _context.Movie select m;

            // Case-insensitive search
            if (!string.IsNullOrEmpty(searchString))
                movies = movies.Where(m => m.Title.ToLower().Contains(searchString.ToLower()));

            if (searchYear.HasValue)
                movies = movies.Where(m => m.Year >= searchYear.Value);

            ViewData["SearchString"] = searchString;
            ViewData["SearchYear"] = searchYear?.ToString();

            return View(await movies.ToListAsync());
        }

        // GET: Movies/Create
        public IActionResult Create() => View();

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Genre,Year,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Year,Rating")] Movie movie)
        {
            if (id != movie.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
