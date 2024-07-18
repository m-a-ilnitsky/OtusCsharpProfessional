using System.Numerics;

namespace HomeWork09.Solid.Model.Ranges;

public interface IReadOnlyRange<T> : ICloneable where T : INumber<T>
{
    T From { get; }

    T To { get; }
}
