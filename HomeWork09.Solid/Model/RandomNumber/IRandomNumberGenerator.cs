using System.Numerics;

namespace HomeWork09.Solid.Model.RandomNumber;

public interface IRandomNumberGenerator<T> where T : INumber<T>
{
    T GetRandomNumber();
}
