using Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{ options.AddPolicy("Todos" ,
   builder => builder.WithOrigins("*").WithHeaders("*").WithMethods("*"));
});

//Esta sección de cósigo se agregó para la conexion ocn DB
var connectionString = builder.Configuration.GetConnectionString("Conexion");
builder.Services.AddDbContext<DBContextSistema>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Todos");

app.UseAuthorization();

app.MapControllers();

app.Run();
