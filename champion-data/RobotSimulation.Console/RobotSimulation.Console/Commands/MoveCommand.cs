using RobotSimulation.Console.Extensions;
using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Commands
{
    public class MoveCommand : ICommand
    {
        public Location Handle(CommandArgument commandArgument)
        {
            commandArgument.EnsureNotNullForPositionAndFacing();

            return commandArgument.FacingType!.Value switch
            {
                FacingType.North => new Location(commandArgument.X!.Value, commandArgument.Y!.Value + 1, commandArgument.FacingType.Value),
                FacingType.South => new Location(commandArgument.X!.Value, commandArgument.Y!.Value - 1, commandArgument.FacingType.Value),
                FacingType.East => new Location(commandArgument.X!.Value + 1, commandArgument.Y!.Value, commandArgument.FacingType.Value),
                FacingType.West => new Location(commandArgument.X!.Value - 1, commandArgument.Y!.Value, commandArgument.FacingType.Value),
                _ => throw new ArgumentException(nameof(commandArgument.FacingType))
            };
        }
    }
}