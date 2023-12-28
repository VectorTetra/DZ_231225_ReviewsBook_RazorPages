using Microsoft.EntityFrameworkCore;
using ReviewsBook_RazorPages.Interfaces;
using ReviewsBook_RazorPages.Models;
using ReviewsBook_RazorPages.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<ReviewsContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped<IReviewsRepository, ReviewsRepository>();
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();

