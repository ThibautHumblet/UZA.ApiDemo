using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TestWebApi.Business.Employees;
using TestWebApi.Business.Employees.Interfaces;
using TestWebApi.Integration.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("Hospital");
Action<SqlServerDbContextOptionsBuilder>? optionBuilder;

optionBuilder = sqlOptions =>
{
    sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 10,
    maxRetryDelay: TimeSpan.FromSeconds(10),
        errorNumbersToAdd: null);
};

builder.Services.AddDbContext<HospitalDBContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServerOptionsAction: optionBuilder);
}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IEmployeeRetriever, EmployeeRetriever>();
builder.Services.AddScoped<IEmployeeModifier, EmployeeModifier>();
builder.Services.AddScoped<IEmployeeRemover, EmployeeRemover>();

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
