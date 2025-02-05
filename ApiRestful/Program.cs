using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Define o caminho base da API RESTful
//app.UsePathBase("/restful");

// Mapeia as rotas corretamente considerando o caminho base
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", () =>
    {
        var stopwatch = Stopwatch.StartNew();
        var response = new { Message = "Hello, Restful API!" };
        stopwatch.Stop();
        return Results.Ok(new { Response = response, TimeTaken = stopwatch.ElapsedMilliseconds });
    });
});

app.Run();
