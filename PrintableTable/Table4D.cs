using System;
using System.Linq;

namespace PrintableTable;

public class Table4D<Y1Type, Y2Type, X1Type, X2Type, DataType>
{
    public Y1Type[] Y1Values { get; }
    public Y2Type[] Y2Values { get; }
    public X1Type[] X1Values { get; }
    public X2Type[] X2Values { get; }
    public DataType[,,,] DataCells { get; }

    public Table4D(
        Y1Type[] y1Values,
        Y2Type[] y2Values,
        X1Type[] x1Values,
        X2Type[] x2Values)
            : this(
                  y1Values,
                  y2Values,
                  x1Values,
                  x2Values,
                  new DataType[y1Values.Length, y2Values.Length, x1Values.Length, x2Values.Length])
    {
    }

    public Table4D(
        Y1Type[] y1Values,
        Y2Type[] y2Values,
        X1Type[] x1Values,
        X2Type[] x2Values,
        DataType[,,,] dataCells)
    {
        Y1Values = y1Values ?? throw new ArgumentNullException(nameof(y1Values));
        Y2Values = y2Values ?? throw new ArgumentNullException(nameof(y2Values));
        X1Values = x1Values ?? throw new ArgumentNullException(nameof(x1Values));
        X2Values = x2Values ?? throw new ArgumentNullException(nameof(x2Values));
        DataCells = dataCells ?? throw new ArgumentNullException(nameof(dataCells));

        if (y1Values.Length == 0) throw new ArgumentException("Array is empty", nameof(y1Values));
        if (y2Values.Length == 0) throw new ArgumentException("Array is empty", nameof(y2Values));
        if (x1Values.Length == 0) throw new ArgumentException("Array is empty", nameof(x1Values));
        if (x2Values.Length == 0) throw new ArgumentException("Array is empty", nameof(x2Values));

        if (dataCells.GetLength(0) != y1Values.Length
            || dataCells.GetLength(1) != y2Values.Length
            || dataCells.GetLength(2) != x1Values.Length
            || dataCells.GetLength(3) != x2Values.Length)
            throw new ArgumentException("Incompatible arrays sizes", nameof(dataCells));
    }

    public void Print(IndentType bodyIndent = IndentType.IndentLeft)
    {
        var table = new StringsTable4D(
                Y1Values.Select(e => e?.ToString() ?? "").ToArray(),
                Y2Values.Select(e => e?.ToString() ?? "").ToArray(),
                X1Values.Select(e => e?.ToString() ?? "").ToArray(),
                X2Values.Select(e => e?.ToString() ?? "").ToArray());

        for (var y1 = 0; y1 < Y1Values.Length; y1++)
        {
            for (var y2 = 0; y2 < Y2Values.Length; y2++)
            {
                for (var x1 = 0; x1 < X1Values.Length; x1++)
                {
                    for (var x2 = 0; x2 < X2Values.Length; x2++)
                    {
                        table[y1, y2, x1, x2] = DataCells[y1, y2, x1, x2]?.ToString() ?? "";
                    }
                }
            }
        }

        table.Print(bodyIndent);
    }
}
