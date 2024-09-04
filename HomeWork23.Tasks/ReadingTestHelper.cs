using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace HomeWork23.Tasks;

public static class ReadingTestHelper
{
    /// <summary>
    /// Вызов чтения файлов последовательно в цикле.
    /// Вывод резльтатов и времени для каждого файла
    /// </summary>
    public static void TestSequentialReading(IList<string> files)
    {
        var stopwatch = new Stopwatch();
        var number = 1;

        Console.WriteLine("Последовательное чтение файлов:");

        stopwatch.Start();

        foreach (var filePath in files)
        {
            ReadAndCount(filePath, number);
            number++;
        }

        stopwatch.Stop();

        WriteTime(files.Count, stopwatch);
    }

    /// <summary>
    /// Вызов чтения файлов параллельно через Parallel.For.
    /// Вывод резльтатов и времени для каждого файла
    /// </summary>
    public static void TestParallelReadingWithParallelFor(IList<string> files)
    {
        var stopwatch = new Stopwatch();

        Console.WriteLine("Параллельное чтение файлов с помощью Parallel.For:");

        stopwatch.Start();
        var parallelLoopResult = Parallel.For(0, files.Count, i => ReadAndCount(files[i], i + 1));
        stopwatch.Stop();

        WriteTime(files.Count, stopwatch);
    }

    /// <summary>
    /// Вызов чтения файлов параллельно через Task и WaitAll.
    /// Вывод резльтатов и времени для каждого файла
    /// </summary>
    public static void TestParallelReadingWithTaskWaitAll(IList<string> files)
    {
        var stopwatch = new Stopwatch();
        var tasks = new Task[files.Count];
        var number = 0;

        Console.WriteLine("Параллельное чтение файлов с помощью Task и WaitAll:");

        stopwatch.Start();

        foreach (var filePath in files)
        {
            var currentIndex = number;
            number++;
            var currentNumber = number;

            tasks[currentIndex] = Task.Run(() => ReadAndCount(filePath, currentNumber));
        }

        Task.WaitAll(tasks);
        stopwatch.Stop();

        WriteTime(files.Count, stopwatch);
    }

    /// <summary>
    /// Чтение файла для подсчёта количества всех символов и количества пробелов в этом файле.
    /// Вывод результатов чтения и времени затраченного на чтение
    /// </summary>
    private static void ReadAndCount(string filePath, int fileNumber)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var fileInfo = new FileInfo(filePath);
        var allSymbolsCount = FileHelper.GetFileSymbolsCount(filePath);
        var spaceSymbolsCount = FileHelper.GetFileSymbolsCount(filePath, ' ');
        stopwatch.Stop();
        Console.WriteLine($"[{fileNumber,2}] {fileInfo.Name,-30} Всего символов: {allSymbolsCount,5} \tПробелов: {spaceSymbolsCount,5} \tза {stopwatch.Elapsed.TotalMicroseconds,8:f1} микросекунд");
    }

    /// <summary>
    /// Вывод времени затраченного на чтение всех файлов
    /// </summary>
    private static void WriteTime(int filesCount, Stopwatch stopwatch)
    {
        Console.WriteLine($"Прочитано {filesCount} файлов за {stopwatch.Elapsed.TotalMicroseconds} микросекунд");
        Console.WriteLine();
        Console.WriteLine();
    }
}
