using System.Reflection;
using System.Text;

namespace HomeWork13.Reflection.Serializer;

internal static class CsvSerializationHelper
{
    public static string Serialize<T>(
        this T obj,
        string columnSeparator,
        string lineSeparator)
    {
        ArgumentNullException.ThrowIfNull(obj);

        var membersNamesAndValues = GetMembersNamesAndValues(obj);

        return membersNamesAndValues.GetCsvString(columnSeparator, lineSeparator);
    }

    private static string GetCsvString(
        this IDictionary<string, object?> namesAndValues,
        string columnSeparator,
        string lineSeparator)
    {
        if (namesAndValues.Count == 0)
        {
            return "";
        }

        var sb = new StringBuilder();

        foreach (var name in namesAndValues.Keys)
        {
            sb.Append(name).Append(columnSeparator);
        }

        sb.Remove(sb.Length - 1, 1).Append(lineSeparator);

        foreach (var value in namesAndValues.Values)
        {
            sb.Append(value).Append(columnSeparator);
        }

        return sb.Remove(sb.Length - 1, 1).ToString();
    }

    private static Dictionary<string, object?> GetMembersNamesAndValues<T>(T obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        var namesAndValues = new Dictionary<string, object?>();

        namesAndValues.AddFields(obj);
        namesAndValues.AddProperties(obj);

        return namesAndValues;
    }

    private static void AddFields(
        this Dictionary<string, object?> namesAndValues,
        object obj)
    {
        var fields = obj
            .GetType()
            .GetPrimitiveFields();

        foreach (var field in fields)
        {
            var name = field.Name;
            var value = field.GetValue(obj);
            namesAndValues.Add(name, value);
        }
    }

    private static void AddProperties(
        this Dictionary<string, object?> namesAndValues,
        object obj)
    {
        var properties = obj
            .GetType()
            .GetPrimitiveProperties();

        foreach (var property in properties)
        {
            var name = property.Name;
            var value = property.GetValue(obj);
            namesAndValues.Add(name, value);
        }
    }

    public static IEnumerable<FieldInfo> GetPrimitiveFields(this Type type)
    {
        return type
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(f => f.FieldType.IsPrimitive && !f.Name.EndsWith(">k__BackingField"));
    }

    public static IEnumerable<PropertyInfo> GetPrimitiveProperties(this Type type)
    {
        return type
            .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(p => p.PropertyType.IsPrimitive && p.SetMethod != null);
    }
}
