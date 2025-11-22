namespace ZorkSharp.World;

/// <summary>
/// Manages player inventory (Single Responsibility Principle)
/// </summary>
public interface IInventory
{
    List<string> Items { get; }
    int TotalWeight { get; }
    int MaxWeight { get; }

    bool Add(string itemId);
    bool Remove(string itemId);
    bool Contains(string itemId);
    bool CanCarry(IGameObject obj);
    List<IGameObject> GetAllItems();
}
