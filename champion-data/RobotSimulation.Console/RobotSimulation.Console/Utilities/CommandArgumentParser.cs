using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Utilities
{
    public static class CommandArgumentParser
    {
        public static CommandArgument FromCommandLineInput(string commandArgument)
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

        public static IEnumerable<CommandArgument> FromFile(string filePath)
        {
            List<CommandArgument> result = new();

            string baseDirectory = AppContext.BaseDirectory;
            string fullFilePath = Path.Combine(baseDirectory, filePath);

            if (!File.Exists(fullFilePath))
            {
                return result;
            }

            foreach (string line in File.ReadLines(fullFilePath))
            {
                result.Add(FromCommandLineInput(line));
            }

            return result;
        }
    }
}