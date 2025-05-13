using Microsoft.EntityFrameworkCore;
using PedidoDB.Data;
using PedidosService.Repository;
using PedidosService.Services;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PedidosService", Version = "v1" });
});



var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<PedidoDb>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddHttpClient<TotalServices>();


builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();


builder.Services.AddScoped<CreatePedidoService>();
builder.Services.AddScoped<MostrarPedidoService>();
builder.Services.AddScoped<ActualizarPedidoService>();
builder.Services.AddScoped<EliminarPedidoService>();
builder.Services.AddScoped<TotalServices>();


builder.Services.AddHttpClient<MenuService>(); 
builder.Services.AddHttpClient<MesasService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();



app.MapControllers(); 

app.Run();
