using RobotSimulation.Console.Extensions;
using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Utilities
{
    public static class CommandArgumentParser
    {
        public static CommandArgument FromCommandLine(string commandLineFromCli)
        {
            (string CommandName, IEnumerable<string> Arguments) = ToCommandNameAndArguments(commandLineFromCli);
            CommandType commandType = Enum.Parse<CommandType>(CommandName, true);

            if (CommandName.Match(CommandType.Place.ToString()))
            {
                int x = int.Parse(Arguments.ElementAt(0));
                int y = int.Parse(Arguments.ElementAt(1));
                FacingType facing = Enum.Parse<FacingType>(Arguments.ElementAt(2), true);

                return new CommandArgument(commandType, x, y, facing);
            }

            return new CommandArgument(commandType);
        }

        public static (string CommandName, IEnumerable<string> Arguments) ToCommandNameAndArguments(string commandLineFromCli)
        {
            List<string> arguments = new();

            string[] splittedCommands = commandLineFromCli.Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();

            string[] splittedFirstPart = splittedCommands[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();

            string commandName = splittedFirstPart[0].ToUpperInvariant();
            string placeCommandName = CommandType.Place.ToString().ToUpperInvariant();
            bool isPlaceCommand = commandName.Match(placeCommandName);

            if (isPlaceCommand && splittedFirstPart.Length == 2)
            {
                string firstArgument = splittedFirstPart[1];
                IEnumerable<string> lastArguments = splittedCommands.Skip(1);
                arguments.Add(firstArgument);
                arguments.AddRange(lastArguments);
            }

            return (commandName, arguments);
        }
    }
}