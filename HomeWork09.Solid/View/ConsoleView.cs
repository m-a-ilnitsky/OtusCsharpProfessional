using HomeWork09.Solid.Model;

namespace HomeWork09.Solid.View;

public class ConsoleView : IGameView
{
    private IGameModel? _model;
    private bool _isFound;

    protected virtual IGameModel Model => _model ?? throw new NullReferenceException();

    public virtual void SetModel(IGameModel model)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
    }

    public virtual void ReportThatNumberIsOutOfRange()
    {
        Console.WriteLine();
        Console.WriteLine("Число {0} находится вне заданного диапазона ({1}, {2})!", Model.UserNumber, Model.MinNumber, Model.MaxNumber);
    }

    public virtual void ReportThatNumberIsGreater()
    {
        Console.WriteLine();
        Console.WriteLine("Число {0} больше загаданного числа!", Model.UserNumber);
    }

    public virtual void ReportThatNumberIsSmaller()
    {
        Console.WriteLine();
        Console.WriteLine("Число {0} меньше загаданного числа!", Model.UserNumber);
    }

    public virtual void ReportThatNumberIsGuessed()
    {
        _isFound = true;
        Console.WriteLine();
        Console.WriteLine("Число {0} равно загаданному числу.", Model.UserNumber);
        Console.WriteLine("Вы угадали число с {0} попытки!", Model.AttemptsCount);
    }

    protected virtual int GetUserNumber()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Попытка #{0}", Model.AttemptsCount + 1);
            Console.WriteLine("Введите целое число в диапазоне ({0}, {1}):", Model.MinNumber, Model.MaxNumber);
            var userString = Console.ReadLine();

            if (int.TryParse(userString, out int userNumber))
            {
                return userNumber;
            }

            Console.WriteLine("Нужно ввести целое число!");
        }
    }

    protected virtual bool ExitFromGame()
    {
        Console.WriteLine();
        Console.WriteLine("Введите 'exit' чтобы выйти из программы:");
        var userString = Console.ReadLine()?.Trim().ToLower();

        return userString == "exit" || userString == "учше";
    }

    protected virtual void CheckUserNumber()
    {
        Model.CheckUserNumber(GetUserNumber());
    }

    protected virtual void ShowGameInfo()
    {
        Console.WriteLine();
        Console.WriteLine("Программа загадывает целое число в диапазоне от ({0}, {1}).", Model.MinNumber, Model.MaxNumber);
        Console.WriteLine("Ваше задача угадать число за минимальное количество попыток");
    }

    public virtual void Run()
    {
        var exit = false;

        while (!exit)
        {
            _isFound = false;
            Model.ResetGame();
            ShowGameInfo();

            while (!_isFound)
            {
                CheckUserNumber();
            }

            exit = ExitFromGame();
        }

        Console.WriteLine("Работа программы завершена!");
    }
}
