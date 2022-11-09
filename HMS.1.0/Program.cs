using Hms.Models;
using Hms.Service;
using HMS.Data;
using HMS.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration= builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<HmsContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("HMSdB"));
});
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<HmsContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITableService,TableService>();
builder.Services.AddScoped<IDishCategoryService, DishCategoryService>();
builder.Services.AddScoped<IDishService, DishService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
