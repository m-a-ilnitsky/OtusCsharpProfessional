namespace HomeWork09.Solid.Model;

public interface IGameModel
{
    int AttemptsCount { get; }

    int UserNumber { get; }

    int MinNumber { get; }

    int MaxNumber { get; }

    void ResetGame();

    void CheckUserNumber(int number);
}
