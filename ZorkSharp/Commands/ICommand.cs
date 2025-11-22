namespace ZorkSharp.Commands;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;

/// <summary>
/// Base interface for all commands (Command Pattern + Open/Closed Principle)
/// New commands can be added without modifying existing code
/// </summary>
public interface ICommand
{
    string Name { get; }
    string[] Aliases { get; }
    string Description { get; }

    CommandResult Execute(ParsedCommand parsedCommand, IWorld world, IGameState gameState);
    bool CanExecute(ParsedCommand parsedCommand, IWorld world, IGameState gameState);
}
