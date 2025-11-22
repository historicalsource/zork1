namespace ZorkSharp.Commands;

/// <summary>
/// Factory for creating and managing commands
/// </summary>
public class CommandFactory : ICommandFactory
{
    private readonly Dictionary<string, ICommand> _commands = new(StringComparer.OrdinalIgnoreCase);

    public void RegisterCommand(ICommand command)
    {
        _commands[command.Name] = command;

        // Register aliases
        foreach (var alias in command.Aliases)
        {
            _commands[alias] = command;
        }
    }

    public ICommand? GetCommand(string verb)
    {
        _commands.TryGetValue(verb, out var command);
        return command;
    }

    public IEnumerable<ICommand> GetAllCommands()
    {
        return _commands.Values.Distinct();
    }
}
