using Microsoft.EntityFrameworkCore;
using MesasService.Data;
using MesasService.Repository;
using MesasService.Services;
using Microsoft.OpenApi.Models;
using MassTransit;
using MesasService.DTOs;
using MesasService.Consumer;
{
    
}



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MesaServices", Version = "v1" });
});



var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<MesaDb>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddMassTransit(X =>
{
    X.AddConsumer<MesaUpdateConsumer>();
    X.UsingRabbitMq((context,cfg)=>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("admin");
            h.Password("admin123");
        });
    
        cfg.ReceiveEndpoint("mesa_mesagge_update_queue", e =>
        {
            e.ConfigureConsumer<MesaUpdateConsumer>(context);
        });
    });
});



builder.Services.AddScoped<IMesaRepository, MesaRepository>();



builder.Services.AddScoped<CrearMesaService>();
builder.Services.AddScoped<MostrarMesaServices>();
builder.Services.AddScoped<ActualizarMesaService>();
builder.Services.AddScoped<EliminarMesaServices>();





var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();



app.MapControllers(); 

app.Run();
