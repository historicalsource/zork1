namespace ZorkSharp.UI;

using ZorkSharp.Core;

/// <summary>
/// Console-based output writer
/// </summary>
public class ConsoleOutputWriter : IOutputWriter
{
    public void Write(string message)
    {
        Console.Write(message);
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteBlankLine()
    {
        Console.WriteLine();
    }

    public void Clear()
    {
        Console.Clear();
    }
}
