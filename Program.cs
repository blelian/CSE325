using BlessingMovies.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("MoviesDB"));

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!context.Movie.Any())
    {
        context.Movie.AddRange(
            new BlessingMovies.Models.Movie { Title = "Inception", Genre = "Sci-Fi", Year = 2010, Rating = 8.8M },
            new BlessingMovies.Models.Movie { Title = "The Matrix", Genre = "Action", Year = 1999, Rating = 8.7M },
            new BlessingMovies.Models.Movie { Title = "Interstellar", Genre = "Sci-Fi", Year = 2014, Rating = 8.6M }
        );
        context.SaveChanges();
    }
}

// Configure pipeline
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
