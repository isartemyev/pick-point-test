using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickPoint.Lib.Repositories.Declaration;
using PickPoint.Lib.Repositories.Implementation;

namespace PickPoint.RestApi.BuilderExtensions;

internal static class RepositoriesBuilderExtensions
{
    public static IHostBuilder UseRepositories(this IHostBuilder builder) => builder.ConfigureServices(services =>
    {
        services.AddTransient<IMerchantRepository, MerchantRepository>();
        services.AddTransient<IMachineRepository, MachineRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
    });
}