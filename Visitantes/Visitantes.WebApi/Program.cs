using Microsoft.EntityFrameworkCore;
using Oracle.DataAccess.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var cadenaConexion = builder.Configuration.GetConnectionString("defaultConnection");

builder.Services.AddDbContext<ModelContext>(x =>
    x.UseOracle(
        cadenaConexion
    )
);

builder.Services.AddControllers();
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
