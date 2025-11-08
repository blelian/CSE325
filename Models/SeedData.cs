using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using BlessingMovies.Data;
using BlessingMovies.Models;

namespace BlessingMovies.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (context.Movie.Any())
                return;

            context.Movie.AddRange(
                new Movie { Title = "Inception", Genre = "Sci-Fi", Year = 2010, Rating = 8.8M },
                new Movie { Title = "The Matrix", Genre = "Action", Year = 1999, Rating = 8.7M },
                new Movie { Title = "Interstellar", Genre = "Sci-Fi", Year = 2014, Rating = 8.6M }
            );

            context.SaveChanges();
        }
    }
}
