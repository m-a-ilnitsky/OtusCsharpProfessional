using HomeWork09.Solid.Model.RandomNumber;
using HomeWork09.Solid.Model.Ranges;
using HomeWork09.Solid.View;

namespace HomeWork09.Solid.Model;

public class GuessNumberGameModel : IGameModel
{
    private readonly IRandomNumberGenerator<int> _randomNumberGenerator;
    private readonly IReadOnlyRange<int> _initialNumbersRange;
    private readonly IGameView _view;

    private IRange<int> _currentNumbersRange;
    private int _hiddenNumber;

    public int AttemptsCount { get; private set; }

    public int UserNumber { get; private set; }

    public int MinNumber => _initialNumbersRange.From;

    public int MaxNumber => _initialNumbersRange.To;

    public int CurrentMinNumber => _currentNumbersRange.From;

    public int CurrentMaxNumber => _currentNumbersRange.To;

    public GuessNumberGameModel(
        IGameView view,
        IRange<int> numbersRange,
        IRandomNumberGenerator<int> randomNumberGenerator)
    {
        _view = view ?? throw new ArgumentNullException(nameof(view));
        _randomNumberGenerator = randomNumberGenerator ?? throw new ArgumentNullException(nameof(randomNumberGenerator));
        _initialNumbersRange = numbersRange ?? throw new ArgumentNullException(nameof(numbersRange));

        _currentNumbersRange = (IRange<int>)_initialNumbersRange.Clone();

        _view.SetModel(this);

        ResetGame();
    }

    public void ResetGame()
    {
        _currentNumbersRange = (IRange<int>)_initialNumbersRange.Clone();
        _hiddenNumber = _randomNumberGenerator.GetRandomNumber();

        AttemptsCount = 0;
        UserNumber = MinNumber - 1;
    }

    public void CheckUserNumber(int number)
    {
        UserNumber = number;
        AttemptsCount++;

        if (UserNumber < MinNumber || UserNumber > MaxNumber)
        {
            _view.ReportThatNumberIsOutOfRange();
        }
        else if (UserNumber < _hiddenNumber)
        {
            _currentNumbersRange.From = UserNumber + 1;
            _view.ReportThatNumberIsSmaller();
        }
        else if (UserNumber > _hiddenNumber)
        {
            _currentNumbersRange.To = UserNumber - 1;
            _view.ReportThatNumberIsGreater();
        }
        else
        {
            _view.ReportThatNumberIsGuessed();
        }
    }
}
