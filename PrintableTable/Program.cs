using PrintableTable;
using System;

var stringsTable4D = new StringsTable4D(
    ["y1-1", "y1-2", "y1-3"],
    ["y2-1", "y2-2"],
    ["x1-1", "x1-2", "x1-3", "x1-4"],
    ["x2-1", "x2-2", "x2-3"]);

for (var y1 = 0; y1 < 3; y1++)
{
    for (var y2 = 0; y2 < 2; y2++)
    {
        for (var x1 = 0; x1 < 4; x1++)
        {
            for (var x2 = 0; x2 < 3; x2++)
            {
                stringsTable4D[y1, y2, x1, x2] = $"{y1}-{y2}-{x1}-{x2}";
            }
        }
    }
}

stringsTable4D.Print();
Console.WriteLine();

var stringsTable2D = new StringsTable4D(
    ["y-1", "y-2", "y-3"],
    null,
    ["x-1", "x-2", "x-3"],
    null);

for (var y1 = 0; y1 < 3; y1++)
{
    for (var x1 = 0; x1 < 3; x1++)
    {
        stringsTable2D[y1, 0, x1, 0] = $"-{y1}-{x1}-";
    }
}

stringsTable2D.Print();
Console.WriteLine();

var stringsTable3D2X = new StringsTable4D(
    ["y1-1", "y1-2", "y1-3"],
    null,
    ["x1-1", "x1-2", "x1-3"],
    ["x2-1", "x2-2"]);

for (var y1 = 0; y1 < 3; y1++)
{
    for (var x1 = 0; x1 < 3; x1++)
    {
        for (var x2 = 0; x2 < 2; x2++)
        {
            stringsTable3D2X[y1, 0, x1, x2] = $"{y1}-{0}-{x1}-{x2}";
        }
    }
}

stringsTable3D2X.Print();
Console.WriteLine();

var stringsTable3D2Y = new StringsTable4D(
    ["y1-1", "y1-2", "y1-3"],
    ["y2-1", "y2-2"],
    ["x1-1", "x1-2", "x1-3", "x1-4"],
    null);

for (var y1 = 0; y1 < 3; y1++)
{
    for (var y2 = 0; y2 < 2; y2++)
    {
        for (var x1 = 0; x1 < 4; x1++)
        {
            stringsTable3D2Y[y1, y2, x1, 0] = $" {y1}-{y2}-{x1} ";
        }
    }
}

stringsTable3D2Y.Print();
Console.WriteLine();


var table4D = new Table4D<int, int, double, int, double>(
    [2, 3, 5, 7],
    [2, 4, 8],
    [0.1, 0.2, 0.3],
    [3, 5, 7]);

for (var y1 = 0; y1 < table4D.Y1Values.Length; y1++)
{
    for (var y2 = 0; y2 < table4D.Y2Values.Length; y2++)
    {
        for (var x1 = 0; x1 < table4D.X1Values.Length; x1++)
        {
            for (var x2 = 0; x2 < table4D.X2Values.Length; x2++)
            {
                var value = (table4D.Y1Values[y1] - table4D.Y2Values[y2]) / table4D.X1Values[x1] * table4D.X2Values[x2];
                table4D.DataCells[y1, y2, x1, x2] = Math.Round(value, 1);
            }
        }
    }
}

Console.WriteLine("Test Table4D");
table4D.Print();
Console.WriteLine();

var table2D = new Table2D<int, int, double>(
    [1, 2, 3, 4, 5, 6, 7],
    [1, 2, 3, 4, 5, 6, 7]);

for (var y1 = 0; y1 < table2D.YValues.Length; y1++)
{
    for (var x1 = 0; x1 < table2D.XValues.Length; x1++)
    {
        table2D.DataCells[y1, x1] = Math.Pow(table2D.YValues[y1], table2D.XValues[x1]);
    }
}

Console.WriteLine("Test Table2D");
table2D.Print();
Console.WriteLine();

var table3D2X = new Table3DWith2X<int, int, int, int>(
    [1, 2, 3, 4],
    [1, 2, 3, 4],
    [1, 2, 3, 4]);

for (var y1 = 0; y1 < table3D2X.YValues.Length; y1++)
{
    for (var x1 = 0; x1 < table3D2X.X1Values.Length; x1++)
    {
        for (var x2 = 0; x2 < table3D2X.X2Values.Length; x2++)
        {
            table3D2X.DataCells[y1, x1, x2] = table3D2X.YValues[y1] + table3D2X.X1Values[x1] * table3D2X.X2Values[x2];
        }
    }
}

Console.WriteLine("Test Table3D2X");
table3D2X.Print();
Console.WriteLine();

var table3D2Y = new Table3DWith2Y<int, int, int, int>(
    [1, 2, 3, 4],
    [1, 11],
    [1, 2, 3, 4]);

for (var y1 = 0; y1 < table3D2Y.Y1Values.Length; y1++)
{
    for (var y2 = 0; y2 < table3D2Y.Y2Values.Length; y2++)
    {
        for (var x1 = 0; x1 < table3D2Y.XValues.Length; x1++)
        {
            table3D2Y.DataCells[y1, y2, x1] = table3D2Y.Y1Values[y1] + table3D2Y.Y2Values[y2] + table3D2Y.XValues[x1];
        }
    }
}

Console.WriteLine("Test Table3D2Y");
table3D2Y.Print();
Console.WriteLine();