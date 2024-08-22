using System;
using System.IO;

namespace HomeWork17.Events.Traverser.Args;

public class DirectoryArgs(string directoryPath) : EventArgs
{
    public DirectoryInfo DirectoryInfo { get; set; } = new DirectoryInfo(directoryPath);
}
