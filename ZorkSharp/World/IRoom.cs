namespace ZorkSharp.World;

using ZorkSharp.Core;

/// <summary>
/// Represents a location in the game world
/// </summary>
public interface IRoom
{
    string Id { get; }
    string Name { get; }
    string Description { get; }
    string LongDescription { get; }
    RoomFlags Flags { get; }

    Dictionary<Direction, IRoomExit> Exits { get; }
    List<string> Items { get; }
    List<string> GlobalItems { get; }

    bool HasFlag(RoomFlags flag);
    bool IsLit { get; }
    string? ActionHandler { get; }
}

/// <summary>
/// Represents an exit from a room
/// </summary>
public interface IRoomExit
{
    string? DestinationRoomId { get; }
    string? CustomMessage { get; }
    Func<bool>? Condition { get; }
    bool CanTraverse();
}
