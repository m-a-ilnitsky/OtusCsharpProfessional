using HomeWork09.Solid.Model;

namespace HomeWork09.Solid.View;

internal class ConsoleView : IGameView
{
    private IGameModel? _model;
    private bool _isFound;

    private IGameModel Model => _model ?? throw new NullReferenceException();

    public void SetGameModel(IGameModel model)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
    }

    public void ReportThatNumberIsOutOfRange()
    {
        Console.WriteLine();
        Console.WriteLine("Число {0} находится вне заданного диапазона ({1}, {2})!", Model.UserNumber, Model.MinNumber, Model.MaxNumber);
    }

    public void ReportThatNumberIsGreater()
    {
        Console.WriteLine();
        Console.WriteLine("Число {0} больше загаданного числа!", Model.UserNumber);
    }

    public void ReportThatNumberIsSmaller()
    {
        Console.WriteLine();
        Console.WriteLine("Число {0} меньше загаданного числа!", Model.UserNumber);
    }

    public void ReportThatNumberIsGuessed()
    {
        _isFound = true;
        Console.WriteLine();
        Console.WriteLine("Число {0} равно загаданному числу.", Model.UserNumber);
        Console.WriteLine("Вы угадали число с {0} попытки!", Model.AttemptsCount);
    }

    private int GetUserNumber()
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

    private bool ExitFromGame()
    {
        Console.WriteLine();
        Console.WriteLine("Введите 'exit' чтобы выйти из программы:");
        var userString = Console.ReadLine()?.Trim().ToLower();

        return userString == "exit" || userString == "учше";
    }

    private void CheckUserNumber()
    {
        Model.CheckUserNumber(GetUserNumber());
    }

    private void ShowGameInfo()
    {
        Console.WriteLine();
        Console.WriteLine("Программа загадывает целое число в диапазоне от ({0}, {1}).", Model.MinNumber, Model.MaxNumber);
        Console.WriteLine("Ваше задача угадать число за минимальное количество попыток");
    }

    public void Run()
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
