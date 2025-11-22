# ZORK I - The Great Underground Empire

A modern C# .NET 9 implementation of the classic Zork I interactive fiction game.

## About

This project is a complete reimplementation of Zork I using modern C# and .NET 9, built with SOLID principles and clean architecture patterns. The original Zork I was created by Marc Blank, Dave Lebling, Bruce Daniels, and Tim Anderson at MIT and published by Infocom.

This implementation is based on the open-source release of the Zork I source code under the MIT License, made available through the collaboration of Microsoft, Xbox, and Activision.

## Architecture

This implementation follows SOLID principles:

### Single Responsibility Principle (SRP)
- Each class has one clear responsibility
- `IOutputWriter` handles only output
- `IParser` handles only parsing
- `IWorld` handles only world state management

### Open/Closed Principle (OCP)
- New commands can be added without modifying existing code
- Command pattern allows extensibility
- Room and object behaviors can be extended

### Liskov Substitution Principle (LSP)
- All command implementations are substitutable
- Interface contracts are properly maintained

### Interface Segregation Principle (ISP)
- Multiple focused interfaces instead of monolithic ones
- `IInputReader`, `IOutputWriter`, `IParser`, etc. are all separate

### Dependency Inversion Principle (DIP)
- High-level modules depend on abstractions
- Dependency injection used throughout
- Interfaces define contracts, not implementations

## Project Structure

```
ZorkSharp/
├── Core/              # Core types and interfaces
│   ├── IGameState.cs
│   ├── IInputReader.cs
│   ├── IOutputWriter.cs
│   ├── Direction.cs
│   └── GameFlags.cs
├── Engine/            # Game engine
│   ├── IGameEngine.cs
│   └── GameEngine.cs
├── Parser/            # Natural language parser
│   ├── IParser.cs
│   ├── GameParser.cs
│   └── ParsedCommand.cs
├── Commands/          # Command pattern implementations
│   ├── ICommand.cs
│   ├── BaseCommand.cs
│   ├── MovementCommand.cs
│   ├── TakeCommand.cs
│   └── ...
├── World/             # Game world (rooms, objects)
│   ├── IRoom.cs
│   ├── IGameObject.cs
│   ├── IWorld.cs
│   ├── Room.cs
│   ├── GameObject.cs
│   └── GameWorld.cs
├── Events/            # Clock and timed events
│   ├── IGameClock.cs
│   └── GameClock.cs
├── UI/                # User interface
│   ├── ConsoleInputReader.cs
│   └── ConsoleOutputWriter.cs
├── Data/              # Game world data
│   └── WorldBuilder.cs
└── Program.cs         # Entry point
```

## Building and Running

### Prerequisites
- .NET 9 SDK or later

### Build
```bash
cd ZorkSharp
dotnet build
```

### Run
```bash
dotnet run
```

## How to Play

### Basic Commands
- **Movement**: `north`, `south`, `east`, `west`, `up`, `down`, `in`, `out` (or `n`, `s`, `e`, `w`, `u`, `d`)
- **Look**: `look` or `l` - Look around the current room
- **Examine**: `examine <object>` or `x <object>` - Examine an object closely
- **Take**: `take <object>` or `get <object>` - Pick up an object
- **Drop**: `drop <object>` - Drop an object
- **Inventory**: `inventory` or `i` - See what you're carrying
- **Score**: `score` - See your current score
- **Help**: `help` - Show available commands
- **Quit**: `quit` - Exit the game

### Example Commands
```
> north
> take lamp
> open mailbox
> read leaflet
> go west
> examine painting
> take sword
```

The parser understands natural language, so you can type commands like:
- `take the brass lantern`
- `open door with key`
- `put sword in trophy case`

## Game World

This implementation includes:
- **30+ rooms** from the classic Zork I dungeon, including:
  - West of House (starting location)
  - The White House interior
  - The Great Underground Empire
  - Maze of twisty passages
  - And many more iconic locations

- **30+ objects**, including:
  - Brass lantern
  - Elvish sword
  - Treasures to collect
  - Tools and items to use

## Features

### Implemented
✅ Natural language parser
✅ Room navigation with exits
✅ Object interaction (take, drop, examine)
✅ Inventory system
✅ Lighting system (dark rooms, light sources)
✅ Container objects
✅ Scoring system
✅ Command pattern for extensibility
✅ Clock/event system for timed events
✅ SOLID architecture

### Future Enhancements
- Complete all 110 rooms from original Zork I
- Complete all 122 objects
- NPCs and combat system
- Puzzle implementations
- Save/load game state
- More complex object interactions
- Spell system
- Full Zork I puzzle implementation

## Technical Highlights

### Design Patterns Used
- **Command Pattern**: All game commands implement `ICommand`
- **Factory Pattern**: `CommandFactory` creates command instances
- **Strategy Pattern**: Different room and object behaviors
- **Template Method**: `BaseCommand` provides common functionality
- **Facade Pattern**: `GameEngine` coordinates all subsystems

### Modern C# Features
- **Records**: Used for `CommandResult`
- **Nullable reference types**: Enabled for type safety
- **Pattern matching**: Used in parser and commands
- **LINQ**: Used throughout for querying
- **Top-level statements**: Clean program entry point

## License

Original Zork I game © Infocom, Inc.
Open source implementation under MIT License

This implementation is created for educational purposes to demonstrate:
- Classic interactive fiction game mechanics
- SOLID principles in C#
- Modern .NET architecture
- Natural language parsing
- Game state management

## Credits

**Original Zork I Authors:**
- Marc Blank
- Dave Lebling
- Bruce Daniels
- Tim Anderson

**Open Source Release:**
- Microsoft, Xbox, and Activision
- Jason Scott (Internet Archive)

**C# .NET 9 Implementation:**
- Claude (Anthropic AI)

## Contributing

This project demonstrates SOLID principles and clean architecture. Feel free to extend it with:
- Additional commands
- More rooms and objects
- New game mechanics
- Improved parser intelligence
- Save/load functionality
- Enhanced UI

When contributing, please maintain the SOLID principles and existing architecture patterns.
