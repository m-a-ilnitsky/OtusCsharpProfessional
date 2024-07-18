using System.Numerics;

namespace HomeWork09.Solid.Model.Ranges;

public interface IRange<T> : IReadOnlyRange<T> where T : INumber<T>
{
    new T From { get; set; }

    new T To { get; set; }
}
