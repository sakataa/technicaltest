using RobotSimulation.Console.Extensions;
using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Validators
{
    internal class CommandArgumentsValidator : ICommandArgumentsValidator
    {
        public IEnumerable<string> Validate(string commandArg)
        {
            List<string> errorMessages = new();

            List<CommandType> validCommands = Enum.GetValues(typeof(CommandType)).Cast<CommandType>().ToList();

            if (string.IsNullOrEmpty(commandArg))
            {
                errorMessages.Add(commandArg);

                return errorMessages;
            }

            string[] splittedCommand = commandArg.Split(" ");
            string commandName = splittedCommand[0].ToUpperInvariant();

            IEnumerable<string> validCommandNamesString = validCommands.Select(x => x.ToString().ToUpperInvariant());
            bool isValidCommand = validCommandNamesString.Contains(commandName);

            if (!isValidCommand)
            {
                string errorMessage = $"Command must be in range of the following values {string.Join(", ", validCommandNamesString)}";

                errorMessages.Add(errorMessage);

                return errorMessages;
            }

            if (commandName.Match(CommandType.Place.ToString()))
            {
                string commandArguments = splittedCommand.Length > 1 ? splittedCommand[1] : string.Empty;
                string placeCommandErrorMessage = $"{commandName} must contain position X,Y and facing NORTH, SOUTH, EAST or WEST. E.g PLACE 0,0,NORTH";

                if (string.IsNullOrWhiteSpace(commandArguments))
                {
                    errorMessages.Add(placeCommandErrorMessage);

                    return errorMessages;
                }
                var splittedValues = commandArguments.Split(",");

                if (splittedValues.Length != 3)
                {
                    errorMessages.Add(placeCommandErrorMessage);

                    return errorMessages;
                }

                string x = splittedValues[0];
                string y = splittedValues[1];
                string f = splittedValues[2];

                if (!Enum.TryParse(f, true, out FacingType _))
                {
                    errorMessages.Add("Facing values must be in NORTH, SOUTH, EAST or WEST");

                    return errorMessages;
                }

                if (!int.TryParse(x, out int xPoint) || xPoint < 0 || xPoint > 5)
                {
                    errorMessages.Add("Position X value must be a number and between 0 and 4");

                    return errorMessages;
                }

                if (!int.TryParse(y, out int yPoint) || yPoint < 0 || yPoint > 5)
                {
                    errorMessages.Add("Position Y value must be a number and between 0 and 4");

                    return errorMessages;
                }
            }

            return errorMessages;
        }
    }
}