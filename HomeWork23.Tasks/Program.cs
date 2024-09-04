// See https://aka.ms/new-console-template for more information
using HomeWork23.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

static void ReadAndCount(string filePath, int fileNumber)
{
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    var fileInfo = new FileInfo(filePath);
    var allSymbolsCount = FileHelper.GetFileSymbolsCount(filePath);
    var spaceSymbolsCount = FileHelper.GetFileSymbolsCount(filePath, ' ');
    stopwatch.Stop();
    Console.WriteLine($"[{fileNumber,2}] {fileInfo.Name,-30} Всего символов: {allSymbolsCount,5} \tПробелов: {spaceSymbolsCount,5} \tза {stopwatch.Elapsed.TotalMicroseconds,8:f1} микросекунд");
}

static void WriteResult(int filesCount, Stopwatch stopwatch)
{
    Console.WriteLine($"Прочитано {filesCount} файла за {stopwatch.Elapsed.TotalMicroseconds} микросекунд");
    Console.WriteLine();
    Console.WriteLine();
}

static void TestSequentialReading(IList<string> files)
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

    WriteResult(files.Count, stopwatch);
}

static void TestParallelReadingWithFor(IList<string> files)
{
    var stopwatch = new Stopwatch();

    Console.WriteLine("Параллельное чтение файлов с помощью Parallel.For:");

    stopwatch.Start();
    var parallelLoopResult = Parallel.For(0, files.Count, i => ReadAndCount(files[i], i + 1));
    stopwatch.Stop();

    WriteResult(files.Count, stopwatch);
}

static void TestParallelReadingWithTaskWaitAll(IList<string> files)
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

    WriteResult(files.Count, stopwatch);
}

static void TestParallelCountingWithWaitAll(string directoryPath)
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

    Console.WriteLine($"Всего символов в файлах: {allSymbolsCount,6}, подсчитано за {allSymbolsMicroseconds,8:f1} микросекунд");
    Console.WriteLine($"Всего пробелов в файлах: {spaceSymbolsCount,6}, подсчитано за {spaceSymbolsMicroseconds,8:f1} микросекунд");
    Console.WriteLine();
    Console.WriteLine();
}

static async Task TestParallelCountingWithWhenAll(string directoryPath)
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

    Console.WriteLine($"Всего символов в файлах: {allSymbolsCount,6}, подсчитано за {allSymbolsMicroseconds,8:f1} микросекунд");
    Console.WriteLine($"Всего пробелов в файлах: {spaceSymbolsCount,6}, подсчитано за {spaceSymbolsMicroseconds,8:f1} микросекунд");
    Console.WriteLine();
    Console.WriteLine();
}

var currentDirectoryPath = Environment.CurrentDirectory;
var selectedDirectoryPath = new DirectoryInfo(currentDirectoryPath)
        .Parent
        ?.Parent
        ?.Parent
        ?.FullName;

if (string.IsNullOrWhiteSpace(selectedDirectoryPath))
{
    Console.WriteLine("Директория не выбрана!");
    return;
}

Console.WriteLine($"Выбранная директория: {selectedDirectoryPath}");
Console.WriteLine();

var files = Directory.EnumerateFiles(selectedDirectoryPath).ToList();

for (int i = 1; i <= 10; i++)
{
    Console.WriteLine("==============");
    Console.WriteLine($"{i,2}-й проход");
    Console.WriteLine();

    TestSequentialReading(files);
    TestParallelReadingWithFor(files);
    TestParallelReadingWithTaskWaitAll(files);

    TestParallelCountingWithWaitAll(selectedDirectoryPath);
    await TestParallelCountingWithWhenAll(selectedDirectoryPath);
}