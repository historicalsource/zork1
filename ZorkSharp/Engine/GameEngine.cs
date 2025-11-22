namespace ZorkSharp.Engine;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;
using ZorkSharp.Commands;
using ZorkSharp.Events;
using System.Text;

/// <summary>
/// Main game engine - coordinates all game systems
/// </summary>
public class GameEngine : IGameEngine
{
    public IGameState GameState { get; }
    public IWorld World { get; }
    public IParser Parser { get; }
    public ICommandFactory CommandFactory { get; }
    public IOutputWriter Output { get; }
    public IGameClock Clock { get; }

    private readonly IInputReader _input;

    public GameEngine(
        IGameState gameState,
        IWorld world,
        IParser parser,
        ICommandFactory commandFactory,
        IOutputWriter output,
        IInputReader input,
        IGameClock clock)
    {
        GameState = gameState;
        World = world;
        Parser = parser;
        CommandFactory = commandFactory;
        Output = output;
        _input = input;
        Clock = clock;
    }

    public void Initialize()
    {
        GameState.Initialize();
        World.Initialize();
        RegisterCommands();
    }

    private void RegisterCommands()
    {
        CommandFactory.RegisterCommand(new MovementCommand());
        CommandFactory.RegisterCommand(new LookCommand());
        CommandFactory.RegisterCommand(new ExamineCommand());
        CommandFactory.RegisterCommand(new InventoryCommand());
        CommandFactory.RegisterCommand(new TakeCommand());
        CommandFactory.RegisterCommand(new DropCommand());
        CommandFactory.RegisterCommand(new QuitCommand());
        CommandFactory.RegisterCommand(new ScoreCommand());
        CommandFactory.RegisterCommand(new HelpCommand(CommandFactory));
    }

    public void Start()
    {
        ShowIntroduction();
        DisplayCurrentRoom();

        while (GameState.IsRunning)
        {
            Output.Write("\n> ");
            var input = _input.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            ProcessCommand(input);

            // Tick the game clock after each command
            Clock.Tick(GameState, World);
        }

        ShowOutro();
    }

    public void ProcessCommand(string input)
    {
        GameState.IncrementMoves();

        var parsedCommand = Parser.Parse(input);

        if (!parsedCommand.IsValid)
        {
            Output.WriteLine(parsedCommand.ErrorMessage ?? "I don't understand that.");
            return;
        }

        var command = CommandFactory.GetCommand(parsedCommand.Verb);

        if (command == null)
        {
            Output.WriteLine($"I don't know how to \"{parsedCommand.Verb}\".");
            return;
        }

        if (!command.CanExecute(parsedCommand, World, GameState))
        {
            Output.WriteLine("You can't do that right now.");
            return;
        }

        var result = command.Execute(parsedCommand, World, GameState);

        if (result.Message != null)
        {
            Output.WriteLine(result.Message);
        }

        if (result.ShouldDisplayRoom)
        {
            DisplayCurrentRoom();
        }

        if (result.Status == CommandStatus.Exit)
        {
            Output.Write("Are you sure? (yes/no) ");
            var confirmation = _input.ReadLine()?.ToLowerInvariant();
            if (confirmation == "yes" || confirmation == "y")
            {
                GameState.IsRunning = false;
            }
            else
            {
                Output.WriteLine("Ok.");
            }
        }

        GameState.LastCommand = input;
    }

    public void DisplayCurrentRoom()
    {
        var room = World.GetRoom(GameState.CurrentRoomId);
        if (room == null)
        {
            Output.WriteLine("You are in a strange void.");
            return;
        }

        // Check if room is lit
        bool isLit = room.IsLit || HasLightSource();

        if (!isLit)
        {
            Output.WriteLine("It is pitch black. You are likely to be eaten by a grue.");
            return;
        }

        // Display room name
        Output.WriteLine($"\n{room.Name}");
        Output.WriteLine(new string('=', room.Name.Length));

        // Display room description
        if (!string.IsNullOrEmpty(room.LongDescription))
        {
            Output.WriteLine(room.LongDescription);
        }
        else
        {
            Output.WriteLine(room.Description);
        }

        // Display visible items
        var visibleItems = World.GetVisibleObjectsInRoom(GameState.CurrentRoomId);
        if (visibleItems.Any())
        {
            Output.WriteBlankLine();
            foreach (var item in visibleItems)
            {
                Output.WriteLine($"There is {GetArticle(item.Name)} {item.Name} here.");
            }
        }

        // Display exits
        DisplayExits(room);
    }

    private void DisplayExits(IRoom room)
    {
        if (!room.Exits.Any())
            return;

        var exits = room.Exits
            .Where(e => e.Value.DestinationRoomId != null)
            .Select(e => e.Key.ToString().ToLower())
            .ToList();

        if (exits.Any())
        {
            Output.WriteBlankLine();
            Output.WriteLine($"Obvious exits: {string.Join(", ", exits)}");
        }
    }

    private bool HasLightSource()
    {
        // Check if player has a light source in inventory
        var items = World.PlayerInventory.GetAllItems();
        return items.Any(item => item.ProvidesLight);
    }

    private string GetArticle(string noun)
    {
        if (string.IsNullOrEmpty(noun))
            return "a";

        char first = char.ToLower(noun[0]);
        return "aeiou".Contains(first) ? "an" : "a";
    }

    private void ShowIntroduction()
    {
        Output.Clear();
        Output.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
        Output.WriteLine("║                          ZORK I                                    ║");
        Output.WriteLine("║              The Great Underground Empire                          ║");
        Output.WriteLine("║                                                                    ║");
        Output.WriteLine("║                  Written by Marc Blank,                            ║");
        Output.WriteLine("║              Dave Lebling, Bruce Daniels, and                      ║");
        Output.WriteLine("║                      Tim Anderson                                  ║");
        Output.WriteLine("║                                                                    ║");
        Output.WriteLine("║              C# .NET 9 implementation by                           ║");
        Output.WriteLine("║                Claude (Anthropic AI)                               ║");
        Output.WriteLine("║                                                                    ║");
        Output.WriteLine("║            Original game © Infocom, Inc.                           ║");
        Output.WriteLine("║         Open source implementation under MIT License               ║");
        Output.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
        Output.WriteBlankLine();
        Output.WriteLine("Type 'help' for a list of commands.");
        Output.WriteBlankLine();
    }

    private void ShowOutro()
    {
        Output.WriteBlankLine();
        Output.WriteLine("Thank you for playing ZORK!");
        Output.WriteLine($"Final score: {GameState.Score} out of {GameState.MaxScore} in {GameState.Moves} moves.");
        Output.WriteBlankLine();
    }
}
