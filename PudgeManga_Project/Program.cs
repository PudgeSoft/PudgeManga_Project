
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
using PudgeManga_Project.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// register dependency injection with AddScoped method for manga model
builder.Services.AddScoped<IAdminMangaRepository<Manga, int>, AdminMangaRepository>();
builder.Services.AddScoped<IAdminChapterRepository<Chapter, int>, AdminChapterRepository>();
builder.Services.AddScoped<IMangaRepository<Manga, int>, MangaRepository>();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IChapterRepository<Chapter, int>, ChapterRepository>();
builder.Services.AddScoped<IGoogleDriveAPIRepository<IFormFile>, GoogleDriveAPIRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

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

app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Manga",
    pattern: "{controller}/{action}/{id?}/{chapter?}/{page?}");


app.Run();
