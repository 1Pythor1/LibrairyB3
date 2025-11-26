using App.Interfaces.Repository;
using App.Interfaces.Service;
using App.Services;
using Book.Models;
using DAL;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using WebApi.MiddleWare;
using static App.Services.StorieService;
using static DAL.Repository.ChoiceRepository;
using static WebApi.Controllers.PageController;

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = builder.Configuration.GetConnectionString("LibrairyDb");

// 1️ Ajouter le DbContext à la DI
builder.Services.AddDbContext<LibrairyContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// 2️ Ajouter ton repository
builder.Services.AddScoped<IStorieRepository, StorieRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<PageAddChoices>(provider =>
{
    IPageService pageRepo = provider.GetRequiredService<IPageService>();
    
    return (int id, Choice[] choices) => pageRepo.PageAddChoices(id, choices);
});
builder.Services.AddScoped<GetAllChoicesFromPage>(provider =>
{
    IChoiceRepository pageRepo = provider.GetRequiredService<IChoiceRepository>();

    return (int id) => pageRepo.GetAllChoicesFromPage(id);
});
builder.Services.AddScoped<GetPage>(provider =>
{
    IPageRepository pageRepo = provider.GetRequiredService<IPageRepository>();

    return (int id) => pageRepo.GetPage(id);
});
builder.Services.AddScoped<ChoiceService.GetPage>(provider =>
{
    IPageRepository pageRepo = provider.GetRequiredService<IPageRepository>();

    return (int id) => pageRepo.GetPage(id);
});
builder.Services.AddScoped<IChoiceRepository, ChoiceRepository>();
builder.Services.AddScoped<IStorieService, StorieService>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<IChoiceService, ChoiceService>();

// Add controllers and swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandler>();

// Swagger dev only
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
