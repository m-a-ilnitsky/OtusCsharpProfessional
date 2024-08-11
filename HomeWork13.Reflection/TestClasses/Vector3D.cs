namespace HomeWork13.Reflection.TestClasses;

public class Vector3D
{
    public double X;
    public double Y;
    public double Z;

    public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

    public override string ToString() => $"Vector3D({X}, {Y}, {Z})";
}
