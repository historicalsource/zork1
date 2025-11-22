namespace ZorkSharp.World;

using ZorkSharp.Core;

/// <summary>
/// Manages the game world state (rooms, objects, inventory)
/// </summary>
public interface IWorld
{
    IRoom? GetRoom(string roomId);
    IGameObject? GetObject(string objectId);
    IGameObject? FindObjectInRoom(string roomId, string objectName);
    IGameObject? FindObjectInInventory(string objectName);
    IGameObject? FindObjectAnywhere(string objectName);
    List<IGameObject> GetObjectsInRoom(string roomId);
    List<IGameObject> GetVisibleObjectsInRoom(string roomId);
    IInventory PlayerInventory { get; }

    bool MoveObject(string objectId, string? newLocationId);
    void Initialize();
}
