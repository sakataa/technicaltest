using RobotSimulation.Console.Extensions;
using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Validators
{
    internal class CommandArgumentsValidator : ICommandArgumentsValidator
    {
        private readonly IPlaceCommandValidator _placeCommandValidator;

        public CommandArgumentsValidator(IPlaceCommandValidator placeCommandValidator)
        {
            _placeCommandValidator = placeCommandValidator;
        }

        public IEnumerable<string> Validate(string commandArg)
        {
            List<string> errorMessages = new();

            if (string.IsNullOrEmpty(commandArg))
            {
                errorMessages.Add("Command is required");

                return errorMessages;
            }

            string[] splittedCommand = commandArg.Split(" ");
            string commandName = splittedCommand[0].ToUpperInvariant();

            List<CommandType> validCommands = Enum.GetValues(typeof(CommandType)).Cast<CommandType>().ToList();

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

                IEnumerable<string> placeCommandValidationMessage = _placeCommandValidator.Validate(commandArguments);
                errorMessages.AddRange(placeCommandValidationMessage);
            }

            return errorMessages;
        }
    }
}