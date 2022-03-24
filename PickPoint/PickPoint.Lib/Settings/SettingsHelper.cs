using System.Reflection;
using Newtonsoft.Json;

namespace PickPoint.Lib.Settings;

public static class SettingsHelper
{
    public static TSettings Init<TSettings>() where TSettings : new()
    {
        var settings = new TSettings();

        settings
            .GetType()
            .GetMethod("Init", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
            ?.Invoke(settings, null);

        return settings;
    }
        
    public static TSettings Init<TSettings>(Action<TSettings> action) where TSettings : new()
    {
        var settings = new TSettings();

        settings
            .GetType()
            .GetMethod("Init", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
            ?.Invoke(settings, null);
            
        action?.Invoke(settings);

        return settings;
    }

    public static TSettings Init<TSettings>(string path) where TSettings : new()
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));

        var src = Init<TSettings>();
        var dst = LoadFromJson<TSettings>(path);

        SetProperties(src, dst);

        dst.SaveToJson(path);

        return dst;
    }

    public static void SaveToJson<TSettings>(this TSettings settings, string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));

        var fileInfo = new FileInfo(path);

        if (fileInfo.Directory is null)
            throw new FileNotFoundException($"The configuration file for {settings.GetType()} was not saved: the path to {path} was not found.", path);

        if (!fileInfo.Directory.Exists)
            fileInfo.Directory.Create();

        File.WriteAllText(path, JsonConvert.SerializeObject(settings, Formatting.Indented));
    }

    public static TSettings LoadFromJson<TSettings>(string path) where TSettings : new()
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException(nameof(path));

        var fileInfo = new FileInfo(path);

        return !fileInfo.Exists ? Init<TSettings>() : JsonConvert.DeserializeObject<TSettings>(File.ReadAllText(path));
    }

    public static void SetProperties(object src, object dst)
    {
        foreach (var item in src.GetType().GetProperties().Where(prop => prop.CanRead && prop.CanWrite))
        {
            var srcValue = item.GetValue(src, null);
            var dstValue = item.GetValue(dst, null);

            if (!item.PropertyType.Module.ScopeName.Contains(nameof(System)))
            {
                if (dstValue is null)
                {
                    SetValue(item, dst, srcValue);

                    continue;
                }

                SetProperties(srcValue, dstValue);
            }
            else
            {
                if (!(dstValue is null))
                {
                    var dstType = dstValue.GetType();

                    if (dstType.IsPrimitive || dstType.IsEnum)
                        continue;

                    if (dstType.IsValueType && dstValue.Equals(GetDefault(dstType)))
                    {
                        SetValue(item, dst, srcValue);
                    }

                    continue;
                }

                SetValue(item, dst, srcValue);
            }
        }
    }

    private static object GetDefault(Type type)
    {
        return type.IsValueType ? Activator.CreateInstance(type) : null;
    }

    private static void SetValue(PropertyInfo item, object dst, object src)
    {
        if (src is null || item.GetSetMethod() is null)
            return;

        item.SetValue(dst, src);
    }

}