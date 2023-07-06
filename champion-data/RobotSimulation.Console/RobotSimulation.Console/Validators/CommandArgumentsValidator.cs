using RobotSimulation.Console.Extensions;
using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;
using RobotSimulation.Console.Utilities;

namespace RobotSimulation.Console.Validators
{
    internal class CommandArgumentsValidator : ICommandArgumentsValidator
    {
        private readonly IPlaceCommandValidator _placeCommandValidator;

        public CommandArgumentsValidator(IPlaceCommandValidator placeCommandValidator)
        {
            _placeCommandValidator = placeCommandValidator;
        }

        public IEnumerable<string> Validate(string commandLine)
        {
            commandLine = commandLine.Trim();
            List<string> errorMessages = new();

            if (string.IsNullOrEmpty(commandLine))
            {
                errorMessages.Add("Command is required");

                return errorMessages;
            }

            (string CommandName, IEnumerable<string> Arguments) = CommandArgumentParser.ToCommandNameAndArguments(commandLine);

            List<string> validCommandNamesString = Enum.GetValues(typeof(CommandType))
                .Cast<CommandType>()
                .Select(x => x.ToString().ToUpperInvariant())
                .ToList();

            bool isValidCommand = validCommandNamesString.Contains(CommandName);

            if (!isValidCommand)
            {
                string errorMessage = $"Command must be in range of the following values {string.Join(", ", validCommandNamesString)}";

                errorMessages.Add(errorMessage);

                return errorMessages;
            }

            if (CommandName.Match(CommandType.Place.ToString()))
            {
                IEnumerable<string> placeCommandValidationMessage = _placeCommandValidator.Validate(Arguments);
                errorMessages.AddRange(placeCommandValidationMessage);
            }

            return errorMessages;
        }
    }
}