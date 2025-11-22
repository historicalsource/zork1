namespace ZorkSharp.Core;

/// <summary>
/// Handles input from the user (Single Responsibility Principle)
/// </summary>
public interface IInputReader
{
    string? ReadLine();
}
