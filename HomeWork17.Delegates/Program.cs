using HomeWork17.Delegates.Extensions;
using HomeWork17.Delegates.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

var pointsArray = new Point2D<int>[]
{
    new(3, -3),
    new(0, -7),
    new(5, -5),
    new(0, -3),
    new(5, -3),
    new(3, -7)
};

var pointsList = new List<Point2D<int>>(pointsArray);
ICollection<Point2D<int>> pointsCollection = [.. pointsArray];
IEnumerable<Point2D<int>> pointsEnumeration = [.. pointsArray];
Point2D<int>[] emptyArray = [];

var vectors = new Vector3D<double>[]
{
    new(0, 0, 3),
    new(0, 3, 0),
    new(3, 0, 0),
    new(0, 0, 7),
    new(0, 7, 0),
    new(7, 0, 0)
};

var vectorsAndNulls = new Vector3D<double>[]
{
    null,
    new(1, 0, 0),
    null,
    new(0, 3, 0),
    null,
    new(0, 0, 5),
    null,
    new(7, 0, 0),
    null
};

static int GetY(Point2D<int> point)
{
    return point.Y;
}

Func<Point2D<int>, int> getPointY = GetY;
Func<Point2D<int>, int> getPointX = point => point.X;

var getX = getPointX;

static void WriteMaxNumberAndItems<TItem, TNumber>(
    IEnumerable<TItem> items,
    Func<TItem, TNumber> convertToNumber,
    bool skipNull = true)
        where TItem : class
        where TNumber : struct,
                        INumber<TNumber>,
                        IMinMaxValue<TNumber>,
                        IComparisonOperators<TNumber, TNumber, bool>
{
    Console.WriteLine($"Коллекция: [{string.Join(", ", items.Select(e => e?.ToString() ?? "null"))}]");
    Console.WriteLine($"Максимальное число: {items.GetMaxNumber<TItem, TNumber>(convertToNumber, skipNull)}");
    Console.WriteLine($"Первый максимальный элемент: {items.GetMaxItem<TItem, TNumber>(convertToNumber, skipNull)}");
    Console.WriteLine($"Все максимальные элементы: [{string.Join(", ", items.GetMaxItems<TItem, TNumber>(convertToNumber, skipNull))}]");
    Console.WriteLine();
}

WriteMaxNumberAndItems(pointsArray, getX);
WriteMaxNumberAndItems(pointsList, getX);
WriteMaxNumberAndItems(pointsCollection, getX);
WriteMaxNumberAndItems(pointsEnumeration, getX);

WriteMaxNumberAndItems(pointsArray, getPointY);

WriteMaxNumberAndItems(emptyArray, getPointY);

WriteMaxNumberAndItems(vectors, v => v.GetLength());

WriteMaxNumberAndItems(vectorsAndNulls, v => v.GetLength());
WriteMaxNumberAndItems(vectorsAndNulls, v => v?.GetLength() ?? 7e7, false);
