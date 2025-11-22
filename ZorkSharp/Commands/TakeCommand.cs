namespace ZorkSharp.Commands;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;

/// <summary>
/// Takes an object
/// </summary>
public class TakeCommand : BaseCommand
{
    public override string Name => "take";
    public override string[] Aliases => new[] { "get", "grab", "pick" };
    public override string Description => "Take an object";

    public override CommandResult Execute(ParsedCommand parsedCommand, IWorld world, IGameState gameState)
    {
        if (parsedCommand.DirectObject == null)
            return Failed("What do you want to take?");

        var obj = world.FindObjectInRoom(gameState.CurrentRoomId, parsedCommand.DirectObject);
        if (obj == null)
            return Failed($"You don't see any {parsedCommand.DirectObject} here.");

        if (!obj.IsTakeable)
            return Failed($"You can't take the {obj.Name}.");

        if (!world.PlayerInventory.CanCarry(obj))
            return Failed("You are carrying too much.");

        world.MoveObject(obj.Id, null);
        world.PlayerInventory.Add(obj.Id);

        return Success("Taken.");
    }
}
