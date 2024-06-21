using Microsoft.EntityFrameworkCore;
using MovieRating.Core.Interfaces;
using MovieRating.Infrastructure;
using MovieRating.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configService = new ConfigService();

const string environmentNameApiKey = "API_KEY";
var environmentValueApiKey = configService.GetApiValue(environmentNameApiKey);

const string environmentNameDbUser = "DB_USER";
const string environmentNameDbPassword = "DB_PASSWORD";
const string environmentNameDbServer = "DB_SERVER";

var environmentValueDbUser = configService.GetDbValue(environmentNameDbUser);
var environmentValueDbPassword = configService.GetDbValue(environmentNameDbPassword);
var environmentValueDbServer = configService.GetDbValue(environmentNameDbServer);

builder.Services.AddDbContext<MovieContext>(options =>
{
    options.UseSqlServer($"Server={environmentValueDbServer};Database=Master;User Id={environmentValueDbUser};Password={environmentValueDbPassword};TrustServerCertificate=True;");
});

builder.Services.AddSingleton<IInfoService>(new InfoService(environmentValueApiKey));
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IMovieService, MovieService>();

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
