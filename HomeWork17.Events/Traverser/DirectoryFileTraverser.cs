using HomeWork17.Events.Traverser.Args;
using System;
using System.IO;

namespace HomeWork17.Events.Traverser;

public class DirectoryFileTraverser(string directoryPath)
{
    private readonly string _directoryPath = directoryPath;

    public event EventHandler<FileArgs>? FileFound;
    public event EventHandler<DirectoryArgs>? DirectoryFound;

    private bool _stopTraversal;

    public void Stop() => _stopTraversal = true;

    public void Traverse()
        => Traverse(_directoryPath, false);

    public void TraverseRecursively()
        => Traverse(_directoryPath, true);

    private bool Traverse(string directoryPath, bool traverseRecursively)
    {
        if (FileFound is null && DirectoryFound is null)
        {
            return false;
        }

        _stopTraversal = false;

        foreach (var filePath in Directory.EnumerateFiles(directoryPath))
        {
            FileFound?.Invoke(this, new FileArgs(filePath));

            if (_stopTraversal)
            {
                return false;
            }
        }

        if (!traverseRecursively || _stopTraversal)
        {
            return false;
        }

        foreach (var innerDirectoryPath in Directory.EnumerateDirectories(directoryPath))
        {
            DirectoryFound?.Invoke(this, new DirectoryArgs(innerDirectoryPath));

            if (_stopTraversal)
            {
                return false;
            }

            var isFinished = Traverse(innerDirectoryPath, traverseRecursively);

            if (!isFinished)
            {
                return false;
            }
        }

        return true;
    }
}
