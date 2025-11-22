namespace ZorkSharp.Commands;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;

/// <summary>
/// Handles movement between rooms
/// </summary>
public class MovementCommand : BaseCommand
{
    public override string Name => "go";
    public override string[] Aliases => new[] { "walk", "move", "n", "s", "e", "w", "ne", "nw", "se", "sw", "u", "d", "north", "south", "east", "west", "up", "down", "in", "out" };
    public override string Description => "Move in a direction";

    public override CommandResult Execute(ParsedCommand parsedCommand, IWorld world, IGameState gameState)
    {
        if (parsedCommand.DirectObject == null)
            return Failed("Which direction?");

        if (!DirectionExtensions.TryParse(parsedCommand.DirectObject, out Direction direction))
            return Failed($"I don't know how to go {parsedCommand.DirectObject}.");

        var currentRoom = world.GetRoom(gameState.CurrentRoomId);
        if (currentRoom == null)
            return Failed("You seem to be lost.");

        if (!currentRoom.Exits.TryGetValue(direction, out var exit))
            return Failed("You can't go that way.");

        if (!exit.CanTraverse())
        {
            if (!string.IsNullOrEmpty(exit.CustomMessage))
                return Failed(exit.CustomMessage);
            return Failed("You can't go that way.");
        }

        if (exit.DestinationRoomId == null)
        {
            if (!string.IsNullOrEmpty(exit.CustomMessage))
                return Failed(exit.CustomMessage);
            return Failed("You can't go that way.");
        }

        var destinationRoom = world.GetRoom(exit.DestinationRoomId);
        if (destinationRoom == null)
            return Failed("You can't go that way.");

        gameState.CurrentRoomId = exit.DestinationRoomId;
        return Success(shouldDisplayRoom: true);
    }
}
