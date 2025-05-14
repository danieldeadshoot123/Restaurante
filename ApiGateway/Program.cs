using Yarp.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);

// Agrega YARP
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();
app.MapReverseProxy(); 

app.Run();
