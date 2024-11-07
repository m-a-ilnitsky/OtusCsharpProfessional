using System;
using System.Numerics;

namespace HomeWork30.PrototypePattern.Tables;

/// <summary>
/// Матрица чисел
/// </summary>
public class Matrix<TNumber> : Table<TNumber>, ICopyable<Matrix<TNumber>>, ICopyable<Table<TNumber>>, ICloneable
        where TNumber : struct, INumber<TNumber>
{
    public Matrix(TNumber[,] items) : base(items)
    {
        if (items.GetLength(0) <= 0 || items.GetLength(1) <= 0)
        {
            throw new ArgumentException($"Оба размера матрицы ({items.GetLength(0)}, {items.GetLength(1)}) должны быть больше 0");
        }
    }

    public Matrix(Matrix<TNumber> matrix) : this(matrix.GetItems()) { }

    public Matrix(int rowsCount, int columsCount) : this(new TNumber[rowsCount, columsCount]) { }

    public Matrix<TNumber> GetProduct(Matrix<TNumber> matrix)
    {
        if (ColumnsCount != matrix.RowsCount)
        {
            throw new ArgumentException($"Количество столбцов ({ColumnsCount}) левой матрицы не равно колиичеству строк ({matrix.RowsCount}) правой матрицы");
        }

        TNumber[,] resultItems = new TNumber[RowsCount, matrix.ColumnsCount];

        for (int i = 0; i < RowsCount; i++)
        {
            for (int j = 0; j < matrix.ColumnsCount; j++)
            {
                TNumber sum = default;

                for (int k = 0; k < ColumnsCount; k++)
                {
                    sum += Items[i, k] * matrix.Items[k, j];
                }

                resultItems[i, j] = sum;
            }
        }

        return new Matrix<TNumber>(resultItems);
    }

    public Matrix<TNumber> GetProduct(TNumber scalar)
    {
        TNumber[,] resultItems = new TNumber[RowsCount, ColumnsCount];

        for (int i = 0; i < RowsCount; i++)
        {
            for (int j = 0; j < ColumnsCount; j++)
            {
                resultItems[i, j] *= scalar;
            }
        }

        return new Matrix<TNumber>(resultItems);
    }

    public Matrix<TNumber> GetTransposed()
    {
        var transposedItems = new TNumber[ColumnsCount, RowsCount];

        for (int i = 0; i < ColumnsCount; i++)
        {
            for (int j = 0; j < RowsCount; j++)
            {
                transposedItems[i, j] = Items[j, i];
            }
        }

        return new Matrix<TNumber>(transposedItems);
    }

    public override Matrix<TNumber> Copy() => new(Items);

    Table<TNumber> ICopyable<Table<TNumber>>.Copy() => new(Items);

    public override object Clone() => Copy();
}
