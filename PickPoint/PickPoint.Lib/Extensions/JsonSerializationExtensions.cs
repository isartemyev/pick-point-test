using System.Globalization;
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
    
    public static TConfig Deserialize<TConfig>(this string json) where TConfig : class
    {
        return JsonConvert.DeserializeObject<TConfig>(json, JsonSerializationOptions) ?? throw new InvalidOperationException();
    }
}