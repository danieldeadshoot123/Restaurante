using MenuService.Data;
using MenuService.Repository;
using MenuService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title ="MenuServices", Version = "v1"});
});

var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<MenuDb>(options => 
    options.UseNpgsql(connectionString));



builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<CrearMenuService>();
builder.Services.AddScoped<MostrarMenuService>();
builder.Services.AddScoped<ActualizarMenuService>();
builder.Services.AddScoped<ELiminarMenuService>();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();



app.Run();

