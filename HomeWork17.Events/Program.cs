using HomeWork17.Events.Traverser;
using HomeWork17.Events.Traverser.Args;
using System;
using System.IO;

Console.WriteLine("Hello, World!");

var currentDirectoryPath = Environment.CurrentDirectory;
var parentDirectoryPath = new DirectoryInfo(currentDirectoryPath)
        .Parent
        ?.Parent
        ?.Parent
        ?.FullName
        ?? currentDirectoryPath;
var traverser = new DirectoryFileTraverser(parentDirectoryPath);

traverser.DirectoryFound += (object? obj, DirectoryArgs args) =>
{
    Console.WriteLine();
    Console.WriteLine($"Директория: {args.DirectoryInfo.Name} \t{args.DirectoryInfo.FullName}");
    Console.WriteLine("Выйти (Escape) / продолжить (любая другая клавиша)?");

    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
    {
        var fileTraverser = obj as DirectoryFileTraverser;

        if (fileTraverser is not null)
        {
            fileTraverser.Stop();
            Console.WriteLine("Выход выполнен!");
        }
    }

    Console.WriteLine();
};

traverser.FileFound += (object? obj, FileArgs args) =>
{
    Console.WriteLine($"Файл: {args.FileInfo.Name}");
};

Console.WriteLine($"Исходная директория: {parentDirectoryPath}");
Console.WriteLine();

traverser.TraverseRecursively();

Console.WriteLine();
Console.WriteLine("Обход завершён!");
