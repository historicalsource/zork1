namespace ZorkSharp.Core;

/// <summary>
/// Handles output to the user (Single Responsibility Principle)
/// </summary>
public interface IOutputWriter
{
    void Write(string message);
    void WriteLine(string message);
    void WriteBlankLine();
    void Clear();
}
