using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Authorization;
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

//IdadeAuthorization
builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

//CONFIGURADO SERVICE Services->UsuarioService
builder.Services.AddScoped<UsuarioService>();
//CONFIGURADO SERVICE Services->TokenService
builder.Services.AddScoped<TokenService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//POLÍTICA DE ACESSO PARA AcessoController - [Authorize(Policy = "IdadeMinima")]
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IdadeMinima", policy =>
                      policy.AddRequirements(new IdadeMinima(18))
                      );
});

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
