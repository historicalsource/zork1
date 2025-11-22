namespace ZorkSharp.Commands;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;

/// <summary>
/// Drops an object
/// </summary>
public class DropCommand : BaseCommand
{
    public override string Name => "drop";
    public override string[] Aliases => new[] { "put", "place" };
    public override string Description => "Drop an object";

    public override CommandResult Execute(ParsedCommand parsedCommand, IWorld world, IGameState gameState)
    {
        if (parsedCommand.DirectObject == null)
            return Failed("What do you want to drop?");

        var obj = world.FindObjectInInventory(parsedCommand.DirectObject);
        if (obj == null)
            return Failed($"You don't have any {parsedCommand.DirectObject}.");

        world.PlayerInventory.Remove(obj.Id);
        world.MoveObject(obj.Id, gameState.CurrentRoomId);

        return Success("Dropped.");
    }
}
