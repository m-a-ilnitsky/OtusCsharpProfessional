using System;
using System.Linq;

namespace PrintableTable;

public class Table2D<YType, XType, DataType>
{
    public YType[] YValues { get; }
    public XType[] XValues { get; }
    public DataType[,] DataCells { get; }

    public Table2D(
        YType[] yValues,
        XType[] xValues)
            : this(
                  yValues,
                  xValues,
                  new DataType[yValues.Length, xValues.Length])
    {
    }

    public Table2D(
        YType[] yValues,
        XType[] xValues,
        DataType[,] dataCells)
    {
        YValues = yValues ?? throw new ArgumentNullException(nameof(yValues));
        XValues = xValues ?? throw new ArgumentNullException(nameof(xValues));
        DataCells = dataCells ?? throw new ArgumentNullException(nameof(dataCells));

        if (yValues.Length == 0) throw new ArgumentException("Array is empty", nameof(yValues));
        if (xValues.Length == 0) throw new ArgumentException("Array is empty", nameof(xValues));

        if (dataCells.GetLength(0) != yValues.Length
            || dataCells.GetLength(1) != xValues.Length)
            throw new ArgumentException("Incompatible arrays sizes", nameof(dataCells));
    }

    public void Print(IndentType bodyIndent = IndentType.IndentLeft)
    {
        var table = new StringsTable4D(
                YValues.Select(e => e?.ToString() ?? "").ToArray(),
                null,
                XValues.Select(e => e?.ToString() ?? "").ToArray(),
                null);

        for (var y1 = 0; y1 < YValues.Length; y1++)
        {
            for (var x1 = 0; x1 < XValues.Length; x1++)
            {
                table[y1, 0, x1, 0] = DataCells[y1, x1]?.ToString() ?? "";
            }
        }

        table.Print(bodyIndent);
    }
}
