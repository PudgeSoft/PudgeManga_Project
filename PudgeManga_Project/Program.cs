
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
using PudgeManga_Project.Models.Repositories;
using RunGroopWebApp.Repository;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// register dependency injection with AddScoped method for manga model
builder.Services.AddScoped<IAdminMangaRepository<Manga, int>, AdminMangaRepository>();
builder.Services.AddScoped<IAdminChapterRepository<Chapter, int>, AdminChapterRepository>();
builder.Services.AddScoped<IMangaRepository<Manga, int>, MangaRepository>();
builder.Services.AddScoped<IChapterRepository<Chapter, int>, ChapterRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();


builder.Services.AddScoped<IGoogleDriveAPIRepository<IFormFile>, GoogleDriveAPIRepository>();

builder.Services.AddScoped<IRatingForMangaRepository, RatingForMangaRepository>();
builder.Services.AddScoped<IRatingForAnimeRepository, RatingForAnimeRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ISearchRepository, SearchRepository>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<IAdminAnimeRepository<Anime, int>, AdminAnimeRepository>();
builder.Services.AddScoped<IAnimeGenreRepository, AnimeGenreRepository>();
builder.Services.AddScoped<IAnimeRepository<Anime, int>, AnimeRepository>();
builder.Services.AddScoped<IAdminSeasonRepository<AnimeSeason, int>, AdminSeasonRepository>();
builder.Services.AddScoped<IAnimeSeasonsRepository<AnimeSeason, int>, AnimeSeasonsRepository>();
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie();

builder.Services.AddSingleton<Seed>();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureConnection"));
});

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    await Seed.SeedUsersAndRolesAsync(app);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Manga",
    pattern: "{controller}/{action}/{id?}/{chapter?}/{page?}");


app.Run();
