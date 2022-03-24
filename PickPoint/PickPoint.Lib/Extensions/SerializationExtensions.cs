using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace PickPoint.Lib.Extensions;

public class DictionaryAsArrayResolver : DefaultContractResolver
{
    protected override JsonContract CreateContract(Type objectType)
    {
        return objectType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>)) ? base.CreateArrayContract(objectType) : base.CreateContract(objectType);
    }
}

public static class SerializationExtensions
{
    private static readonly IContractResolver DefaultJsonContractResolver = new DictionaryAsArrayResolver { NamingStrategy = new CamelCaseNamingStrategy() };

    public static string ToJson<T>(
        this T obj,
        Formatting formatting = Formatting.Indented,
        IContractResolver? contractResolver = default
    )
        where T : class?
    {
        if (obj is null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        return JsonConvert.SerializeObject(obj, formatting,
            new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Culture = CultureInfo.InvariantCulture,
                NullValueHandling = NullValueHandling.Ignore,

                ObjectCreationHandling = ObjectCreationHandling.Replace,
                ContractResolver = contractResolver ?? DefaultJsonContractResolver,
                Converters = new List<JsonConverter> {new StringEnumConverter()}
            });
    }
}