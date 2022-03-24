using System.Text;
using Microsoft.Extensions.Configuration.Json;

namespace PickPoint.Lib.Helpers.Configuration;

public class CustomConfigurationProvider : JsonStreamConfigurationProvider
{
    public const string Key = "configuration";
        
    public CustomConfigurationProvider(JsonStreamConfigurationSource source) : base(source)
    {
    }

    public override void Load(Stream stream)
    {
        if (!stream.CanSeek)
        {
            return;
        }

        stream.Seek(0L, SeekOrigin.Begin);

        using var ms = new MemoryStream();

        stream.CopyTo(ms);

        var json = Encoding.UTF8.GetString(ms.ToArray());

        Data = new Dictionary<string, string> { { Key, json } };
    }
}