using APICategoria.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Obtendo a string de conexão que está no appSetings
var mySqlConection = builder.Configuration.GetConnectionString("DefaultConection");

// Pega o contexto e configura, setado o provedor.
builder.Services.AddDbContext<AppDbContexto>(option => 
    option.UseMySql(mySqlConection, 
    ServerVersion.AutoDetect(mySqlConection)));

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
