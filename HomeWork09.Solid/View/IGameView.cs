using HomeWork09.Solid.Model;

namespace HomeWork09.Solid.View;

public interface IGameView
{
    void SetGameModel(IGameModel model);

    void Run();

    void ReportThatNumberIsOutOfRange();

    void ReportThatNumberIsGreater();

    void ReportThatNumberIsSmaller();

    void ReportThatNumberIsGuessed();
}
