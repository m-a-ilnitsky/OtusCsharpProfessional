using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace HomeWork28.ParallelSum;

public static class ParallelMath
{
    public static TNumber GetSumConsecutively<TNumber>(this TNumber[] numbers)
        where TNumber : struct, INumber<TNumber>
    {
        TNumber sum = default;

        foreach (var number in numbers)
        {
            sum += number;
        }

        return sum;
    }

    public static int GetSumByLinq(this int[] numbers) => numbers.Sum(e => e);

    public static int GetSumByParallelLinq(this int[] numbers)
    {
        return numbers
            .AsParallel()
            .Sum(e => e);
    }

    public static int GetSumByParallelLinq(this int[] numbers, int? degreeOfParallelism)
    {
        degreeOfParallelism ??= Environment.ProcessorCount;

        return numbers
            .AsParallel()
            .WithDegreeOfParallelism(degreeOfParallelism.Value)
            .Sum(e => e);
    }

    public static int GetSumByParallelLinqWithPartitioner(this int[] numbers)
    {
        return Partitioner
            .Create(numbers, true)
            .AsParallel()
            .Sum(e => e);
    }

    public static int GetSumByParallelLinqWithPartitioner(
        this int[] numbers,
        int? degreeOfParallelism)
    {
        degreeOfParallelism ??= Environment.ProcessorCount;

        return Partitioner
            .Create(numbers, true)
            .AsParallel()
            .WithDegreeOfParallelism(degreeOfParallelism.Value)
            .Sum(e => e);
    }

    public static TNumber GetSumByParallelFor<TNumber>(
        this TNumber[] numbers,
        int? partsCount = null)
            where TNumber : struct, INumber<TNumber>
    {
        partsCount ??= Environment.ProcessorCount;
        var partSize = (int)Math.Ceiling((double)numbers.Length / partsCount.Value);

        TNumber sum = default;
        object _lock = new();

        Parallel.For(0, partsCount.Value, i =>
        {
            var startIndex = i * partSize;
            var maxSize = numbers.Length - startIndex;
            var copySyze = maxSize < partSize
                ? maxSize
                : partSize;
            var arrayPart = new TNumber[copySyze];
            Array.Copy(numbers, startIndex, arrayPart, 0, copySyze);

            var partialSum = arrayPart.GetSumConsecutively();

            lock (_lock)
            {
                sum += partialSum;
            }
        });

        return sum;
    }

    public static TNumber GetSumByParallelForWithOneArray<TNumber>(
        this TNumber[] numbers,
        int? partsCount = null)
            where TNumber : struct, INumber<TNumber>
    {
        partsCount ??= Environment.ProcessorCount;
        var partSize = (int)Math.Ceiling((double)numbers.Length / partsCount.Value);

        TNumber sum = default;
        object _lock = new();

        Parallel.For(0, partsCount.Value, partIndex =>
        {
            var startIndex = partIndex * partSize;
            var stopIndex = startIndex + partSize;
            if (stopIndex > numbers.Length)
            {
                stopIndex = numbers.Length;
            }

            TNumber partialSum = default;
            for (int i = startIndex; i < stopIndex; i++)
            {
                partialSum += numbers[i];
            }

            lock (_lock)
            {
                sum += partialSum;
            }
        });

        return sum;
    }

    public static TNumber GetSumByParallelForEach<TNumber>(
        this TNumber[] numbers,
        int? partsCount = null)
            where TNumber : struct, INumber<TNumber>
    {
        var rangePartitioner = partsCount is null
                ? Partitioner.Create(0, numbers.Length)
                : Partitioner.Create(0, numbers.Length, numbers.Length / partsCount.Value);

        TNumber sum = default;
        object _lock = new();

        Parallel.ForEach(rangePartitioner, range =>
        {
            TNumber partialSum = default;

            for (int i = range.Item1; i < range.Item2; i++)
            {
                partialSum += numbers[i];
            }

            lock (_lock)
            {
                sum += partialSum;
            }
        });

        return sum;
    }
}
