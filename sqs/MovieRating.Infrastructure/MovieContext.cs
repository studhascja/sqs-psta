using Microsoft.EntityFrameworkCore;
using MovieRating.Core.Models;

namespace MovieRating.Infrastructure;

/// <summary>
/// Class <c>MovieContext</c> represents the database context for the movie rating application.
/// </summary>
public sealed class MovieContext : DbContext
{
    /// <summary>
    /// Property <c>Movies</c> gets or sets the DbSet of <c>Movie</c> entities.
    /// </summary>
    public DbSet<Movie> Movies { get; set; }

    /// <summary>
    /// Property <c>Ratings</c> gets or sets the DbSet of <c>Rating</c> entities.
    /// </summary>
    public DbSet<Rating> Ratings { get; set; }

    /// <summary>
    /// Initializes a new instance of the <c>MovieContext</c> class with the specified options.
    /// Ensures that the database is created.
    /// </summary>
    /// <param name="options">The options to be used by a <c>DbContext</c>.</param>
    public MovieContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    /// <summary>
    /// Method <c>OnModelCreating</c> configures the entity mappings and relationships for the context.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasMany(movie => movie.Ratings)
            .WithOne(rating => rating.Movie)
            .OnDelete(DeleteBehavior.Cascade);
    }
}