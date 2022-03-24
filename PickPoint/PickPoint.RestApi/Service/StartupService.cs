using Microsoft.Extensions.Hosting;
using Serilog;

namespace PickPoint.RestApi.Service;

public class StartupService : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Log.Information("RestApi service has started");
            
        await Task.CompletedTask.ConfigureAwait(false);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Log.Information("RestApi service was stopped");
            
        await Task.CompletedTask.ConfigureAwait(false);
    }
}