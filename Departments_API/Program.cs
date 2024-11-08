using Departmens_DAL;
using Departments_BLL.Interfaces;
using Departments_BLL.Services;
using Departments_DAL.Interfaces;
using Departments_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);

//DB
builder.Services.AddDbContext<DepartmentsContext>(options =>
       options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

//DI
builder.Services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
builder.Services.AddScoped<IDepartmentsService, DepartamentsService>();




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
