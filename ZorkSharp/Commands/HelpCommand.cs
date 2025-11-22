namespace ZorkSharp.Commands;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;
using System.Text;

/// <summary>
/// Shows help information
/// </summary>
public class HelpCommand : BaseCommand
{
    public override string Name => "help";
    public override string[] Aliases => new[] { "?" };
    public override string Description => "Show help information";

    private readonly ICommandFactory _commandFactory;

    public HelpCommand(ICommandFactory commandFactory)
    {
        _commandFactory = commandFactory;
    }

    public override CommandResult Execute(ParsedCommand parsedCommand, IWorld world, IGameState gameState)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ZORK I: The Great Underground Empire");
        sb.AppendLine("=====================================");
        sb.AppendLine();
        sb.AppendLine("Available commands:");
        sb.AppendLine();

        var commands = _commandFactory.GetAllCommands()
            .OrderBy(c => c.Name)
            .ToList();

        foreach (var command in commands)
        {
            sb.Append($"  {command.Name.ToUpper()}");
            if (command.Aliases.Any())
            {
                sb.Append($" ({string.Join(", ", command.Aliases)})");
            }
            sb.AppendLine($" - {command.Description}");
        }

        sb.AppendLine();
        sb.AppendLine("Directions: N, S, E, W, NE, NW, SE, SW, U, D, IN, OUT");
        sb.AppendLine();
        sb.AppendLine("Type commands in natural language, like:");
        sb.AppendLine("  'take the lamp'");
        sb.AppendLine("  'open door with key'");
        sb.AppendLine("  'go north'");

        return Success(sb.ToString());
    }
}
