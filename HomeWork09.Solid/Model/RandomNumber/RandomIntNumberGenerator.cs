using HomeWork09.Solid.Model.Ranges;

namespace HomeWork09.Solid.Model.RandomNumber;

public class RandomIntNumberGenerator : IRandomNumberGenerator<int>
{
    private readonly Random _random = new Random();

    private readonly IReadOnlyRange<int> _range;

    public RandomIntNumberGenerator(IReadOnlyRange<int> range)
    {
        _range = range ?? throw new ArgumentNullException(nameof(range));
    }

    public int GetRandomNumber()
    {
        return _random.Next(_range.From, _range.To + 1);
    }
}
