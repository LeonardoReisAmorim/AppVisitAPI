using AppVisitAPI.Data.Context;
using AppVisitAPI.Interfaces.IArquivo;
using AppVisitAPI.Interfaces.ICidade;
using AppVisitAPI.Interfaces.IEstado;
using AppVisitAPI.Interfaces.ILugar;
using AppVisitAPI.Interfaces.IPais;
using AppVisitAPI.Repositories;
using AppVisitAPI.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Utilizando connection string localhost para desenvolvimento.
builder.Services.AddDbContext<Context>(x => x.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DevelopmentConnection")));
//builder.Services.AddDbContext<Context>(x => x.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPaisService, PaisService>();
builder.Services.AddScoped<IPaisRepository, PaisRepository>();
builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
builder.Services.AddScoped<ICidadeService, CidadeService>();
builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
builder.Services.AddScoped<ILugarService, LugarService>();
builder.Services.AddScoped<ILugarRepository, LugarRepository>();
builder.Services.AddScoped<IArquivoService, ArquivoService>();
builder.Services.AddScoped<IArquivoRepository, ArquivoRepository>();

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = long.MaxValue;
});

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = long.MaxValue;
    x.MultipartHeadersLengthLimit = int.MaxValue;
});

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app cors
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("corsapp");
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
