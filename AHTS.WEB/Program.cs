//using AHTS.DAL.Context;
using AHTS.BLL.Services;
using AHTS.DAL.Contexts;
using AHTS.DAL.Repos.Employee;
using AHTS.Core.Interfaces.DLL;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using AHTS.Core.Interfaces.BLL;
using AHTS.DAL.Repos.Asset;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// 1. Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext
builder.Services.AddDbContext<AhtsContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped<IemployeeRepo, employeeRepo>();
builder.Services.AddScoped<IAssetRepo, AssetRepo>();

// Register Services
builder.Services.AddScoped<IemployeeService, employeeService>();
builder.Services.AddScoped<IAssetService, assetService>();

var app = builder.Build();
app.UseStaticFiles();

app.UseCors("AllowFrontend");
// 2. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapSwagger("/openapi/{documentName}.json");
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
