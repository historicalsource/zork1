namespace ZorkSharp.Parser;

/// <summary>
/// Represents a parsed user command
/// </summary>
public class ParsedCommand
{
    public string Verb { get; set; } = string.Empty;
    public string? DirectObject { get; set; }
    public string? IndirectObject { get; set; }
    public string? Preposition { get; set; }
    public string[] Tokens { get; set; } = Array.Empty<string>();
    public bool IsValid { get; set; }
    public string? ErrorMessage { get; set; }

    public static ParsedCommand Invalid(string errorMessage)
    {
        return new ParsedCommand
        {
            IsValid = false,
            ErrorMessage = errorMessage
        };
    }

    public static ParsedCommand Valid(string verb, string? directObject = null,
        string? indirectObject = null, string? preposition = null)
    {
        return new ParsedCommand
        {
            IsValid = true,
            Verb = verb,
            DirectObject = directObject,
            IndirectObject = indirectObject,
            Preposition = preposition
        };
    }
}
