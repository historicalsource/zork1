namespace ZorkSharp.World;

using ZorkSharp.Core;

/// <summary>
/// Manages the game world
/// </summary>
public class GameWorld : IWorld
{
    private readonly Dictionary<string, IRoom> _rooms = new();
    private readonly Dictionary<string, IGameObject> _objects = new();
    private IInventory? _inventory;

    public IInventory PlayerInventory => _inventory ??= new Inventory(this);

    public void AddRoom(IRoom room)
    {
        _rooms[room.Id] = room;
    }

    public void AddObject(IGameObject obj)
    {
        _objects[obj.Id] = obj;
    }

    public IRoom? GetRoom(string roomId)
    {
        _rooms.TryGetValue(roomId, out var room);
        return room;
    }

    public IGameObject? GetObject(string objectId)
    {
        _objects.TryGetValue(objectId, out var obj);
        return obj;
    }

    public IGameObject? FindObjectInRoom(string roomId, string objectName)
    {
        var room = GetRoom(roomId);
        if (room == null) return null;

        // Check room items
        foreach (var itemId in room.Items)
        {
            var obj = GetObject(itemId);
            if (obj != null && MatchesObjectName(obj, objectName))
                return obj;
        }

        // Check global items
        foreach (var itemId in room.GlobalItems)
        {
            var obj = GetObject(itemId);
            if (obj != null && MatchesObjectName(obj, objectName))
                return obj;
        }

        return null;
    }

    public IGameObject? FindObjectInInventory(string objectName)
    {
        foreach (var itemId in PlayerInventory.Items)
        {
            var obj = GetObject(itemId);
            if (obj != null && MatchesObjectName(obj, objectName))
                return obj;
        }
        return null;
    }

    public IGameObject? FindObjectAnywhere(string objectName)
    {
        return _objects.Values.FirstOrDefault(obj => MatchesObjectName(obj, objectName));
    }

    public List<IGameObject> GetObjectsInRoom(string roomId)
    {
        var room = GetRoom(roomId);
        if (room == null) return new List<IGameObject>();

        var objects = new List<IGameObject>();

        foreach (var itemId in room.Items)
        {
            var obj = GetObject(itemId);
            if (obj != null)
                objects.Add(obj);
        }

        return objects;
    }

    public List<IGameObject> GetVisibleObjectsInRoom(string roomId)
    {
        return GetObjectsInRoom(roomId)
            .Where(obj => obj.IsVisible)
            .ToList();
    }

    public bool MoveObject(string objectId, string? newLocationId)
    {
        var obj = GetObject(objectId);
        if (obj == null) return false;

        // Remove from current location
        var currentLocation = obj.LocationId;
        if (currentLocation != null)
        {
            var room = GetRoom(currentLocation);
            room?.Items.Remove(objectId);

            // Also remove from any container
            var container = GetObject(currentLocation);
            container?.Contents.Remove(objectId);
        }

        // Remove from inventory
        PlayerInventory.Remove(objectId);

        // Add to new location
        obj.LocationId = newLocationId;

        if (newLocationId != null)
        {
            var room = GetRoom(newLocationId);
            if (room != null)
            {
                room.Items.Add(objectId);
            }
            else
            {
                // Might be a container
                var container = GetObject(newLocationId);
                container?.Contents.Add(objectId);
            }
        }

        return true;
    }

    public void Initialize()
    {
        // This will be populated with game data
    }

    private bool MatchesObjectName(IGameObject obj, string name)
    {
        if (obj is GameObject gameObj)
            return gameObj.MatchesName(name);

        return obj.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
    }
}
