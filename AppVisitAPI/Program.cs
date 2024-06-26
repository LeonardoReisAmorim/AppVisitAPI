using AppVisitAPI.Data.Context;
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
builder.Services.AddScoped<PaisService, PaisService>();
builder.Services.AddScoped<EstadoService, EstadoService>();
builder.Services.AddScoped<CidadeService, CidadeService>();
builder.Services.AddScoped<LugarService, LugarService>();
builder.Services.AddScoped<ArquivoService, ArquivoService>();

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
