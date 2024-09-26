using System;
using System.Linq;

namespace PrintableTable;

public class Table3DWith2X<YType, X1Type, X2Type, DataType>
{
    public YType[] YValues { get; }
    public X1Type[] X1Values { get; }
    public X2Type[] X2Values { get; }
    public DataType[,,] DataCells { get; }

    public Table3DWith2X(
        YType[] yValues,
        X1Type[] x1Values,
        X2Type[] x2Values)
            : this(
                  yValues,
                  x1Values,
                  x2Values,
                  new DataType[yValues.Length, x1Values.Length, x2Values.Length])
    {
    }

    public Table3DWith2X(
        YType[] yValues,
        X1Type[] x1Values,
        X2Type[] x2Values,
        DataType[,,] dataCells)
    {
        YValues = yValues ?? throw new ArgumentNullException(nameof(yValues));
        X1Values = x1Values ?? throw new ArgumentNullException(nameof(x1Values));
        X2Values = x2Values ?? throw new ArgumentNullException(nameof(x2Values));
        DataCells = dataCells ?? throw new ArgumentNullException(nameof(dataCells));

        if (yValues.Length == 0) throw new ArgumentException("Array is empty", nameof(yValues));
        if (x1Values.Length == 0) throw new ArgumentException("Array is empty", nameof(x1Values));
        if (x2Values.Length == 0) throw new ArgumentException("Array is empty", nameof(x2Values));

        if (dataCells.GetLength(0) != yValues.Length
            || dataCells.GetLength(1) != x1Values.Length
            || dataCells.GetLength(2) != x2Values.Length)
            throw new ArgumentException("Incompatible arrays sizes", nameof(dataCells));
    }

    public void Print(IndentType bodyIndent = IndentType.IndentLeft)
    {
        var table = new StringsTable4D(
                YValues.Select(e => e?.ToString() ?? "").ToArray(),
                null,
                X1Values.Select(e => e?.ToString() ?? "").ToArray(),
                X2Values.Select(e => e?.ToString() ?? "").ToArray());

        for (var y1 = 0; y1 < YValues.Length; y1++)
        {
            for (var x1 = 0; x1 < X1Values.Length; x1++)
            {
                for (var x2 = 0; x2 < X2Values.Length; x2++)
                {
                    table[y1, 0, x1, x2] = DataCells[y1, x1, x2]?.ToString() ?? "";
                }
            }
        }

        table.Print(bodyIndent);
    }
}
