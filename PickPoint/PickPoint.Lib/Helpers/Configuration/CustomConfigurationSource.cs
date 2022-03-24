using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace PickPoint.Lib.Helpers.Configuration;

public class CustomConfigurationSource : JsonStreamConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new CustomConfigurationProvider(this);
    }
}