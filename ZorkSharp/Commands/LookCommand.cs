namespace ZorkSharp.Commands;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;

/// <summary>
/// Displays current room description
/// </summary>
public class LookCommand : BaseCommand
{
    public override string Name => "look";
    public override string[] Aliases => new[] { "l" };
    public override string Description => "Look around the current room";

    public override CommandResult Execute(ParsedCommand parsedCommand, IWorld world, IGameState gameState)
    {
        return Success(shouldDisplayRoom: true);
    }
}
