using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService1.Services;

public class HourGlassService(ILogger<GreeterService> logger) : HourGlass.HourGlassBase
{
    private readonly ILogger<GreeterService> _logger = logger;

    public override async Task Subscribe(Empty request, IServerStreamWriter<Hour> responseStream, ServerCallContext context)
    {
        while (!context.CancellationToken.IsCancellationRequested)
        {
            await Task.Delay(1000);
            var hour = DateTime.Now.ToLongTimeString();
            await responseStream.WriteAsync(new Hour { Message = hour });
        }
    }
}