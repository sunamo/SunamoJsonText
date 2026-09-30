using sunamo;



public class JsonSystemTextJson : IJsSerializer
{
    /// <summary>
    ///     Instance must bu create due to anytime availability in RH.DumpAsString
    /// </summary>
    public static JsonSystemTextJson instance = new();

    private JsonSystemTextJson()
    {
    }

    public object Deserialize(string o, Type targetType)
    {
        return JsonSerializer.Deserialize(o, targetType);
    }

    public string Serialize(object o)
    {
        return JsonSerializer.Serialize(o, o.GetType());
    }

    public string SerializeT<T>(T o)
    {
        return JsonSerializer.Serialize(o, o.GetType());
    }

    public static string FormatJsonText(string json)
    {
        using JsonDocument jDoc = JsonDocument.Parse(json);
        return JsonSerializer.Serialize(jDoc, new JsonSerializerOptions { WriteIndented = true });
    }

    public string SerializeT<T>(T o, bool indented, n.JsonSerializerSettings jsonSerializerSettings = null)
    {
        return JsonSerializer.Serialize(o, o.GetType(), new JsonSerializerOptions { WriteIndented = indented });
    }

    public string Serialize(object o, bool indented, n.JsonSerializerSettings jsonSerializerSettings = null)
    {
        if (jsonSerializerSettings == null)
        {
            jsonSerializerSettings = new n.JsonSerializerSettings();
        }

        object oo = null;

        if (jsonSerializerSettings.NullValueHandling == n.NullValueHandling.Ignore)
        {
            /*
toto funguje pokud všechny proměnné josu v rootu
            pokud nejsou, poskládat objekt bez null rekurzivně je složité

            a vložit zde jen např. compilerOptions, to bych potom musel zpět dopsat zbytek jsonu

            nejlepší na toto bude použít jiný serializer který umí vynechat null


             */
            oo = ReClasser.FixMeUp(o);
        }

        // Value cannot be null. (Parameter 'inputType')
        return JsonSerializer.Serialize(oo != null ? oo : o, oo != null ? oo.GetType() : o.GetType(), new JsonSerializerOptions { WriteIndented = indented });
    }

}
