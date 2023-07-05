using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Interfaces
{
    public interface ICommandFactory
    {
        ICommand GetCommand(CommandType type);
    }
}