using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickPoint.Lib.Facades.Declarations;
using PickPoint.Lib.Facades.Implementations;

namespace PickPoint.RestApi.BuilderExtensions;

internal static class FacadesBuilderExtensions
{
    public static IHostBuilder UseFacades(this IHostBuilder builder) => builder.ConfigureServices((_, services) =>
    {
        services.AddTransient<IAuthFacade, AuthFacade>();
        services.AddTransient<IMachineFacade, MachineFacade>();
        services.AddTransient<IMerchantFacade, MerchantFacade>();
        services.AddTransient<IOrderFacade, OrderFacade>();
    });
}