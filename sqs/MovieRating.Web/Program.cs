using Microsoft.EntityFrameworkCore;
using MovieRating.Core.Exceptions;
using MovieRating.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string environmentNameApiKey = "API_KEY";
var environmentValueApiKey = Environment.GetEnvironmentVariable(environmentNameApiKey);
if (environmentValueApiKey == null) throw new EnvironmentVariableNotSetException("API-Key not set correctly");

const string environmentNameDbUser = "DB_USER";
const string environmentNameDbPassword = "DB_PASSWORD";
const string environmentNameDbServer = "DB_SERVER";

var environmentValueDbUser = Environment.GetEnvironmentVariable(environmentNameDbUser);
var environmentValueDbPassword = Environment.GetEnvironmentVariable(environmentNameDbPassword);
var environmentValueDbServer = Environment.GetEnvironmentVariable(environmentNameDbServer);
if (environmentValueDbServer == null || environmentValueDbPassword == null || environmentValueDbUser == null)
{
    throw new EnvironmentVariableNotSetException("DB-Environment-Variables not set correctly");
}

builder.Services.AddDbContext<MovieContext>(options =>
{
    options.UseSqlServer($"Server={environmentValueDbServer};Database=Master;User Id={environmentValueDbUser};Password={environmentValueDbPassword};TrustServerCertificate=True;");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();

public partial class Program;
