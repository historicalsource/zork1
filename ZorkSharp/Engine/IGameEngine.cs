namespace ZorkSharp.Engine;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;
using ZorkSharp.Commands;
using ZorkSharp.Events;

/// <summary>
/// Main game engine interface (Facade Pattern)
/// Coordinates all game systems
/// </summary>
public interface IGameEngine
{
    IGameState GameState { get; }
    IWorld World { get; }
    IParser Parser { get; }
    ICommandFactory CommandFactory { get; }
    IOutputWriter Output { get; }
    IGameClock Clock { get; }

    void Initialize();
    void Start();
    void ProcessCommand(string input);
    void DisplayCurrentRoom();
}
