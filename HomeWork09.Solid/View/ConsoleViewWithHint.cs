namespace HomeWork09.Solid.View;

public class ConsoleViewWithHint : ConsoleView
{
    protected override int GetUserNumber()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Попытка #{0}", Model.AttemptsCount + 1);
            Console.WriteLine("Введите целое число в диапазоне ({0}, {1}) или ? для подсказки:", Model.MinNumber, Model.MaxNumber);
            var userString = Console.ReadLine();

            if (userString?.Trim() == "?")
            {
                Console.WriteLine("Введите целое число в диапазоне ({0}, {1}):", Model.CurrentMinNumber, Model.CurrentMaxNumber);
                userString = Console.ReadLine();
            }

            if (int.TryParse(userString, out int userNumber))
            {
                return userNumber;
            }

            Console.WriteLine("Нужно ввести целое число!");
        }
    }
}
