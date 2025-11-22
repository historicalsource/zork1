namespace ZorkSharp.Commands;

/// <summary>
/// Creates command instances (Factory Pattern + Dependency Inversion)
/// </summary>
public interface ICommandFactory
{
    ICommand? GetCommand(string verb);
    void RegisterCommand(ICommand command);
    IEnumerable<ICommand> GetAllCommands();
}
