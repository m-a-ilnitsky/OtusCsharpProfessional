using System.Reflection;

namespace HomeWork13.Reflection.Serializer;

internal static class CsvDeserializationHelper
{
    public static T Deserialize<T>(
        this string csv,
        IList<FieldInfo> fields,
        IList<PropertyInfo> properties,
        string columnSeparator,
        string lineSeparator) where T : new()
    {
        if (string.IsNullOrWhiteSpace(csv))
        {
            throw new ArgumentException($"Переданная строка '{csv}' не содержит данных");
        }

        var csvNames = csv
            .GetMembersNames(columnSeparator, lineSeparator)
            .ToList();
        var csvValuesStrings = csv
            .GetMembersValuesStrings(columnSeparator, lineSeparator)
            .ToList();

        if (csvNames.Count != csvValuesStrings.Count)
        {
            throw new ArgumentException($"Количество имен ({csvNames.Count}) не соответствует количеству значений ({csvValuesStrings.Count})");
        }

        var obj = new T();
        var typeName = typeof(T).Name;

        for (var i = 0; i < csvNames.Count; i++)
        {
            var name = csvNames[i];
            var valueString = csvValuesStrings[i];

            var field = fields.FirstOrDefault(x => x.Name == name);
            if (field != null)
            {
                obj.SetFieldValue(field, valueString);
                continue;
            }

            var property = properties.FirstOrDefault(x => x.Name == name);
            if (property != null)
            {
                obj.SetPropertyValue(property, valueString);
                continue;
            }

            throw new ArgumentException($"Тип '{typeName}' не содержит член с именем '{name}'");
        }

        return obj;
    }

    private static string[] GetMembersNames(
        this string csv,
        string columnSeparator,
        string lineSeparator)
    {
        var namesLine = csv.Split(lineSeparator)[0];
        var names = namesLine.Split(columnSeparator);

        return names;
    }

    private static string[] GetMembersValuesStrings(
        this string csv,
        string columnSeparator,
        string lineSeparator)
    {
        var valuesLine = csv.Split(lineSeparator)[1];
        var valuesStrings = valuesLine.Split(columnSeparator);

        return valuesStrings;
    }

    private static void SetFieldValue<T>(
        this T obj,
        FieldInfo field,
        string valueString)
    {
        var type = field.FieldType;
        var value = Parse(type, valueString);
        field.SetValue(obj, value);
    }

    private static void SetPropertyValue<T>(
        this T obj,
        PropertyInfo property,
        string valueString)
    {
        var type = property.PropertyType;
        var value = Parse(type, valueString);
        property.SetValue(obj, value);
    }

    private static object Parse(
        Type type,
        string valueString)
    {
        const string parseMethodName = "Parse";

        var parseMethod = type
            .GetMethod(
                parseMethodName,
                BindingFlags.Static | BindingFlags.Public,
                [typeof(string)])
            ?? throw new Exception($"Тип '{type.Name}' не содержит статический метод '{parseMethodName}'");

        var value = parseMethod
            .Invoke(null, [valueString])
            ?? throw new Exception($"Примитивный тип '{type.Name}' не должен быть равен null");

        return value;
    }
}
