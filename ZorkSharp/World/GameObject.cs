namespace ZorkSharp.World;

using ZorkSharp.Core;

/// <summary>
/// Represents an object in the game world
/// </summary>
public class GameObject : IGameObject
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string[] Synonyms { get; set; } = Array.Empty<string>();
    public string[] Adjectives { get; set; } = Array.Empty<string>();
    public ObjectFlags Flags { get; set; }
    public string? LocationId { get; set; }
    public int Size { get; set; }
    public int Capacity { get; set; }
    public int Value { get; set; }
    public List<string> Contents { get; set; } = new();
    public string? ActionHandler { get; set; }

    public bool HasFlag(ObjectFlags flag) => (Flags & flag) != 0;

    public bool IsContainer => HasFlag(ObjectFlags.Container);
    public bool IsTakeable => HasFlag(ObjectFlags.Takeable);
    public bool IsVisible => HasFlag(ObjectFlags.Visible);
    public bool ProvidesLight => HasFlag(ObjectFlags.Light);

    public bool CanContain(IGameObject obj)
    {
        if (!IsContainer) return false;

        int currentSize = Contents.Sum(c => 1);
        return currentSize + obj.Size <= Capacity;
    }

    public bool MatchesName(string name)
    {
        string lowerName = name.ToLowerInvariant();
        return Name.ToLowerInvariant() == lowerName ||
               Synonyms.Any(s => s.ToLowerInvariant() == lowerName);
    }

    public bool MatchesAdjective(string adjective)
    {
        string lowerAdj = adjective.ToLowerInvariant();
        return Adjectives.Any(a => a.ToLowerInvariant() == lowerAdj);
    }
}
