using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApiGrpc.Services;
var builder = WebApplication.CreateBuilder(args);

// Configura o Kestrel para suportar HTTP/2
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
    });
});

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<GreeterService>(); 
app.MapGet("/", () => "Servidor gRPC rodando...");

app.Run();
