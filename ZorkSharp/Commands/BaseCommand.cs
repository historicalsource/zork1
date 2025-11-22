namespace ZorkSharp.Commands;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;

/// <summary>
/// Base class for commands (Template Method Pattern)
/// </summary>
public abstract class BaseCommand : ICommand
{
    public abstract string Name { get; }
    public virtual string[] Aliases => Array.Empty<string>();
    public abstract string Description { get; }

    public abstract CommandResult Execute(ParsedCommand parsedCommand, IWorld world, IGameState gameState);

    public virtual bool CanExecute(ParsedCommand parsedCommand, IWorld world, IGameState gameState)
    {
        return true;
    }

    protected IGameObject? FindObject(string? objectName, IWorld world, IGameState gameState)
    {
        if (string.IsNullOrWhiteSpace(objectName))
            return null;

        // Try inventory first
        var obj = world.FindObjectInInventory(objectName);
        if (obj != null) return obj;

        // Try current room
        obj = world.FindObjectInRoom(gameState.CurrentRoomId, objectName);
        if (obj != null) return obj;

        return null;
    }

    protected CommandResult Success(string? message = null, bool shouldDisplayRoom = false)
    {
        return new CommandResult(CommandStatus.Success, message, shouldDisplayRoom);
    }

    protected CommandResult Failed(string message)
    {
        return new CommandResult(CommandStatus.Failed, message);
    }

    protected CommandResult NotHandled(string message)
    {
        return new CommandResult(CommandStatus.NotHandled, message);
    }
}
