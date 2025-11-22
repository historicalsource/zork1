namespace ZorkSharp.Commands;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;
using System.Text;

/// <summary>
/// Displays player inventory
/// </summary>
public class InventoryCommand : BaseCommand
{
    public override string Name => "inventory";
    public override string[] Aliases => new[] { "i", "inv" };
    public override string Description => "Show your inventory";

    public override CommandResult Execute(ParsedCommand parsedCommand, IWorld world, IGameState gameState)
    {
        var items = world.PlayerInventory.GetAllItems();

        if (items.Count == 0)
            return Success("You are empty-handed.");

        var sb = new StringBuilder();
        sb.AppendLine("You are carrying:");

        foreach (var item in items)
        {
            sb.AppendLine($"  {item.Name}");
        }

        return Success(sb.ToString().TrimEnd());
    }
}
