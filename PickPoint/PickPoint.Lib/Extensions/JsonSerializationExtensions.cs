using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PickPoint.Lib.Extensions;

public static class JsonSerializationExtensions
{
    public static readonly JsonSerializerSettings JsonSerializationOptions = new()
    {
        Formatting             = Formatting.Indented,
        DateFormatHandling     = DateFormatHandling.IsoDateFormat,
        Culture                = CultureInfo.InvariantCulture,
        NullValueHandling      = NullValueHandling.Ignore,
        TypeNameHandling       = TypeNameHandling.Auto,
        ObjectCreationHandling = ObjectCreationHandling.Replace,
        ContractResolver       = new DictionaryAsArrayResolver(),
        Converters             = new JsonConverter[] { new StringEnumConverter()}
    };
        
    public static byte[] Serialize<TConfig>(this TConfig config) where TConfig : class
    {
        return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(config, JsonSerializationOptions));
    }

    public static TConfig Deserialize<TConfig>(this IEnumerable<byte> data) where TConfig : class
    {
        return JsonConvert.DeserializeObject<TConfig>(Encoding.UTF8.GetString(data.ToArray()), JsonSerializationOptions);
    }
        
    public static TConfig Deserialize<TConfig>(this string json) where TConfig : class
    {
        return JsonConvert.DeserializeObject<TConfig>(json, JsonSerializationOptions);
    }

    public static string Deserialize<TConfig>(this Stream stream) where TConfig : class
    {
        using var ms = new MemoryStream();
            
        stream.CopyTo(ms);
            
        var instance = JsonConvert.DeserializeObject<TConfig>(Encoding.UTF8.GetString(ms.ToArray()), JsonSerializationOptions);

        return JsonConvert.SerializeObject(instance, JsonSerializationOptions);
    }
}