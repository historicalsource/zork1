namespace ZorkSharp.World;

using ZorkSharp.Core;

/// <summary>
/// Represents an interactive object in the game
/// </summary>
public interface IGameObject
{
    string Id { get; }
    string Name { get; }
    string Description { get; }
    string[] Synonyms { get; }
    string[] Adjectives { get; }
    ObjectFlags Flags { get; }
    string? LocationId { get; set; }
    int Size { get; }
    int Capacity { get; }
    int Value { get; }
    List<string> Contents { get; }
    string? ActionHandler { get; }

    bool HasFlag(ObjectFlags flag);
    bool IsContainer { get; }
    bool IsTakeable { get; }
    bool IsVisible { get; }
    bool ProvidesLight { get; }
    bool CanContain(IGameObject obj);
}
