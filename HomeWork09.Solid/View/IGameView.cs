using HomeWork09.Solid.Model;

namespace HomeWork09.Solid.View;

public interface IGameView : IRunable, IModelSettable<IGameModel>
{
    void ReportThatNumberIsOutOfRange();

    void ReportThatNumberIsGreater();

    void ReportThatNumberIsSmaller();

    void ReportThatNumberIsGuessed();
}
