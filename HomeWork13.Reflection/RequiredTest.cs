using HomeWork13.Reflection.Serializer;
using HomeWork13.Reflection.TestClasses;
using System.Diagnostics;
using System.Text.Json;

namespace HomeWork13.Reflection;

public class RequiredTest
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        IncludeFields = true
    };

    public static void Run()
    {
        var repetitionsCount = 100000;

        var f = F.GetNew();
        var range = new DoubleRange(5, 7);
        var point = new Point
        {
            X = 3,
            Y = 4
        };
        var vector3D = new Vector3D
        {
            X = 3,
            Y = 4,
            Z = 5
        };
        var vector2D = new Vector2D
        {
            X = 3,
            Y = 4
        };

        Test(f, repetitionsCount);
        Test(range, repetitionsCount);
        Test(point, repetitionsCount);
        Test(vector3D, repetitionsCount);
        Test(vector2D, repetitionsCount);
    }

    public static void Test<T>(T sourceObject, int repetitionsCount)
        where T : notnull, new()
    {
        var (csv, csvSerializationTime) = TestCsvSerialization(sourceObject, repetitionsCount);
        var (csvObject, csvDeserializationTime) = TestCsvDeserialization<T>(csv, repetitionsCount);

        var (json, jsonSerializationTime) = TestJsonSerialization(sourceObject, repetitionsCount);
        var (jsonObject, jsonDeserializationTime) = TestJsonDeserialization<T>(json, repetitionsCount);

        Console.WriteLine(Environment.NewLine + Environment.NewLine);
        Console.WriteLine($"Source object: {sourceObject}");
        Console.WriteLine();
        Console.WriteLine("Csv:");
        Console.WriteLine(csv);
        Console.WriteLine($"Object from csv: {csvObject}");
        Console.WriteLine();
        Console.WriteLine($"Json: {json}");
        Console.WriteLine($"Object from json: {jsonObject}");
        Console.WriteLine();
        Console.WriteLine($"Repetitions count: {repetitionsCount}");
        Console.WriteLine($"To csv  serialization: {csvSerializationTime.Milliseconds}ms, deserialization: {csvDeserializationTime.Milliseconds}ms");
        Console.WriteLine($"To json serialization: {jsonSerializationTime.Milliseconds}ms, deserialization: {jsonDeserializationTime.Milliseconds}ms");
    }

    public static (string Csv, TimeSpan Time) TestCsvSerialization<T>(T obj, int repetitionsCount)
        where T : notnull
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        string csv = CsvSerializer.Serialize(obj);

        for (var i = 1; i < repetitionsCount; i++)
        {
            csv = CsvSerializer.Serialize(obj);
        }

        stopwatch.Stop();

        return (csv, stopwatch.Elapsed);
    }

    public static (T Obj, TimeSpan Time) TestCsvDeserialization<T>(string csv, int repetitionsCount)
        where T : new()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var obj = CsvSerializer.Deserialize<T>(csv);

        for (var i = 1; i < repetitionsCount; i++)
        {
            obj = CsvSerializer.Deserialize<T>(csv);
        }

        stopwatch.Stop();

        return (obj!, stopwatch.Elapsed);
    }

    public static (string Json, TimeSpan Time) TestJsonSerialization<T>(T obj, int repetitionsCount)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        string json = JsonSerializer.Serialize(obj, JsonSerializerOptions);

        for (var i = 1; i < repetitionsCount; i++)
        {
            json = JsonSerializer.Serialize(obj, JsonSerializerOptions);
        }

        stopwatch.Stop();

        return (json, stopwatch.Elapsed);
    }

    public static (T Obj, TimeSpan Time) TestJsonDeserialization<T>(string json, int repetitionsCount)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var obj = JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);

        for (var i = 1; i < repetitionsCount; i++)
        {
            obj = JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
        }

        stopwatch.Stop();

        return (obj!, stopwatch.Elapsed);
    }
}
