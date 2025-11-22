namespace ZorkSharp.Core;

/// <summary>
/// Manages the current game state
/// </summary>
public interface IGameState
{
    bool IsRunning { get; set; }
    int Score { get; set; }
    int Moves { get; set; }
    int MaxScore { get; }
    string CurrentRoomId { get; set; }
    bool HasWon { get; set; }
    bool IsInDarkness { get; }
    string? LastCommand { get; set; }

    void Initialize();
    void IncrementMoves();
    void AddScore(int points);
}
