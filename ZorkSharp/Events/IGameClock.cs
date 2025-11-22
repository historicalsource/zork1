namespace ZorkSharp.Events;

using ZorkSharp.Core;
using ZorkSharp.World;

/// <summary>
/// Manages timed events and interrupts
/// </summary>
public interface IGameClock
{
    void Tick(IGameState gameState, IWorld world);
    void RegisterDemon(IClockEvent clockEvent);
    void RegisterInterrupt(IClockEvent clockEvent);
    void RemoveEvent(string eventId);
}

/// <summary>
/// Represents a timed event
/// </summary>
public interface IClockEvent
{
    string Id { get; }
    bool IsActive { get; set; }
    int Interval { get; }
    int NextTrigger { get; set; }

    void Execute(IGameState gameState, IWorld world, IOutputWriter output);
}
