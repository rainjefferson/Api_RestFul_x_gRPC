using Grpc.Core;
using ApiGrpc.Protos;
using System.Diagnostics;

namespace ApiGrpc.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var response = new HelloReply { Message = "Hello, gRPC!" };
            stopwatch.Stop();
            Console.WriteLine($"gRPC Time Taken: {stopwatch.ElapsedMilliseconds} ms");
            return Task.FromResult(response);
        }
    }
}
