using System;
using System.Linq;

namespace PrintableTable;

public class Table3DWith2Y<Y1Type, Y2Type, XType, DataType>
{
    public Y1Type[] Y1Values { get; }
    public Y2Type[] Y2Values { get; }
    public XType[] XValues { get; }
    public DataType[,,] DataCells { get; }

    public Table3DWith2Y(
        Y1Type[] y1Values,
        Y2Type[] y2Values,
        XType[] xValues)
            : this(
                  y1Values,
                  y2Values,
                  xValues,
                  new DataType[y1Values.Length, y2Values.Length, xValues.Length])
    {
    }

    public Table3DWith2Y(
        Y1Type[] y1Values,
        Y2Type[] y2Values,
        XType[] xValues,
        DataType[,,] dataCells)
    {
        Y1Values = y1Values ?? throw new ArgumentNullException(nameof(y1Values));
        Y2Values = y2Values ?? throw new ArgumentNullException(nameof(y2Values));
        XValues = xValues ?? throw new ArgumentNullException(nameof(xValues));
        DataCells = dataCells ?? throw new ArgumentNullException(nameof(dataCells));

        if (y1Values.Length == 0) throw new ArgumentException("Array is empty", nameof(y1Values));
        if (y2Values.Length == 0) throw new ArgumentException("Array is empty", nameof(y2Values));
        if (xValues.Length == 0) throw new ArgumentException("Array is empty", nameof(xValues));

        if (dataCells.GetLength(0) != y1Values.Length
            || dataCells.GetLength(1) != y2Values.Length
            || dataCells.GetLength(2) != xValues.Length)
            throw new ArgumentException("Incompatible arrays sizes", nameof(dataCells));
    }

    public void Print(IndentType bodyIndent = IndentType.IndentAround)
    {
        var table = new StringsTable4D(
                Y1Values.Select(e => e?.ToString() ?? "").ToArray(),
                Y2Values.Select(e => e?.ToString() ?? "").ToArray(),
                XValues.Select(e => e?.ToString() ?? "").ToArray(),
                null);

        for (var y1 = 0; y1 < Y1Values.Length; y1++)
        {
            for (var y2 = 0; y2 < Y2Values.Length; y2++)
            {
                for (var x1 = 0; x1 < XValues.Length; x1++)
                {
                    table[y1, y2, x1, 0] = DataCells[y1, y2, x1]?.ToString() ?? "";
                }
            }
        }

        table.Print(bodyIndent);
    }
}
