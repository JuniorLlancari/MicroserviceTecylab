using Usuarios.Api.Extensions;
using Usuarios.Application;
using Usuarios.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfraestructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations(); // creacion de bd

app.SeedData();

app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();
