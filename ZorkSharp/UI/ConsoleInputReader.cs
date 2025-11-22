namespace ZorkSharp.UI;

using ZorkSharp.Core;

/// <summary>
/// Console-based input reader
/// </summary>
public class ConsoleInputReader : IInputReader
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}
