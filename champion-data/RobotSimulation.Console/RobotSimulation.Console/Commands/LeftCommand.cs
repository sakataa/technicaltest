using RobotSimulation.Console.Extensions;
using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Commands
{
    public class LeftCommand : ICommand
    {
        public Location Handle(CommandArgument commandArgument)
        {
            commandArgument.EnsureNotNullForPositionAndFacing();

            return commandArgument.FacingType switch
            {
                FacingType.North => new Location(commandArgument.X!.Value, commandArgument.Y!.Value, FacingType.West),
                FacingType.West => new Location(commandArgument.X!.Value, commandArgument.Y!.Value, FacingType.South),
                FacingType.South => new Location(commandArgument.X!.Value, commandArgument.Y!.Value, FacingType.East),
                FacingType.East => new Location(commandArgument.X!.Value, commandArgument.Y!.Value, FacingType.North),
                _ => throw new ArgumentException(nameof(commandArgument.FacingType))
            };
        }
    }
}