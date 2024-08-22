using System;
using System.IO;

namespace HomeWork17.Events.Traverser.Args;

public class FileArgs(string filePath) : EventArgs
{
    public FileInfo FileInfo { get; set; } = new FileInfo(filePath);
}
