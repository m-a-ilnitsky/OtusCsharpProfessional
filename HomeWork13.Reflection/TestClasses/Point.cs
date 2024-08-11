namespace HomeWork13.Reflection.TestClasses;

public class Point
{
    public long X { get; set; }

    public long Y { get; set; }

    public override string ToString() => $"Point({X}, {Y})";
}
