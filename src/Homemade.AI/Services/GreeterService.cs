using Grpc.Core;

using Homemade.AI;

namespace Homemade.AI.Services;

/// <summary>
/// Example service implementation.
/// </summary>
public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;

    /// <summary>
    /// Example service implementation.
    /// </summary>
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
    }
}