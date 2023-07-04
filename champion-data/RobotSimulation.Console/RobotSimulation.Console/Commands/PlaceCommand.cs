using RobotSimulation.Console.Extensions;
using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Commands
{
    internal class PlaceCommand : ICommand
    {
        public Location Handle(CommandArgument commandArgument)
        {
            commandArgument.EnsureNotNullForPositionAndFacing();

            return new Location(commandArgument.X!.Value, commandArgument.Y!.Value, commandArgument.FacingType!.Value);
        }
    }
}