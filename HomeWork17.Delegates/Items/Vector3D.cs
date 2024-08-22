using System;
using System.Numerics;

namespace HomeWork17.Delegates.Items;

record class Vector3D<TNumber>(TNumber X, TNumber Y, TNumber Z)
    where TNumber : struct, INumber<TNumber>
{
    public double GetLength() => Math.Sqrt(Convert.ToDouble(X * X + Y * Y + Z * Z));

    public override string ToString() => $"({X}; {Y}; {Z})";
}
