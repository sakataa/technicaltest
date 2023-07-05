using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Commands
{
    internal class CommandFactory : ICommandFactory
    {
        public ICommand GetCommand(CommandType type)
        {
            return type switch
            {
                CommandType.Place => new PlaceCommand(),
                CommandType.Move => new MoveCommand(),
                CommandType.Left => new LeftCommand(),
                CommandType.Right => new RightCommand(),
                _ => throw new ArgumentException(nameof(type)),
            };
        }
    }
}