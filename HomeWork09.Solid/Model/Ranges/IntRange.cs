namespace HomeWork09.Solid.Model.Ranges;

public class IntRange : IRange<int>
{
    public int From { get; set; }

    public int To { get; set; }

    public IntRange() { }

    public IntRange(int from, int to)
    {
        From = from;
        To = to;
    }

    public object Clone() => new IntRange(From, To);
}
