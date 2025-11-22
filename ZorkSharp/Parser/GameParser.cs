namespace ZorkSharp.Parser;

/// <summary>
/// Natural language parser for game commands
/// </summary>
public class GameParser : IParser
{
    private readonly HashSet<string> _verbs = new(StringComparer.OrdinalIgnoreCase);
    private readonly HashSet<string> _prepositions = new(StringComparer.OrdinalIgnoreCase);
    private readonly HashSet<string> _articles = new(StringComparer.OrdinalIgnoreCase) { "the", "a", "an" };
    private readonly HashSet<string> _fillerWords = new(StringComparer.OrdinalIgnoreCase) { "at", "to" };

    public GameParser()
    {
        InitializeVocabulary();
    }

    private void InitializeVocabulary()
    {
        // Common verbs
        string[] verbs = {
            "take", "get", "drop", "put", "open", "close", "read", "examine", "look",
            "go", "walk", "move", "inventory", "i", "quit", "exit", "help", "score",
            "attack", "kill", "hit", "fight", "eat", "drink", "climb", "push", "pull",
            "turn", "light", "extinguish", "wave", "raise", "lower", "rub", "touch",
            "tie", "untie", "lock", "unlock", "search", "find", "throw", "give",
            "enter", "leave", "wait", "z", "jump", "swim", "dig", "pour", "fill",
            "empty", "break", "fix", "mend", "knock", "listen", "smell", "taste",
            "pray", "chant", "say", "yell", "scream", "ring", "wind", "play",
            "north", "n", "south", "s", "east", "e", "west", "w",
            "northeast", "ne", "northwest", "nw", "southeast", "se", "southwest", "sw",
            "up", "u", "down", "d", "in", "out"
        };

        foreach (var verb in verbs)
            _verbs.Add(verb);

        // Common prepositions
        string[] prepositions = {
            "with", "using", "in", "into", "on", "onto", "under", "behind",
            "from", "to", "at", "through", "across", "over", "around", "beside"
        };

        foreach (var prep in prepositions)
            _prepositions.Add(prep);
    }

    public ParsedCommand Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return ParsedCommand.Invalid("I beg your pardon?");

        // Tokenize input
        var tokens = Tokenize(input);
        if (tokens.Length == 0)
            return ParsedCommand.Invalid("I beg your pardon?");

        // First token should be a verb or direction
        string verb = tokens[0].ToLowerInvariant();

        // Handle special cases
        if (verb == "i" || verb == "inventory")
            return ParsedCommand.Valid("inventory");

        if (verb == "l" || verb == "look")
        {
            if (tokens.Length == 1)
                return ParsedCommand.Valid("look");
            // "look at X" or "look in X"
            if (tokens.Length >= 2)
            {
                var directObj = string.Join(" ", tokens.Skip(1).Where(t => !_articles.Contains(t) && !_fillerWords.Contains(t)));
                return ParsedCommand.Valid("examine", directObj);
            }
        }

        if (verb == "x" || verb == "examine")
        {
            if (tokens.Length == 1)
                return ParsedCommand.Invalid("What do you want to examine?");
            var directObj = string.Join(" ", tokens.Skip(1).Where(t => !_articles.Contains(t)));
            return ParsedCommand.Valid("examine", directObj);
        }

        // Check if it's a direction command
        if (DirectionExtensions.TryParse(verb, out _))
        {
            return ParsedCommand.Valid("go", verb);
        }

        // Parse verb + objects
        if (!_verbs.Contains(verb))
        {
            return ParsedCommand.Invalid($"I don't understand the word \"{verb}\".");
        }

        // Find preposition to split direct and indirect objects
        int prepIndex = -1;
        string? preposition = null;

        for (int i = 1; i < tokens.Length; i++)
        {
            if (_prepositions.Contains(tokens[i]))
            {
                prepIndex = i;
                preposition = tokens[i];
                break;
            }
        }

        string? directObject = null;
        string? indirectObject = null;

        if (prepIndex > 1)
        {
            // We have direct object before preposition
            directObject = string.Join(" ", tokens[1..prepIndex].Where(t => !_articles.Contains(t)));
        }
        else if (prepIndex == -1 && tokens.Length > 1)
        {
            // No preposition, all remaining tokens are direct object
            directObject = string.Join(" ", tokens[1..].Where(t => !_articles.Contains(t)));
        }

        if (prepIndex != -1 && prepIndex < tokens.Length - 1)
        {
            // We have indirect object after preposition
            indirectObject = string.Join(" ", tokens[(prepIndex + 1)..].Where(t => !_articles.Contains(t)));
        }

        var command = ParsedCommand.Valid(verb, directObject, indirectObject, preposition);
        command.Tokens = tokens;
        return command;
    }

    private string[] Tokenize(string input)
    {
        // Split on whitespace and punctuation
        var tokens = input.ToLowerInvariant()
            .Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(t => !string.IsNullOrWhiteSpace(t))
            .ToArray();

        return tokens;
    }

    public void AddVocabulary(string word, string type)
    {
        switch (type.ToLowerInvariant())
        {
            case "verb":
                _verbs.Add(word);
                break;
            case "preposition":
                _prepositions.Add(word);
                break;
        }
    }

    public bool IsKnownWord(string word)
    {
        return _verbs.Contains(word) ||
               _prepositions.Contains(word) ||
               _articles.Contains(word);
    }
}
