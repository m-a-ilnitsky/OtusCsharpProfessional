using System.Numerics;

namespace HomeWork17.Delegates.Items;

public class Point2D<TNumber>
    where TNumber : struct, INumber<TNumber>
{
    public TNumber X { get; set; }

    public TNumber Y { get; set; }

    public Point2D() { }

    public Point2D(TNumber x, TNumber y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X}; {Y})";
}
