//using AHTS.DAL.Context;
using AHTS.BLL.Services;
using AHTS.DAL.Contexts;
using AHTS.DAL.Repos.Employee;
using AHTS.Core.Interfaces.DLL;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using AHTS.Core.Interfaces.BLL;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext
builder.Services.AddDbContext<AhtsContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped<IemployeeRepo, employeeRepo>();

// Register Services
builder.Services.AddScoped<IemployeeService, employeeService>();

var app = builder.Build();

// 2. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapSwagger("/openapi/{documentName}.json");
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
