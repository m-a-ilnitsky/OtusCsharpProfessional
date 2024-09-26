
using HomeWork28.ParallelSum;
using PrintableTable;
using System;
using System.Diagnostics;

var numbers1 = new int[10_000];
var numbers2 = new int[100_000];
var numbers3 = new int[1_000_000];
var numbers4 = new int[10_000_000];

FillByRandomNumbers(numbers1, 0, 20);
FillByRandomNumbers(numbers2, 0, 20);
FillByRandomNumbers(numbers3, 0, 20);
FillByRandomNumbers(numbers4, 0, 20);

var methodsNames = new string[]
{
    "Consecutively by for",
    "Consecutively by LINQ",

    "PLINQ default",

    "PLINQ with degree 1",
    "PLINQ with degree 2",
    "PLINQ with degree 3",
    "PLINQ with degree 4",
    "PLINQ with degree 8",
    "PLINQ with degree 16",
    "PLINQ with degree 32",

    "PLINQ+Partitioner default",

    "PLINQ+Partitioner with degree 1",
    "PLINQ+Partitioner with degree 2",
    "PLINQ+Partitioner with degree 3",
    "PLINQ+Partitioner with degree 4",
    "PLINQ+Partitioner with degree 8",
    "PLINQ+Partitioner with degree 16",
    "PLINQ+Partitioner with degree 32",

    "ParallelFor(new arrays) with parts 1",
    "ParallelFor(new arrays) with parts 2",
    "ParallelFor(new arrays) with parts 3",
    "ParallelFor(new arrays) with parts 4",
    "ParallelFor(new arrays) with parts 8",
    "ParallelFor(new arrays) with parts 16",
    "ParallelFor(new arrays) with parts 32",

    "ParallelFor(single array) with parts 1",
    "ParallelFor(single array) with parts 2",
    "ParallelFor(single array) with parts 3",
    "ParallelFor(single array) with parts 4",
    "ParallelFor(single array) with parts 8",
    "ParallelFor(single array) with parts 16",
    "ParallelFor(single array) with parts 32",

    "ParallelForEach+Partitioner with parts default",

    "ParallelForEach+Partitioner with parts 1",
    "ParallelForEach+Partitioner with parts 2",
    "ParallelForEach+Partitioner with parts 3",
    "ParallelForEach+Partitioner with parts 4",
    "ParallelForEach+Partitioner with parts 8",
    "ParallelForEach+Partitioner with parts 16",
    "ParallelForEach+Partitioner with parts 32",
};

var arraysSizes = new int[]
{
    numbers1.Length,
    numbers2.Length,
    numbers3.Length,
    numbers4.Length
};

var attempts = new int[] { 1, 2, 3 };

var numbersArrays = new int[][]
{
    numbers1,
    numbers2,
    numbers3,
    numbers4
};

var sumsTable = new Table3DWith2X<string, int, int, int>(
    methodsNames,
    arraysSizes,
    attempts);

var timesTable = new Table3DWith2X<string, int, int, int>(
    methodsNames,
    arraysSizes,
    attempts);

var minTimesTable = new Table2D<string, int, int>(
    methodsNames,
    arraysSizes);

for (int methodIndex = 0; methodIndex < methodsNames.Length; methodIndex++)
{
    for (int arrayIndex = 0; arrayIndex < arraysSizes.Length; arrayIndex++)
    {
        Func<int> func = methodIndex switch
        {
            0 => () => numbersArrays[arrayIndex].GetSumConsecutively(),
            1 => () => numbersArrays[arrayIndex].GetSumByLinq(),

            2 => () => numbersArrays[arrayIndex].GetSumByParallelLinq(),

            3 => () => numbersArrays[arrayIndex].GetSumByParallelLinq(1),
            4 => () => numbersArrays[arrayIndex].GetSumByParallelLinq(2),
            5 => () => numbersArrays[arrayIndex].GetSumByParallelLinq(3),
            6 => () => numbersArrays[arrayIndex].GetSumByParallelLinq(4),
            7 => () => numbersArrays[arrayIndex].GetSumByParallelLinq(8),
            8 => () => numbersArrays[arrayIndex].GetSumByParallelLinq(16),
            9 => () => numbersArrays[arrayIndex].GetSumByParallelLinq(32),

            10 => () => numbersArrays[arrayIndex].GetSumByParallelLinqWithPartitioner(),

            11 => () => numbersArrays[arrayIndex].GetSumByParallelLinqWithPartitioner(1),
            12 => () => numbersArrays[arrayIndex].GetSumByParallelLinqWithPartitioner(2),
            13 => () => numbersArrays[arrayIndex].GetSumByParallelLinqWithPartitioner(3),
            14 => () => numbersArrays[arrayIndex].GetSumByParallelLinqWithPartitioner(4),
            15 => () => numbersArrays[arrayIndex].GetSumByParallelLinqWithPartitioner(8),
            16 => () => numbersArrays[arrayIndex].GetSumByParallelLinqWithPartitioner(16),
            17 => () => numbersArrays[arrayIndex].GetSumByParallelLinqWithPartitioner(32),

            18 => () => numbersArrays[arrayIndex].GetSumByParallelFor(1),
            19 => () => numbersArrays[arrayIndex].GetSumByParallelFor(2),
            20 => () => numbersArrays[arrayIndex].GetSumByParallelFor(3),
            21 => () => numbersArrays[arrayIndex].GetSumByParallelFor(4),
            22 => () => numbersArrays[arrayIndex].GetSumByParallelFor(8),
            23 => () => numbersArrays[arrayIndex].GetSumByParallelFor(16),
            24 => () => numbersArrays[arrayIndex].GetSumByParallelFor(32),

            25 => () => numbersArrays[arrayIndex].GetSumByParallelForWithOneArray(1),
            26 => () => numbersArrays[arrayIndex].GetSumByParallelForWithOneArray(2),
            27 => () => numbersArrays[arrayIndex].GetSumByParallelForWithOneArray(3),
            28 => () => numbersArrays[arrayIndex].GetSumByParallelForWithOneArray(4),
            29 => () => numbersArrays[arrayIndex].GetSumByParallelForWithOneArray(8),
            30 => () => numbersArrays[arrayIndex].GetSumByParallelForWithOneArray(16),
            31 => () => numbersArrays[arrayIndex].GetSumByParallelForWithOneArray(32),

            32 => () => numbersArrays[arrayIndex].GetSumByParallelForEach(),

            33 => () => numbersArrays[arrayIndex].GetSumByParallelForEach(1),
            34 => () => numbersArrays[arrayIndex].GetSumByParallelForEach(2),
            35 => () => numbersArrays[arrayIndex].GetSumByParallelForEach(3),
            36 => () => numbersArrays[arrayIndex].GetSumByParallelForEach(4),
            37 => () => numbersArrays[arrayIndex].GetSumByParallelForEach(8),
            38 => () => numbersArrays[arrayIndex].GetSumByParallelForEach(16),
            39 => () => numbersArrays[arrayIndex].GetSumByParallelForEach(32),

            _ => throw new NotImplementedException()
        };

        var minTime = int.MaxValue;

        for (int attemptIndex = 0; attemptIndex < attempts.Length; attemptIndex++)
        {
            var (time, sum) = TestSum(func);

            sumsTable.DataCells[methodIndex, arrayIndex, attemptIndex] = sum;
            timesTable.DataCells[methodIndex, arrayIndex, attemptIndex] = time;

            minTime = Math.Min(minTime, time);
        }

        for (int i = 0; i < 30; i++)
        {
            var (time, _) = TestSum(func);
            minTime = Math.Min(minTime, time);
        }

        minTimesTable.DataCells[methodIndex, arrayIndex] = minTime;
    }
}

Console.WriteLine($"Environment.ProcessorCount = {Environment.ProcessorCount}");
Console.WriteLine();

Console.WriteLine("Table of sums for 3 attempts");
sumsTable.Print();
Console.WriteLine();

Console.WriteLine("Table of times in microseconds for 3 attempts");
timesTable.Print();
Console.WriteLine();

Console.WriteLine("Table of min times in microseconds for 33 attempts");
minTimesTable.Print();
Console.WriteLine();

static void FillByRandomNumbers(int[] numbers, int minNumber, int maxNumber)
{
    var rnd = new Random();

    for (int i = 0; i < numbers.Length; i++)
    {
        numbers[i] = rnd.Next(minNumber, maxNumber + 1);
    }
}

static (int Microseconds, int Sum) TestSum(Func<int> getSum)
{
    var stopwatch = new Stopwatch();

    stopwatch.Start();
    var sum = getSum.Invoke();
    stopwatch.Stop();

    return ((int)Math.Ceiling(stopwatch.Elapsed.TotalMicroseconds), sum);
}