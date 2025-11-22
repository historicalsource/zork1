namespace ZorkSharp.Events;

using ZorkSharp.Core;
using ZorkSharp.World;

/// <summary>
/// Manages timed events and game clock
/// </summary>
public class GameClock : IGameClock
{
    private readonly List<IClockEvent> _demons = new();      // Always run
    private readonly List<IClockEvent> _interrupts = new();  // Run when player acts
    private readonly IOutputWriter _output;
    private int _tickCount = 0;

    public GameClock(IOutputWriter output)
    {
        _output = output;
    }

    public void Tick(IGameState gameState, IWorld world)
    {
        _tickCount++;

        // Execute demons (always run)
        foreach (var demon in _demons.Where(d => d.IsActive).ToList())
        {
            if (_tickCount >= demon.NextTrigger)
            {
                demon.Execute(gameState, world, _output);
                demon.NextTrigger = _tickCount + demon.Interval;
            }
        }

        // Execute interrupts (run when player acts)
        foreach (var interrupt in _interrupts.Where(i => i.IsActive).ToList())
        {
            if (_tickCount >= interrupt.NextTrigger)
            {
                interrupt.Execute(gameState, world, _output);
                interrupt.NextTrigger = _tickCount + interrupt.Interval;
            }
        }
    }

    public void RegisterDemon(IClockEvent clockEvent)
    {
        _demons.Add(clockEvent);
        clockEvent.NextTrigger = _tickCount + clockEvent.Interval;
    }

    public void RegisterInterrupt(IClockEvent clockEvent)
    {
        _interrupts.Add(clockEvent);
        clockEvent.NextTrigger = _tickCount + clockEvent.Interval;
    }

    public void RemoveEvent(string eventId)
    {
        _demons.RemoveAll(d => d.Id == eventId);
        _interrupts.RemoveAll(i => i.Id == eventId);
    }
}

/// <summary>
/// Base class for clock events
/// </summary>
public abstract class ClockEventBase : IClockEvent
{
    public string Id { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int Interval { get; set; } = 1;
    public int NextTrigger { get; set; } = 0;

    public abstract void Execute(IGameState gameState, IWorld world, IOutputWriter output);
}
