using Microsoft.EntityFrameworkCore;
using MovieRating.Core.Exceptions;
using MovieRating.Core.Models;

namespace MovieRating.Infrastructure;

public class MovieContext : DbContext
{
    public static readonly string EnvironmentNameDbUser = "DB_USER";
    public static readonly string EnvironmentNameDbPassword = "DB_PASSWORD";
    public static readonly string EnvironmentNameDbServer = "DB_Server";

    private readonly string? _environmentValueDbUser = Environment.GetEnvironmentVariable(EnvironmentNameDbUser);
    private readonly string? _environmentValueDbPassword = Environment.GetEnvironmentVariable(EnvironmentNameDbPassword);
    private readonly string? _environmentValueDbServer = Environment.GetEnvironmentVariable(EnvironmentNameDbServer);
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_environmentValueDbServer == null || _environmentValueDbPassword == null || _environmentValueDbUser == null) throw new EnvironmentVariableNotSetException("DB-Environment-Variables not set correctly");
        var server = _environmentValueDbServer; 
        var user = _environmentValueDbUser; 
        var password = _environmentValueDbPassword; 

        optionsBuilder.UseSqlServer(
            $"Server={server};Database=Master;User Id={user};Password={password};TrustServerCertificate=True;");
    }
}