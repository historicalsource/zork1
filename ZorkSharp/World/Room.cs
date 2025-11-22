namespace ZorkSharp.World;

using ZorkSharp.Core;

/// <summary>
/// Represents a room in the game world
/// </summary>
public class Room : IRoom
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public RoomFlags Flags { get; set; }
    public Dictionary<Direction, IRoomExit> Exits { get; set; } = new();
    public List<string> Items { get; set; } = new();
    public List<string> GlobalItems { get; set; } = new();
    public string? ActionHandler { get; set; }

    public bool HasFlag(RoomFlags flag) => (Flags & flag) != 0;

    public bool IsLit => HasFlag(RoomFlags.Light);
}

/// <summary>
/// Represents an exit from a room
/// </summary>
public class RoomExit : IRoomExit
{
    public string? DestinationRoomId { get; set; }
    public string? CustomMessage { get; set; }
    public Func<bool>? Condition { get; set; }

    public bool CanTraverse()
    {
        return Condition?.Invoke() ?? true;
    }
}
