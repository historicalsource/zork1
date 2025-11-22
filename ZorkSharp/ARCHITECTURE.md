# ZorkSharp Architecture

## SOLID Principles Implementation

This document describes how ZorkSharp implements SOLID principles in its architecture.

## 1. Single Responsibility Principle (SRP)

Each class has a single, well-defined responsibility:

### Core Components
- **`GameState`**: Manages only game state (score, moves, current location)
- **`ConsoleInputReader`**: Only responsible for reading input
- **`ConsoleOutputWriter`**: Only responsible for writing output
- **`GameParser`**: Only responsible for parsing user input into commands

### World Management
- **`GameWorld`**: Manages rooms and objects
- **`Inventory`**: Manages player inventory
- **`Room`**: Represents a single location
- **`GameObject`**: Represents a single object

### Engine
- **`GameEngine`**: Coordinates game systems (Facade pattern)
- **`GameClock`**: Manages timed events

## 2. Open/Closed Principle (OCP)

The system is open for extension but closed for modification:

### Command System
New commands can be added without modifying existing code:

```csharp
// Add a new command by implementing ICommand
public class NewCommand : BaseCommand
{
    public override string Name => "newcmd";
    public override CommandResult Execute(...)
    {
        // Implementation
    }
}

// Register it in the factory
commandFactory.RegisterCommand(new NewCommand());
```

### Room and Object Behaviors
- Room actions can be extended through action handlers
- Object behaviors can be extended through flags and action handlers
- New object types can be added without changing existing code

## 3. Liskov Substitution Principle (LSP)

All implementations are substitutable for their interfaces:

### Command Substitution
```csharp
ICommand command = new TakeCommand();  // Can be any ICommand
command.Execute(parsedCommand, world, gameState);
```

### World Substitution
```csharp
IWorld world = new GameWorld();  // Could be TestWorld, NetworkWorld, etc.
var obj = world.GetObject("SWORD");
```

All derived classes properly implement their base interface contracts without breaking expectations.

## 4. Interface Segregation Principle (ISP)

The system uses many focused interfaces instead of monolithic ones:

### Focused Interfaces
- **`IInputReader`**: Only `ReadLine()`
- **`IOutputWriter`**: Only output methods (`Write`, `WriteLine`, etc.)
- **`IParser`**: Only parsing methods
- **`ICommand`**: Only command execution
- **`IGameClock`**: Only time management
- **`IRoom`**: Only room properties and exits
- **`IGameObject`**: Only object properties and flags

Clients only depend on the interfaces they actually use.

## 5. Dependency Inversion Principle (DIP)

High-level modules depend on abstractions, not concretions:

### Dependency Injection in GameEngine
```csharp
public class GameEngine : IGameEngine
{
    public GameEngine(
        IGameState gameState,        // Abstraction
        IWorld world,                 // Abstraction
        IParser parser,               // Abstraction
        ICommandFactory commandFactory, // Abstraction
        IOutputWriter output,         // Abstraction
        IInputReader input,           // Abstraction
        IGameClock clock)             // Abstraction
    {
        // All dependencies are injected as interfaces
    }
}
```

### Program.cs (Composition Root)
```csharp
// Create concrete implementations
var output = new ConsoleOutputWriter();
var input = new ConsoleInputReader();
var gameState = new GameState();
var world = new GameWorld();

// Inject into high-level module
var gameEngine = new GameEngine(
    gameState,
    world,
    parser,
    commandFactory,
    output,
    input,
    clock
);
```

## Design Patterns Used

### 1. Command Pattern
- **Interface**: `ICommand`
- **Base Class**: `BaseCommand`
- **Concrete Commands**: `MovementCommand`, `TakeCommand`, `DropCommand`, etc.
- **Invoker**: `GameEngine`
- **Receiver**: `IWorld`, `IGameState`

Benefits:
- Commands are objects that can be queued, logged, undone
- Easy to add new commands
- Separates command execution from command representation

### 2. Factory Pattern
- **Interface**: `ICommandFactory`
- **Implementation**: `CommandFactory`
- **Products**: `ICommand` implementations

Benefits:
- Centralized command creation
- Easy command registration
- Supports aliases

### 3. Strategy Pattern
- Room and object behaviors vary based on flags
- Different exit types (conditional, locked, etc.)
- Parser strategies for different command types

### 4. Template Method Pattern
- **Base Class**: `BaseCommand`
- **Template Methods**: `Execute`, `CanExecute`
- **Hook Methods**: Overridden in derived commands

### 5. Facade Pattern
- **Facade**: `GameEngine`
- **Subsystems**: Parser, World, Commands, Clock, State
- Provides simplified interface to complex subsystems

### 6. Builder Pattern
- **Builder**: `WorldBuilder`
- **Product**: `GameWorld` with rooms and objects
- Separates construction from representation

## Architecture Layers

```
┌─────────────────────────────────────────┐
│         Presentation Layer              │
│   (ConsoleInputReader, OutputWriter)    │
└─────────────────────────────────────────┘
                    │
                    ▼
┌─────────────────────────────────────────┐
│          Application Layer              │
│          (GameEngine)                   │
└─────────────────────────────────────────┘
                    │
        ┌───────────┼───────────┐
        ▼           ▼           ▼
┌─────────────┐ ┌────────┐ ┌─────────┐
│   Parser    │ │Commands│ │  Clock  │
│  (Domain)   │ │(Domain)│ │(Domain) │
└─────────────┘ └────────┘ └─────────┘
        │           │           │
        └───────────┼───────────┘
                    ▼
        ┌───────────────────────┐
        │    Domain Layer       │
        │  (World, GameState)   │
        └───────────────────────┘
                    │
                    ▼
        ┌───────────────────────┐
        │     Data Layer        │
        │   (WorldBuilder)      │
        └───────────────────────┘
```

## Benefits of This Architecture

### 1. Testability
- All components can be unit tested in isolation
- Interfaces allow easy mocking
- No tight coupling

### 2. Maintainability
- Clear separation of concerns
- Easy to locate and fix bugs
- Each class has one reason to change

### 3. Extensibility
- New commands: Implement `ICommand`
- New room types: Extend `Room`
- New object types: Extend `GameObject`
- New input/output: Implement `IInputReader`/`IOutputWriter`
- New game mechanics: Add to appropriate subsystem

### 4. Flexibility
- Could swap console I/O for GUI, web, or network
- Could swap in-memory world for database-backed
- Could add AI parser instead of simple keyword parser
- Could add multiplayer support

## Example: Adding a New Feature

### Adding a "Say" Command

1. **Create Command** (Open/Closed)
```csharp
public class SayCommand : BaseCommand
{
    public override string Name => "say";
    public override string[] Aliases => new[] { "speak" };
    public override string Description => "Say something";

    public override CommandResult Execute(
        ParsedCommand parsedCommand,
        IWorld world,
        IGameState gameState)
    {
        if (parsedCommand.DirectObject == null)
            return Failed("What do you want to say?");

        return Success($"You say: \"{parsedCommand.DirectObject}\"");
    }
}
```

2. **Register Command** (Dependency Inversion)
```csharp
commandFactory.RegisterCommand(new SayCommand());
```

3. **No other code changes needed!** (Open/Closed Principle)

## Conclusion

This architecture demonstrates how SOLID principles and design patterns create a maintainable, extensible, and testable codebase. The original ZIL implementation was already well-architected; this C# version modernizes it while preserving its elegant design and adding contemporary software engineering practices.
