using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BKAssembly;

public class BKJsonStringEnumConverter<T> : JsonConverter<T> where T : Enum
{
    private readonly T _defaultValue;
    private readonly JsonStringEnumConverter _converter;

    public BKJsonStringEnumConverter() : this(default(T), null, true)
    {
    }

    public BKJsonStringEnumConverter(T defaultValue, JsonNamingPolicy? namingPolicy = null, bool allowIntegerValues = true)
    {
        _defaultValue = defaultValue;
        _converter = new JsonStringEnumConverter(namingPolicy, allowIntegerValues);
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return _converter.CanConvert(typeToConvert);
    }

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            var convert = _converter.CreateConverter(typeToConvert, options) as JsonConverter<T>;
            return convert.Read(ref reader, typeToConvert, options);
        }
        catch
        {
            return _defaultValue;
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var convert = _converter.CreateConverter(typeof(T), options) as JsonConverter<T>;
        convert.Write(writer, value, options);
    }
}
