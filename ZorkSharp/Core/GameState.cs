namespace ZorkSharp.Core;

/// <summary>
/// Manages current game state
/// </summary>
public class GameState : IGameState
{
    public bool IsRunning { get; set; }
    public int Score { get; set; }
    public int Moves { get; set; }
    public int MaxScore { get; } = 350;
    public string CurrentRoomId { get; set; } = "WEST-OF-HOUSE";
    public bool HasWon { get; set; }
    public bool IsInDarkness { get; private set; }
    public string? LastCommand { get; set; }

    private readonly Dictionary<string, object> _flags = new();

    public void Initialize()
    {
        IsRunning = true;
        Score = 0;
        Moves = 0;
        CurrentRoomId = "WEST-OF-HOUSE";
        HasWon = false;
        IsInDarkness = false;
        LastCommand = null;
        _flags.Clear();
    }

    public void IncrementMoves()
    {
        Moves++;
    }

    public void AddScore(int points)
    {
        Score += points;
    }

    public void SetFlag(string flag, object value)
    {
        _flags[flag] = value;
    }

    public T? GetFlag<T>(string flag)
    {
        if (_flags.TryGetValue(flag, out var value) && value is T typedValue)
        {
            return typedValue;
        }
        return default;
    }

    public bool HasFlag(string flag)
    {
        return _flags.ContainsKey(flag);
    }

    public void SetDarkness(bool isDark)
    {
        IsInDarkness = isDark;
    }
}
