namespace ZorkSharp.Parser;

/// <summary>
/// Parses user input into game commands (Single Responsibility Principle)
/// </summary>
public interface IParser
{
    ParsedCommand Parse(string input);
    void AddVocabulary(string word, string type);
    bool IsKnownWord(string word);
}
