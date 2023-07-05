using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Utilities
{
    public static class CommandArgumentParser
    {
        public static CommandArgument FromCommandLine(string commandArgument)
        {
            string[] splittedCommand = commandArgument.Split(" ");
            CommandType commandType = Enum.Parse<CommandType>(splittedCommand[0], true);

            if (commandType == CommandType.Place)
            {
                string commandArguments = splittedCommand[1];
                string[] splittedValues = commandArguments.Split(",");

                int x = int.Parse(splittedValues[0]);
                int y = int.Parse(splittedValues[1]);
                FacingType facing = Enum.Parse<FacingType>(splittedValues[2], true);

                return new CommandArgument(commandType, x, y, facing);
            }

            return new CommandArgument(commandType);
        }
    }
}