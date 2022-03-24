using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickPoint.Lib.Helpers.Auth;

namespace PickPoint.RestApi.BuilderExtensions;

internal static class HelpersBuilderExtensions
{
    public static IHostBuilder UseHelpers(this IHostBuilder builder) => builder.ConfigureServices((_, services) =>
    {
        services.AddTransient<IAuthTokenStore, AuthTokenStore>();
    });
}