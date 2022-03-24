using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickPoint.Lib.Mapping;

namespace PickPoint.RestApi.BuilderExtensions;

internal static class MappingsBuilderExtensions
{
    public static IHostBuilder UseMapping(this IHostBuilder builder) => builder.ConfigureServices(services =>
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(OrderMappingProfile.Init());
            mc.AddProfile(MachineMappingProfile.Init());
            mc.AddProfile(MerchantMappingProfile.Init());
        });

        var mapper = mappingConfig.CreateMapper();

        services.AddSingleton(mapper);
    });
}