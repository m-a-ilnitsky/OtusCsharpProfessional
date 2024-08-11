namespace HomeWork13.Reflection.Serializer;

public static class CsvSerializer
{
    public const string LineSeparator = "\n";
    public const string ColumnSeparator = ",";

    public static string Serialize<T>(
        this T obj,
        string columnSeparator = ColumnSeparator,
        string lineSeparator = LineSeparator)
    {
        return CsvSerializationHelper.Serialize(obj, columnSeparator, lineSeparator);
    }

    public static T Deserialize<T>(
        string csv,
        string columnSeparator = ColumnSeparator,
        string lineSeparator = LineSeparator) where T : new()
    {
        var type = typeof(T);

        var fields = type
            .GetPrimitiveFields()
            .ToList();
        var properties = type
            .GetPrimitiveProperties()
            .ToList();

        return csv.Deserialize<T>(
            fields,
            properties,
            columnSeparator,
            lineSeparator);
    }
}
