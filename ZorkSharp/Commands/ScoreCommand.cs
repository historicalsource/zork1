namespace ZorkSharp.Commands;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;

/// <summary>
/// Shows the current score
/// </summary>
public class ScoreCommand : BaseCommand
{
    public override string Name => "score";
    public override string[] Aliases => Array.Empty<string>();
    public override string Description => "Show your score";

    public override CommandResult Execute(ParsedCommand parsedCommand, IWorld world, IGameState gameState)
    {
        string rank = GetRank(gameState.Score);
        return Success($"Your score is {gameState.Score} (total of {gameState.MaxScore} points), in {gameState.Moves} moves.\nThis gives you the rank of {rank}.");
    }

    private string GetRank(int score)
    {
        return score switch
        {
            >= 300 => "Master Adventurer",
            >= 200 => "Adventurer",
            >= 100 => "Junior Adventurer",
            >= 50 => "Novice Adventurer",
            _ => "Beginner"
        };
    }
}
