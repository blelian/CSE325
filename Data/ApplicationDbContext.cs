using Microsoft.EntityFrameworkCore;
using BlessingMovies.Models;

namespace BlessingMovies.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = null!;
    }
}
