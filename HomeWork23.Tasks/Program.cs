using HomeWork23.Tasks;
using System;
using System.IO;
using System.Linq;

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

    ReadingTestHelper.TestSequentialReading(files);
    ReadingTestHelper.TestParallelReadingWithParallelFor(files);
    ReadingTestHelper.TestParallelReadingWithTaskWaitAll(files);

    CountingTestHelper.TestParallelCountingWithWaitAll(selectedDirectoryPath);
    await CountingTestHelper.TestParallelCountingWithWhenAll(selectedDirectoryPath);
}
