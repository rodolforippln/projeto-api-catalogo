using ApiCatalogo.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connStrings = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CatalogoContex>(options
    => options.UseMySql(connStrings, ServerVersion.AutoDetect(connStrings)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
