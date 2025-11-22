namespace ZorkSharp.World;

/// <summary>
/// Manages player inventory
/// </summary>
public class Inventory : IInventory
{
    private readonly IWorld _world;
    public List<string> Items { get; } = new();
    public int MaxWeight { get; set; } = 100;

    public Inventory(IWorld world)
    {
        _world = world;
    }

    public int TotalWeight
    {
        get
        {
            return Items.Sum(itemId =>
            {
                var obj = _world.GetObject(itemId);
                return obj?.Size ?? 0;
            });
        }
    }

    public bool Add(string itemId)
    {
        if (Items.Contains(itemId))
            return false;

        var obj = _world.GetObject(itemId);
        if (obj == null || !CanCarry(obj))
            return false;

        Items.Add(itemId);
        return true;
    }

    public bool Remove(string itemId)
    {
        return Items.Remove(itemId);
    }

    public bool Contains(string itemId)
    {
        return Items.Contains(itemId);
    }

    public bool CanCarry(IGameObject obj)
    {
        return TotalWeight + obj.Size <= MaxWeight;
    }

    public List<IGameObject> GetAllItems()
    {
        return Items
            .Select(id => _world.GetObject(id))
            .Where(obj => obj != null)
            .Cast<IGameObject>()
            .ToList();
    }
}
