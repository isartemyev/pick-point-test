using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickPoint.Lib.Filters.Declarations;
using PickPoint.Lib.Filters.Implementations;

namespace PickPoint.RestApi.BuilderExtensions;

internal static class FiltersBuilderExtensions
{
    public static IHostBuilder UseFilters(this IHostBuilder builder) => builder.ConfigureServices((_, services) =>
    {
        services.AddTransient<IOrderFilter, OrderFilter>();
        services.AddTransient<IMachineFilter, MachineFilter>();
        services.AddTransient<IMerchantFilter, MerchantFilter>();
    });
}