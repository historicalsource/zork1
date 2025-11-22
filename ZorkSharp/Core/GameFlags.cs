namespace ZorkSharp.Core;

/// <summary>
/// Flags for game objects and rooms
/// </summary>
[Flags]
public enum ObjectFlags : long
{
    None = 0,
    Takeable = 1 << 0,
    Openable = 1 << 1,
    IsOpen = 1 << 2,
    Container = 1 << 3,
    Weapon = 1 << 4,
    Actor = 1 << 5,
    Light = 1 << 6,
    Readable = 1 << 7,
    Drinkable = 1 << 8,
    Edible = 1 << 9,
    Visible = 1 << 10,
    Touched = 1 << 11,
    Treasure = 1 << 12,
    Wearable = 1 << 13,
    Flammable = 1 << 14,
    Climbable = 1 << 15,
    Surface = 1 << 16,
    Locked = 1 << 17,
    Transparent = 1 << 18,
    Vehicle = 1 << 19,
    Tool = 1 << 20
}

[Flags]
public enum RoomFlags : long
{
    None = 0,
    Light = 1 << 0,
    Sacred = 1 << 1,
    Outside = 1 << 2,
    Water = 1 << 3,
    Land = 1 << 4,
    Visited = 1 << 5,
    NoExit = 1 << 6
}
