namespace ZorkSharp.Commands;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;

/// <summary>
/// Quits the game
/// </summary>
public class QuitCommand : BaseCommand
{
    public override string Name => "quit";
    public override string[] Aliases => new[] { "exit", "q" };
    public override string Description => "Quit the game";

    public override CommandResult Execute(ParsedCommand parsedCommand, IWorld world, IGameState gameState)
    {
        gameState.IsRunning = false;
        return new CommandResult(CommandStatus.Exit, "Do you really want to quit? (yes/no)");
    }
}
