using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork23.Tasks;

public static class FileHelper
{
    /// <summary>
    /// Получение количества всех символов в файле
    /// </summary>
    public static int GetFileSymbolsCount(string filePath)
    {
        using var reader = new StreamReader(filePath);

        var symbolsCount = 0;

        while (reader.Read() > -1)
        {
            symbolsCount++;
        }

        return symbolsCount;
    }

    /// <summary>
    /// Получение количества заданных символов в файле
    /// </summary>
    public static int GetFileSymbolsCount(string filePath, char symbol)
    {
        using var reader = new StreamReader(filePath);

        var symbolsCount = 0;
        int symbolNumber;

        while ((symbolNumber = reader.Read()) > -1)
        {
            if (symbol == (char)symbolNumber)
            {
                symbolsCount++;
            }
        }

        return symbolsCount;
    }

    /// <summary>
    /// Параллельное получение количества всех символов во всех файлах директории
    /// Параллелизм релизован через new Task и Task.WaitAll
    /// </summary>
    public static int GetDirectoryFilesSymbolsCount(string directoryPath)
    {
        var files = Directory.EnumerateFiles(directoryPath).ToArray();
        var tasks = new Task<int>[files.Length];

        var i = 0;

        foreach (var filePath in files)
        {
            tasks[i] = new Task<int>(() => GetFileSymbolsCount(filePath));
            tasks[i].Start();
            i++;
        }

        Task.WaitAll(tasks);
        int sumCount = 0;

        foreach (var task in tasks)
        {
            sumCount += task.Result;
        }

        return sumCount;
    }

    /// <summary>
    /// Параллельное получение количества заданных символов во всех файлах директории
    /// Параллелизм релизован через new Task и Task.WaitAll
    /// </summary>
    public static int GetDirectoryFilesSymbolsCount(string directoryPath, char symbol)
    {
        var files = Directory.EnumerateFiles(directoryPath).ToArray();
        var tasks = new Task<int>[files.Length];

        var i = 0;

        foreach (var filePath in files)
        {
            tasks[i] = new Task<int>(() => GetFileSymbolsCount(filePath, symbol));
            tasks[i].Start();
            i++;
        }

        Task.WaitAll(tasks);
        int sumCount = 0;

        foreach (var task in tasks)
        {
            sumCount += task.Result;
        }

        return sumCount;
    }

    /// <summary>
    /// Параллельное получение количества всех символов во всех файлах директории
    /// Параллелизм релизован через Task.Run и Task.WhenAll
    /// </summary>
    public static async Task<int> GetDirectoryFilesSymbolsCountAsync(string directoryPath)
    {
        var files = Directory.EnumerateFiles(directoryPath).ToArray();
        var tasks = new Task<int>[files.Length];

        var i = 0;

        foreach (var filePath in files)
        {
            tasks[i] = Task.Run(() => GetFileSymbolsCount(filePath));
            i++;
        }

        await Task.WhenAll(tasks);
        int sumCount = 0;

        foreach (var task in tasks)
        {
            sumCount += task.Result;
        }

        return sumCount;
    }

    /// <summary>
    /// Параллельное получение количества заданных символов во всех файлах директории
    /// Параллелизм релизован через Task.Run и Task.WhenAll
    /// </summary>
    public static async Task<int> GetDirectoryFilesSymbolsCountAsync(string directoryPath, char symbol)
    {
        var files = Directory.EnumerateFiles(directoryPath).ToArray();
        var tasks = new Task<int>[files.Length];

        var i = 0;

        foreach (var filePath in files)
        {
            tasks[i] = Task.Run(() => GetFileSymbolsCount(filePath, symbol));
            i++;
        }

        await Task.WhenAll(tasks);
        int sumCount = 0;

        foreach (var task in tasks)
        {
            sumCount += task.Result;
        }

        return sumCount;
    }
}
