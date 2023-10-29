using AppVisitAPI.Data.Context;
using AppVisitAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Utilizando connection string localhost para desenvolvimento. Quando estiver todo o desenvolvimento finalizado, gero uma nova imagem e subo no docker hub, e ai faço a aplicação rodar em docker 
builder.Services.AddDbContext<Context>(x => x.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DevelopmentConnection")));
//builder.Services.AddDbContext<Context>(x => x.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<PaisService, PaisService>();
builder.Services.AddScoped<EstadoService, EstadoService>();
builder.Services.AddScoped<CidadeService, CidadeService>();
builder.Services.AddScoped<LugarService, LugarService>();
builder.Services.AddScoped<ArquivoService, ArquivoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
