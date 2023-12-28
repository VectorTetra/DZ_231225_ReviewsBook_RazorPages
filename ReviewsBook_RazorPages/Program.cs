using Microsoft.EntityFrameworkCore;
using ReviewsBook_RazorPages.Interfaces;
using ReviewsBook_RazorPages.Models;
using ReviewsBook_RazorPages.Repositories;

var builder = WebApplication.CreateBuilder(args);
// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<ReviewsContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped<IReviewsRepository, ReviewsRepository>();
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();

