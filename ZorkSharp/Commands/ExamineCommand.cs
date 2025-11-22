namespace ZorkSharp.Commands;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;

/// <summary>
/// Examines an object
/// </summary>
public class ExamineCommand : BaseCommand
{
    public override string Name => "examine";
    public override string[] Aliases => new[] { "x", "inspect" };
    public override string Description => "Examine an object";

    public override CommandResult Execute(ParsedCommand parsedCommand, IWorld world, IGameState gameState)
    {
        if (parsedCommand.DirectObject == null)
            return Failed("What do you want to examine?");

        var obj = FindObject(parsedCommand.DirectObject, world, gameState);
        if (obj == null)
            return Failed($"You don't see any {parsedCommand.DirectObject} here.");

        return Success(obj.Description);
    }
}
