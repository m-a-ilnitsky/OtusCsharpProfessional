using System;
using System.Linq;

namespace PrintableTable;

internal class StringsTable4D
{
    public const char LightHorizontal = '\u2500';
    public const char LightVertical = '\u2502';

    public const char DoubleHorizontal = '\u2550';
    public const char DoubleVertical = '\u2551';

    protected readonly string[] X1Values;
    protected readonly string[] X2Values;
    protected readonly string[] Y1Values;
    protected readonly string[] Y2Values;

    protected readonly string[,,,] DataCells;

    protected readonly int[] LeftHeaderColumnsSizes;
    protected readonly int[] X1ColumnsSizes;
    protected readonly int[,] DataColumnsSizes;
    protected readonly int[] AllColumnsSizes;

    protected int LeftHeaderWidth => IsSingleY
        ? LeftHeaderColumnsSizes[0]
        : LeftHeaderColumnsSizes.Sum() + 1;
    protected int DataWidth => X1ColumnsSizes.Sum() + X1ColumnsSizes.Length - 1;
    protected int TotalWidth => AllColumnsSizes.Sum() + AllColumnsSizes.Length + 1;

    public int X1ValuesCount => X1Values.Length == 0 ? 1 : X1Values.Length;
    public int X2ValuesCount => X2Values.Length == 0 ? 1 : X2Values.Length;
    public int Y1ValuesCount => Y1Values.Length == 0 ? 1 : Y1Values.Length;
    public int Y2ValuesCount => Y2Values.Length == 0 ? 1 : Y2Values.Length;

    public bool IsSingleX => X2ValuesCount == 1;
    public bool IsSingleY => Y2ValuesCount == 1;

    public int DataRowsCount => Y1ValuesCount * Y2ValuesCount;
    public int DataColumnsCount => X1ValuesCount * X2ValuesCount;
    public int DataCellsCount => DataRowsCount * DataColumnsCount;

    public int AllRowsCount => DataRowsCount + (X2ValuesCount == 1 ? 1 : 2);
    public int AllColumnsCount => DataColumnsCount + (Y2ValuesCount == 1 ? 1 : 2);
    public int AllCellsCount => AllRowsCount * AllColumnsCount;

    public string this[int y1, int y2, int x1, int x2]
    {
        get => DataCells[y1, y2, x1, x2];
        set => DataCells[y1, y2, x1, x2] = value;
    }

    public StringsTable4D(
        string[] y1Values,
        string[]? y2Values,
        string[] x1Values,
        string[]? x2Values)
    {
        Y1Values = y1Values ?? throw new ArgumentNullException(nameof(y1Values));
        Y2Values = y2Values ?? [];
        X1Values = x1Values ?? throw new ArgumentNullException(nameof(x1Values));
        X2Values = x2Values ?? [];

        DataCells = new string[Y1ValuesCount, Y2ValuesCount, X1ValuesCount, X2ValuesCount];

        AllColumnsSizes = new int[AllColumnsCount];
        LeftHeaderColumnsSizes = IsSingleY ? new int[1] : new int[2];
        X1ColumnsSizes = new int[X1ValuesCount];
        DataColumnsSizes = new int[X1ValuesCount, X2ValuesCount];
    }

    public void Print(IndentType bodyIndent = IndentType.IndentLeft)
    {
        SetColumnsSizes();
        PrintTopHeader();
        PrintBody(bodyIndent);
    }

    private void SetColumnsSizes()
    {
        var columnIndex = 0;

        AllColumnsSizes[columnIndex] = Y1Values.Max(s => s.Length);
        LeftHeaderColumnsSizes[columnIndex] = AllColumnsSizes[columnIndex];
        columnIndex++;

        if (!IsSingleY)
        {
            AllColumnsSizes[columnIndex] = Y2Values.Max(s => s.Length);
            LeftHeaderColumnsSizes[columnIndex] = AllColumnsSizes[columnIndex];
            columnIndex++;
        }

        if (IsSingleX)
        {
            for (int i = 0; columnIndex < AllColumnsSizes.Length; i++, columnIndex++)
            {
                DataColumnsSizes[i, 0] = GetColumnMaxSize(i, 0);
                X1ColumnsSizes[i] = DataColumnsSizes[i, 0];
                AllColumnsSizes[columnIndex] = DataColumnsSizes[i, 0];
            }
        }
        else
        {
            for (int x1 = 0; x1 < X1ValuesCount; x1++)
            {
                X1ColumnsSizes[x1] = 0;

                for (int x2 = 0; x2 < X2ValuesCount; x2++)
                {
                    DataColumnsSizes[x1, x2] = GetColumnMaxSize(x1, x2);
                    X1ColumnsSizes[x1] += DataColumnsSizes[x1, x2];
                    AllColumnsSizes[columnIndex] = DataColumnsSizes[x1, x2];
                    columnIndex++;
                }

                X1ColumnsSizes[x1] += X2ValuesCount - 1;
            }
        }
    }

    private int GetColumnMaxSize(int x1, int x2)
    {
        var max = GetDataColumnMaxSize(x1, x2);

        if (X1Values[x1].Length > max)
        {
            max = X1Values[x1].Length;
        }

        if (!IsSingleX && X2Values[x2].Length > max)
        {
            max = X2Values[x2].Length;
        }

        return max;
    }

    private int GetDataColumnMaxSize(int x1, int x2)
    {
        var max = 0;

        for (int y1 = 0; y1 < Y1ValuesCount; y1++)
        {
            for (int y2 = 0; y2 < Y2ValuesCount; y2++)
            {
                if (DataCells[y1, y2, x1, x2]?.Length > max)
                {
                    max = DataCells[y1, y2, x1, x2].Length;
                }
            }
        }

        return max;
    }

    private static string PadAround(string line, int width)
    {
        if (line is null)
        {
            return "".PadRight(width);
        }
        if (line.Length == width)
        {
            return line;
        }
        if (line.Length > width)
        {
            return line[..width];
        }

        var diff = width - line.Length;
        var rightPad = diff / 2;

        return line.PadRight(line.Length + rightPad).PadLeft(width);
    }

    private void PrintTopHeader()
    {
        PrintTableHorizontalLine();

        PrintLeftHeader();
        for (int x1 = 0; x1 < X1ValuesCount; x1++)
        {
            PrintTopHeaderValue(PadAround(X1Values[x1], X1ColumnsSizes[x1]));
            Console.Write(DoubleVertical);
        }
        Console.WriteLine();

        if (!IsSingleX)
        {
            var havyLine = "".PadRight(DataWidth, LightHorizontal);

            PrintLeftHeader();
            Console.Write(havyLine);
            Console.WriteLine(DoubleVertical);

            PrintLeftHeader();
            for (int x1 = 0; x1 < X1ValuesCount; x1++)
            {
                for (int x2 = 0; x2 < X2ValuesCount; x2++)
                {
                    PrintTopHeaderValue(PadAround(X2Values[x2], DataColumnsSizes[x1, x2]));

                    if (X2ValuesCount - 1 != x2)
                    {
                        Console.Write(LightVertical);
                    }
                    else
                    {
                        Console.Write(DoubleVertical);
                    }
                }
            }
            Console.WriteLine();
        }

        PrintTableHorizontalLine();
    }

    private void PrintTableHorizontalLine()
    {
        var doubleLine = "".PadRight(TotalWidth, DoubleHorizontal);
        Console.WriteLine(doubleLine);
    }

    private void PrintLeftHeaderHorizontalLine(int y2)
    {
        Console.Write(DoubleVertical);

        if (y2 + 1 == Y2ValuesCount)
        {
            Console.Write("".PadRight(LeftHeaderWidth, DoubleHorizontal));
        }
        else
        {
            Console.Write("".PadRight(LeftHeaderColumnsSizes[0]));
            Console.Write(LightVertical);
            Console.Write("".PadRight(LeftHeaderColumnsSizes[1], LightHorizontal));
        }

        Console.Write(DoubleVertical);
    }

    private void PrintBodyHorizontalLine(int y2)
    {
        if (y2 + 1 == Y2ValuesCount)
        {
            Console.Write("".PadRight(DataWidth, DoubleHorizontal));
        }
        else
        {
            Console.Write("".PadRight(DataWidth, LightHorizontal));
        }

        Console.WriteLine(DoubleVertical);
    }

    private void PrintLeftHeader(string? y1Value = null, string? y2Value = null)
    {
        Console.Write(DoubleVertical);

        if (y1Value is null && y2Value is null)
        {
            Console.Write("".PadLeft(LeftHeaderWidth));
        }
        else
        {
            var line = (y1Value ?? "").PadLeft(AllColumnsSizes[0]);
            PrintLeftHeaderValue(line);

            if (!IsSingleY)
            {
                Console.Write(LightVertical);
                line = (y2Value ?? "").PadLeft(AllColumnsSizes[1]);
                PrintLeftHeaderValue(line);
            }
        }

        Console.Write(DoubleVertical);
    }

    private static void PrintLeftHeaderValue(string value)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(value);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    private static void PrintTopHeaderValue(string value)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(value);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    private void PrintBody(IndentType bodyIndent)
    {
        if (IsSingleY)
        {
            PrintBodyWithSingleY(bodyIndent);
        }
        else
        {
            PrintBodyWithTwoY(bodyIndent);
        }

        PrintTableHorizontalLine();
    }

    private void PrintBodyWithSingleY(IndentType bodyIndent)
    {
        for (int y1 = 0; y1 < Y1ValuesCount; y1++)
        {
            PrintLeftHeader(Y1Values[y1]);
            PrintDataLine(y1, 0, bodyIndent);

            if (y1 + 1 < Y1ValuesCount)
            {
                PrintLeftHeaderHorizontalLine(0);
                PrintBodyHorizontalLine(0);
            }
        }
    }

    private void PrintBodyWithTwoY(IndentType bodyIndent)
    {
        for (int y1 = 0; y1 < Y1ValuesCount; y1++)
        {
            for (int y2 = 0; y2 < Y2ValuesCount; y2++)
            {
                if (y2 == 0)
                {
                    PrintLeftHeader(Y1Values[y1], Y2Values[y2]);
                }
                else
                {
                    PrintLeftHeader(null, Y2Values[y2]);
                }

                PrintDataLine(y1, y2, bodyIndent);

                if (y1 + 1 < Y1ValuesCount || y2 + 1 < Y2ValuesCount)
                {
                    PrintLeftHeaderHorizontalLine(y2);
                    PrintBodyHorizontalLine(y2);
                }
            }
        }
    }

    private void PrintDataLine(int y1, int y2, IndentType bodyIndent)
    {
        if (IsSingleX)
        {
            PrintDataLineForSingleX(y1, y2, bodyIndent);
        }
        else
        {
            PrintDataLineForTwoX(y1, y2, bodyIndent);
        }
    }

    private void PrintDataLineForSingleX(int y1, int y2, IndentType bodyIndent)
    {
        for (int x1 = 0; x1 < X1ValuesCount; x1++)
        {
            PrintCellValue(DataCells[y1, y2, x1, 0], X1ColumnsSizes[x1], bodyIndent);
            Console.Write(DoubleVertical);
        }

        Console.WriteLine();
    }

    private void PrintDataLineForTwoX(int y1, int y2, IndentType bodyIndent)
    {
        for (int x1 = 0; x1 < X1ValuesCount; x1++)
        {
            for (int x2 = 0; x2 < X2ValuesCount; x2++)
            {
                PrintCellValue(DataCells[y1, y2, x1, x2], DataColumnsSizes[x1, x2], bodyIndent);

                if (x2 + 1 < X2ValuesCount)
                {
                    Console.Write(LightVertical);
                }
            }

            if (x1 + 1 < X1ValuesCount)
            {
                Console.Write(DoubleVertical);
            }
        }

        Console.WriteLine(DoubleVertical);
    }

    private static void PrintCellValue(string line, int width, IndentType bodyIndent)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(GetWithIndent(line, width, bodyIndent));
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    private static string GetWithIndent(string line, int width, IndentType bodyIndent)
    {
        return bodyIndent switch
        {
            IndentType.IndentLeft => line.PadLeft(width),
            IndentType.IndentRight => line.PadRight(width),
            IndentType.IndentAround => PadAround(line, width),
            _ => throw new NotImplementedException()
        };
    }
}
