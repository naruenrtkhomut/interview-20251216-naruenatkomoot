using Interview_Test.Infrastructure;
using Interview_Test.Middlewares;
using Interview_Test.Repositories;
using Interview_Test.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
var connection = "Server=localhost;Database=InterviewTestDb;User=sa;Password=@Passw0rd;Encrypt=True;TrustServerCertificate=True;";
builder.Services.AddDbContext<InterviewTestDbContext>(options =>
    {
        options.UseSqlServer(connection,
            sqlOptions =>
            {
                sqlOptions.UseCompatibilityLevel(110);
                sqlOptions.CommandTimeout(30);
                sqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
            });
    }
);

/** add service authen middleware */
builder.Services.AddScoped<AuthenMiddleware>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


/** database migrations before build */
var scope = app.Services.CreateAsyncScope();
InterviewTestDbContext db = scope.ServiceProvider.GetRequiredService<InterviewTestDbContext>();
await db.Database.MigrateAsync();


app.UseCors("AllowAngular");
app.UseMiddleware<AuthenMiddleware>();
app.UseMvc();
app.Run();