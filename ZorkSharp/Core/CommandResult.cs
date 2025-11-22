namespace ZorkSharp.Core;

/// <summary>
/// Represents the result of command execution
/// </summary>
public enum CommandStatus
{
    Success,
    Failed,
    NotHandled,
    Fatal,
    Exit
}

/// <summary>
/// Result of a command execution
/// </summary>
public record CommandResult(
    CommandStatus Status,
    string? Message = null,
    bool ShouldDisplayRoom = false
);
