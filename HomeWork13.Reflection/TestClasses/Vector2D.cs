namespace HomeWork13.Reflection.TestClasses;

public class Vector2D
{
    public double X;
    public double Y;

    public double GetLength() => Math.Sqrt(X * X + Y * Y);

    public override string ToString() => $"Vector2D({X}, {Y})";
}
