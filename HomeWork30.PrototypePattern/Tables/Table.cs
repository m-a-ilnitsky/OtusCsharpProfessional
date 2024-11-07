using System;
using System.Text;

namespace HomeWork30.PrototypePattern.Tables;

/// <summary>
/// Прямоугольная таблица элементов одинакового типа
/// </summary>
public class Table<TItem> : ICopyable<Table<TItem>>, ICloneable
{
    protected TItem[,] Items;

    public int RowsCount => Items.GetLength(0);

    public int ColumnsCount => Items.GetLength(1);

    public TItem this[int rowIndex, int columnIndex] => Items[rowIndex, columnIndex];

    public TItem[] this[int rowIndex] => GetRow(rowIndex);

    public Table(TItem[,] items)
    {
        if (items.GetLength(0) <= 0 || items.GetLength(1) <= 0)
        {
            throw new ArgumentException($"Оба размера ({items.GetLength(0)}, {items.GetLength(1)}) должны быть больше 0");
        }

        Items = (TItem[,])(items?.Clone() ?? throw new ArgumentNullException(nameof(items)));
    }

    public TItem[] GetRow(int rowIndex)
    {
        var row = new TItem[ColumnsCount];

        for (var i = 0; i < ColumnsCount; i++)
        {
            row[i] = Items[rowIndex, i];
        }

        return row;
    }

    public TItem[] GetColumn(int columnIndex)
    {
        var column = new TItem[RowsCount];

        for (var i = 0; i < RowsCount; i++)
        {
            column[i] = Items[i, columnIndex];
        }

        return column;
    }

    public TItem[,] GetItems() => (TItem[,])Items.Clone();

    public override string ToString()
    {
        var widths = new int[ColumnsCount];

        for (var i = 0; i < ColumnsCount; i++)
        {
            for (var j = 0; j < RowsCount; j++)
            {
                var str = Items[j, i]?.ToString() ?? "null";
                widths[i] = Math.Max(widths[i], str.Length);
            }
        }

        var sb = new StringBuilder();

        for (var i = 0; i < RowsCount; i++)
        {
            sb.Append('|');
            for (var j = 0; j < ColumnsCount; j++)
            {
                var str = (Items[i, j]?.ToString() ?? "null").PadLeft(widths[j] + 1);
                sb.Append(str);
            }
            sb.Append(" |").Append(Environment.NewLine);
        }

        return sb.ToString();
    }

    public string ToLine()
    {
        var sb = new StringBuilder("[");

        for (var i = 0; i < RowsCount; i++)
        {
            sb.Append(string.Join(", ", GetRow(i)));
        }

        sb.Append(']');

        return sb.ToString();
    }

    public virtual Table<TItem> Copy() => new Table<TItem>(Items);

    public virtual object Clone() => Copy();
}
