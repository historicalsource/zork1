namespace ZorkSharp.Core;

/// <summary>
/// Available movement directions in the game
/// </summary>
public enum Direction
{
    North,
    South,
    East,
    West,
    NorthEast,
    NorthWest,
    SouthEast,
    SouthWest,
    Up,
    Down,
    In,
    Out,
    Land
}

public static class DirectionExtensions
{
    private static readonly Dictionary<string, Direction> DirectionMap = new()
    {
        ["n"] = Direction.North,
        ["north"] = Direction.North,
        ["s"] = Direction.South,
        ["south"] = Direction.South,
        ["e"] = Direction.East,
        ["east"] = Direction.East,
        ["w"] = Direction.West,
        ["west"] = Direction.West,
        ["ne"] = Direction.NorthEast,
        ["northeast"] = Direction.NorthEast,
        ["nw"] = Direction.NorthWest,
        ["northwest"] = Direction.NorthWest,
        ["se"] = Direction.SouthEast,
        ["southeast"] = Direction.SouthEast,
        ["sw"] = Direction.SouthWest,
        ["southwest"] = Direction.SouthWest,
        ["u"] = Direction.Up,
        ["up"] = Direction.Up,
        ["d"] = Direction.Down,
        ["down"] = Direction.Down,
        ["in"] = Direction.In,
        ["enter"] = Direction.In,
        ["out"] = Direction.Out,
        ["exit"] = Direction.Out,
        ["land"] = Direction.Land
    };

    public static bool TryParse(string input, out Direction direction)
    {
        return DirectionMap.TryGetValue(input.ToLowerInvariant(), out direction);
    }
}
