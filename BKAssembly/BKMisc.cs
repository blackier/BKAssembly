using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Windows;

namespace BKAssembly;

public static class BKMisc
{
    public static string CalculMD5(string srcStr)
    {
        MD5 md5 = MD5.Create();

        byte[] byteSrc = Encoding.UTF8.GetBytes(srcStr);
        byte[] byteTarget = md5.ComputeHash(byteSrc);

        StringBuilder srcBuilder = new StringBuilder();
        foreach (byte b in byteTarget)
        {
            srcBuilder.Append(b.ToString("x2"));
        }

        return srcBuilder.ToString();
    }

    public static string JsonSerialize<T>(
        T obj,
        bool ignoreNullValues = true,
        bool writeIndented = true,
        JavaScriptEncoder? charsetEncoder = null
    )
    {
        if (charsetEncoder == null)
            charsetEncoder = JavaScriptEncoder.Create(UnicodeRanges.All);
        JsonSerializerOptions options =
            new()
            {
                DefaultIgnoreCondition = ignoreNullValues
                    ? JsonIgnoreCondition.WhenWritingNull
                    : JsonIgnoreCondition.Never,
                WriteIndented = writeIndented,
                Encoder = charsetEncoder
            };
        return JsonSerializer.Serialize<T>(obj, options);
    }

    public static T? JsonDeserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }

    public static string LoadTextFile(string filePath)
    {
        string retStr = "";
        if (File.Exists(filePath))
        {
            retStr = File.ReadAllText(filePath);
        }
        return retStr;
    }

    public static bool SaveTextFile(string filePath, string text)
    {
        bool retBool = true;
        try
        {
            File.WriteAllText(filePath, text);
        }
        catch
        {
            retBool = false;
        }
        return retBool;
    }

    public static TimeSpan TimeNow()
    {
        return DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
    }

    public static T? CloneObject<T>(T theObject)
    {
        string jsonData = JsonSerialize(theObject);
        return JsonDeserialize<T>(jsonData);
    }
}
