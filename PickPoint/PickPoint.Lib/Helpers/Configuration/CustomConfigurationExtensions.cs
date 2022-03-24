using System.Security;
using Microsoft.Extensions.Configuration;
using PickPoint.Lib.Extensions;

namespace PickPoint.Lib.Helpers.Configuration;

public static class CustomConfigurationExtensions
{
    public static T Get<T>(this IConfigurationSection section) where T : class
    {
        if (section.Value is null)
        {
            throw new VerificationException("Value can not be null");
        }
            
        var obj = section.Value.Deserialize<T>();

        return obj;
    }
}