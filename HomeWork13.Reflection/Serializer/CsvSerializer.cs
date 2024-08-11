using System.Data;
using System.Reflection;
using System.Text;

namespace HomeWork13.Reflection.Serializer;

public static class CsvSerializer
{
    public static char LineSeparator = '\n';
    public static char ColumnSeparator = ',';

    public static string Serialize(this object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        var membersNamesAndValues = GetMembersNamesAndValues(obj);

        return GetCsvString(membersNamesAndValues);
    }

    #region Serialize private methods

    private static Dictionary<string, object?> GetMembersNamesAndValues(object obj)
    {
        var namesAndValues = new Dictionary<string, object?>();

        AddFields(namesAndValues, obj);
        AddProperties(namesAndValues, obj);

        return namesAndValues;
    }

    private static void AddFields(
        IDictionary<string, object?> namesAndValues,
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
        IDictionary<string, object?> namesAndValues,
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

    private static IEnumerable<FieldInfo> GetPrimitiveFields(this Type type)
    {
        return type
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(f => f.FieldType.IsPrimitive);
    }

    private static IEnumerable<PropertyInfo> GetPrimitiveProperties(this Type type)
    {
        return type
            .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(p => p.PropertyType.IsPrimitive);
    }

    private static string GetCsvString(IDictionary<string, object?> namesAndValues)
    {
        if (namesAndValues.Count == 0)
        {
            return "Нет подходящих свойств";
        }

        var sb = new StringBuilder();

        foreach (var name in namesAndValues.Keys)
        {
            sb.Append(name).Append(ColumnSeparator);
        }

        sb.Remove(sb.Length - 1, 1).Append(LineSeparator);

        foreach (var value in namesAndValues.Values)
        {
            sb.Append(value).Append(ColumnSeparator);
        }

        return sb.Remove(sb.Length - 1, 1).ToString();
    }

    #endregion

    public static T Deserialize<T>(string csv) where T : new()
    {
        if (string.IsNullOrWhiteSpace(csv))
        {
            throw new ArgumentException($"Переданная строка '{csv}' не содержит данных");
        }

        var csvNames = csv
            .GetMembersNames()
            .ToList();
        var csvValuesStrings = csv
            .GetMembersValuesStrings()
            .ToList();

        if (csvNames.Count != csvValuesStrings.Count)
        {
            throw new ArgumentException($"Количество имен ({csvNames.Count}) не соответствует количеству значений ({csvValuesStrings.Count})");
        }

        var type = typeof(T);

        var fields = type
            .GetPrimitiveFields()
            .ToList();
        var properties = type
            .GetPrimitiveProperties()
            .ToList();

        var obj = new T();

        for (var i = 0; i < csvNames.Count; i++)
        {
            var name = csvNames[i];
            var valueString = csvValuesStrings[i];

            var field = fields.FirstOrDefault(x => x.Name == name);
            if (field != null)
            {
                SetFieldValue(obj, field, valueString);
                continue;
            }

            var property = properties.FirstOrDefault(x => x.Name == name);
            if (property != null)
            {
                SetPropertyValue(obj, property, valueString);
                continue;
            }

            throw new ArgumentException($"Тип '{type.Name}' не содержит член с именем '{name}'");
        }

        return obj;
    }

    #region Deserialize private methods

    private static IEnumerable<string> GetMembersNames(this string csv)
    {
        var namesLine = csv.GetLines()[0];
        var names = namesLine.GetColumns();

        return names;
    }

    private static IEnumerable<string> GetMembersValuesStrings(this string csv)
    {
        var valuesLine = csv.GetLines()[1];
        var valuesStrings = valuesLine.GetColumns();

        return valuesStrings;
    }

    private static string[] GetLines(this string csv) => csv.Split(LineSeparator);

    private static string[] GetColumns(this string csvLine) => csvLine.Split(ColumnSeparator);

    private static void SetFieldValue(object obj, FieldInfo field, string valueString)
    {
        Type type = field.FieldType;
        var value = Parse(type, valueString);
        field.SetValue(obj, value);
    }

    private static void SetPropertyValue(object obj, PropertyInfo property, string valueString)
    {
        Type type = property.PropertyType;
        var value = Parse(type, valueString);
        property.SetValue(obj, value);
    }

    private static object Parse(Type type, string valueString)
    {
        const string methodName = "Parse";

        var parseMethod = type
            .GetMethod(
                methodName,
                BindingFlags.Static | BindingFlags.Public,
                [typeof(string)])
            ?? throw new Exception($"Тип '{type.Name}' не содержит статический метод '{methodName}'");

        var value = parseMethod
            .Invoke(null, [valueString])
            ?? throw new Exception($"Примитивный тип '{type.Name}' не должен быть равен null");

        return value;
    }

    #endregion
}
