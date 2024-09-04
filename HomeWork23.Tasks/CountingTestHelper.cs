using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HomeWork23.Tasks;

public static class CountingTestHelper
{
    /// <summary>
    /// Вызов функции параллельного подсчёта количества символов, реализованной через new Task и WaitAll.
    /// Вывод суммарных резльтатов и времени для всех файлов папки
    /// </summary>
    public static void TestParallelCountingWithWaitAll(string directoryPath)
    {
        var stopwatch = new Stopwatch();

        Console.WriteLine("WaitAll: Параллельный подсчёт всех символов и пробелов во всех файлах папки");

        stopwatch.Start();
        var allSymbolsCount = FileHelper.GetDirectoryFilesSymbolsCount(directoryPath);
        stopwatch.Stop();
        var allSymbolsMicroseconds = stopwatch.Elapsed.TotalMicroseconds;

        stopwatch.Restart();
        var spaceSymbolsCount = FileHelper.GetDirectoryFilesSymbolsCount(directoryPath, ' ');
        stopwatch.Stop();
        var spaceSymbolsMicroseconds = stopwatch.Elapsed.TotalMicroseconds;

        WriteCountsAndTimes(allSymbolsCount, allSymbolsMicroseconds, spaceSymbolsCount, spaceSymbolsMicroseconds);
    }

    /// <summary>
    /// Вызов функции параллельного подсчёта количества символов, реализованной через Task.Run и WhenAll.
    /// Вывод суммарных резльтатов и времени для всех файлов папки
    /// </summary>
    public static async Task TestParallelCountingWithWhenAll(string directoryPath)
    {
        var stopwatch = new Stopwatch();

        Console.WriteLine("WhenAll: Параллельный подсчёт всех символов и пробелов во всех файлах папки");

        stopwatch.Start();
        var allSymbolsCount = await FileHelper.GetDirectoryFilesSymbolsCountAsync(directoryPath);
        stopwatch.Stop();
        var allSymbolsMicroseconds = stopwatch.Elapsed.TotalMicroseconds;

        stopwatch.Restart();
        var spaceSymbolsCount = await FileHelper.GetDirectoryFilesSymbolsCountAsync(directoryPath, ' ');
        stopwatch.Stop();
        var spaceSymbolsMicroseconds = stopwatch.Elapsed.TotalMicroseconds;

        WriteCountsAndTimes(allSymbolsCount, allSymbolsMicroseconds, spaceSymbolsCount, spaceSymbolsMicroseconds);
    }

    /// <summary>
    /// Вывод количества символов и времени затраченного на чтение всех файлов
    /// </summary>
    private static void WriteCountsAndTimes(
        int allSymbolsCount, double allSymbolsMicroseconds,
        int spaceSymbolsCount, double spaceSymbolsMicroseconds)
    {
        Console.WriteLine($"Всего символов в файлах: {allSymbolsCount,6}, подсчитано за {allSymbolsMicroseconds,8:f1} микросекунд");
        Console.WriteLine($"Всего пробелов в файлах: {spaceSymbolsCount,6}, подсчитано за {spaceSymbolsMicroseconds,8:f1} микросекунд");
        Console.WriteLine();
        Console.WriteLine();
    }
}
