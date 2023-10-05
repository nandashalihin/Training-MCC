using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;
using BookingManagementApp.Utilities.Handlers;
using BookingManagementApp.Utilities.Validations.Employees;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookingManagementDbContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddScoped<IRepository<University>, UniversityRepository>();
builder.Services.AddScoped<IRepository<Room>, RoomRepository>();
builder.Services.AddScoped<IRepository<Role>, RoleRepository>();
builder.Services.AddScoped<IRepository<Account>, AccountRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRepository<Booking>, BookingRepository>();
builder.Services.AddScoped<IRepository<AccountRole>, AccountRoleRepository>();
builder.Services.AddScoped<IRepository<Education>, EducationRepository>();

builder.Services.AddControllers()
       .ConfigureApiBehaviorOptions(options =>
       {
           // Custom validation response
           options.InvalidModelStateResponseFactory = context =>
           {
               var errors = context.ModelState.Values
                                   .SelectMany(v => v.Errors)
                                   .Select(v => v.ErrorMessage);
               return new BadRequestObjectResult(new ResponseValidatorHandler(errors));
           };
       });

builder.Services.AddFluentValidationAutoValidation()
       .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
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
