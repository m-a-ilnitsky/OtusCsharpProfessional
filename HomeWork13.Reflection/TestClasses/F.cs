namespace HomeWork13.Reflection.TestClasses;

public class F
{
    int i1, i2, i3, i4, i5;

    public static F GetNew() => new()
    {
        i1 = 1,
        i2 = 2,
        i3 = 3,
        i4 = 4,
        i5 = 5
    };

    public override string ToString() => $"F{{i1 = {i1}, i2 = {i2}, i3 = {i3}, i4 = {i4}, i5 = {i5}}}";
}
