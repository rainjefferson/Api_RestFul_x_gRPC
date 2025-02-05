using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
using ApiGrpc.Protos;

class Program
{
    static async Task Main()
    {
        int numRequests = 1000; // Defina quantas requisições deseja testar
        Console.WriteLine($"Iniciando teste de {numRequests} requisições para cada API...");

        // Executa os testes simultaneamente
        var restfulTask = TestarApiRestful(numRequests);
        var grpcTask = TestarApiGrpc(numRequests);

        await Task.WhenAll(restfulTask, grpcTask);

        Console.WriteLine("\nComparação Final:");
        Console.WriteLine($"⏳ API RESTful - Média: {restfulTask.Result:F2} ms");
        Console.WriteLine($"⚡ API gRPC - Média: {grpcTask.Result:F2} ms");

        Console.WriteLine("\n🏆 API mais rápida: " + (restfulTask.Result < grpcTask.Result ? "RESTful" : "gRPC"));
    }

    static async Task<double> TestarApiRestful(int numRequests)
    {
        using var httpClient = new HttpClient();
        string url = "http://localhost:5001";

        Stopwatch stopwatch = Stopwatch.StartNew();
        Task<HttpResponseMessage>[] tasks = new Task<HttpResponseMessage>[numRequests];

        for (int i = 0; i < numRequests; i++)
        {
            tasks[i] = httpClient.GetAsync(url);
        }

        await Task.WhenAll(tasks);
        stopwatch.Stop();

        double tempoMedio = stopwatch.Elapsed.TotalMilliseconds / numRequests;
        Console.WriteLine($"API RESTful - Tempo total: {stopwatch.Elapsed.TotalMilliseconds:F2} ms | Média por requisição: {tempoMedio:F2} ms");
        return tempoMedio;
    }


    static async Task<double> TestarApiGrpc(int numRequests)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5000");
        var client = new Greeter.GreeterClient(channel); // Cliente gerado a partir do .proto

        Stopwatch stopwatch = Stopwatch.StartNew();
        Task<HelloReply>[] tasks = new Task<HelloReply>[numRequests];

        for (int i = 0; i < numRequests; i++)
        {
            tasks[i] = client.SayHelloAsync(new HelloRequest { Name = "Teste" }).ResponseAsync;
        }

        await Task.WhenAll(tasks);
        stopwatch.Stop();

        double tempoMedio = stopwatch.Elapsed.TotalMilliseconds / numRequests;
        Console.WriteLine($"API gRPC - Tempo total: {stopwatch.Elapsed.TotalMilliseconds:F2} ms | Média por requisição: {tempoMedio:F2} ms");
        return tempoMedio;
    }
}
