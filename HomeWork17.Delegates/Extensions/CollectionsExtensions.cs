using System;
using System.Collections.Generic;
using System.Numerics;

namespace HomeWork17.Delegates.Extensions;

public static class CollectionsExtensions
{
    public static TNumber? GetMaxNumber<TItem, TNumber>(
        this IEnumerable<TItem> collection,
        Func<TItem, TNumber> convertToNumber,
        bool skipNull = true)
            where TItem : class
            where TNumber : struct,
                            INumber<TNumber>,
                            IComparisonOperators<TNumber, TNumber, bool>
    {
        Nullable<TNumber> maxNumber = default;

        foreach (var item in collection)
        {
            if (skipNull && item is null)
            {
                continue;
            }

            var number = convertToNumber(item);

            if (maxNumber is null || maxNumber < number)
            {
                maxNumber = number;
            }
        }

        return maxNumber;
    }

    public static TItem? GetMaxItem<TItem, TNumber>(
        this IEnumerable<TItem> collection,
        Func<TItem, TNumber> convertToNumber,
        bool skipNull = true)
            where TItem : class
            where TNumber : INumber<TNumber>,
                            IMinMaxValue<TNumber>,
                            IComparisonOperators<TNumber, TNumber, bool>
    {
        TNumber maxNumber = TNumber.MinValue;
        TItem? maxItem = default;

        foreach (var item in collection)
        {
            if (skipNull && item is null)
            {
                continue;
            }

            var number = convertToNumber(item);

            if (maxNumber < number)
            {
                maxNumber = number;
                maxItem = item;
            }
        }

        return maxItem;
    }

    public static IEnumerable<TItem> GetMaxItems<TItem, TNumber>(
        this IEnumerable<TItem> collection,
        Func<TItem, TNumber> convertToNumber,
        bool skipNull = true)
            where TItem : class
            where TNumber : INumber<TNumber>,
                            IMinMaxValue<TNumber>,
                            IComparisonOperators<TNumber, TNumber, bool>
    {
        TNumber maxNumber = TNumber.MinValue;
        var maxItems = new List<TItem>();

        foreach (var item in collection)
        {
            if (skipNull && item is null)
            {
                continue;
            }

            var number = convertToNumber(item);

            if (maxNumber < number)
            {
                maxNumber = number;
                maxItems.Clear();
                maxItems.Add(item);
            }
            else if (maxNumber == number)
            {
                maxItems.Add(item);
            }
        }

        return maxItems;
    }
}
