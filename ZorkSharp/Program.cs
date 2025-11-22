using ZorkSharp.Core;
using ZorkSharp.Engine;
using ZorkSharp.Parser;
using ZorkSharp.World;
using ZorkSharp.Commands;
using ZorkSharp.Events;
using ZorkSharp.UI;
using ZorkSharp.Data;

namespace ZorkSharp;

/// <summary>
/// Main program entry point
/// Demonstrates Dependency Injection and Inversion of Control (SOLID principles)
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // Create dependencies (Dependency Injection)
        var output = new ConsoleOutputWriter();
        var input = new ConsoleInputReader();
        var gameState = new GameState();
        var world = new GameWorld();
        var parser = new GameParser();
        var commandFactory = new CommandFactory();
        var clock = new GameClock(output);

        // Build the game world
        var worldBuilder = new WorldBuilder(world);
        worldBuilder.BuildWorld();

        // Create and initialize the game engine
        var gameEngine = new GameEngine(
            gameState,
            world,
            parser,
            commandFactory,
            output,
            input,
            clock
        );

        gameEngine.Initialize();

        // Start the game
        try
        {
            gameEngine.Start();
        }
        catch (Exception ex)
        {
            output.WriteLine($"\nAn error occurred: {ex.Message}");
            output.WriteLine("The game will now exit.");
        }
    }
}
