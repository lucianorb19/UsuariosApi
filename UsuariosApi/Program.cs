using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Data;
using UsuariosApi.Models;
using UsuariosApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//VARIÁVEL PARA DEFINIÇÕES ABAIXO
var connectionString = builder.Configuration.GetConnectionString("UsuarioConnection");
builder.Services.AddDbContext<UsuarioDbContext>(opts =>
{
    opts.UseMySql(connectionString,
              ServerVersion.AutoDetect(connectionString));
});


//ADICIONANDO Identity AO PROJETO - RELACIONADO COM USUARIO E SEU PAPEL
// .AddEntityFrameworkStores<UsuarioDbContext>() IDENTITY CUIDA DA CONEXÃO COM BD ATRAVÉS DE UsuarioDbContext
// .AddDefaultTokenProviders(); AUTENTICAÇÃO
builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>() 
    .AddDefaultTokenProviders();

//AUTOMAPPER
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//CONFIGURADO SERVICE Services->CadastroService
builder.Services.AddScoped<CadastroService>();

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
