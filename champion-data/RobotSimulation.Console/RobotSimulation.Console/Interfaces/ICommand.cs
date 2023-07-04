using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Interfaces
{
    public interface ICommand
    {
        Location Handle(CommandArgument commandArgument);
    }
}