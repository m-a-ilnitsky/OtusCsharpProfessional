using HomeWork13.Reflection.Serializer;
using HomeWork13.Reflection.TestClasses;

namespace HomeWork13.Reflection;

public class SimpleTest
{
    public static void Run()
    {
        var f1 = new F();
        var f2 = F.GetNew();

        var point1 = new Point();
        var point2 = new Point
        {
            X = 3,
            Y = 4
        };

        var range1 = new DoubleRange();
        var range2 = new DoubleRange(5, 7);

        object[] objects = [f1, f2, point1, point2, range1, range2];

        foreach (var obj in objects)
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Source object: " + obj);

            var csv = CsvSerializer.Serialize(obj);
            Console.WriteLine("csv:");
            Console.WriteLine(csv);

            object? createdObj = obj is F ? CsvSerializer.Deserialize<F>(csv)
                            : obj is Point ? CsvSerializer.Deserialize<Point>(csv)
                            : obj is DoubleRange ? CsvSerializer.Deserialize<DoubleRange>(csv)
                            : null;

            Console.WriteLine("Deserialized object: " + createdObj);
        }

        Console.WriteLine();
        Console.WriteLine("End");
    }
}