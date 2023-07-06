using RobotSimulation.Console.Constants;
using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Validators
{
    public class PlaceCommandValidator : IPlaceCommandValidator
    {
        public IEnumerable<string> Validate(IEnumerable<string> commandArguments)
        {
            List<string> errorMessages = new();

            if (!commandArguments.Any() || commandArguments.Count() != 3)
            {
                string placeCommandErrorMessage = $"PLACE command must contain position X,Y and facing NORTH, SOUTH, EAST or WEST. E.g PLACE 0,0,NORTH";
                errorMessages.Add(placeCommandErrorMessage);

                return errorMessages;
            }

            string[] splittedValues = commandArguments.Select(x => x.Trim()).ToArray();

            string x = splittedValues[0];
            string y = splittedValues[1];
            string f = splittedValues[2].ToUpperInvariant();

            IEnumerable<string> validFacingTypes = Enum.GetValues(typeof(FacingType)).Cast<FacingType>()
                .Select(x => x.ToString().ToUpperInvariant())
                .ToList();

            if (!validFacingTypes.Contains(f))
            {
                errorMessages.Add("Facing values must be in NORTH, SOUTH, EAST or WEST");

                return errorMessages;
            }

            int maxWidthIndex = AppConstants.TableWidth - 1;
            if (!int.TryParse(x, out int xPoint) || xPoint < 0 || xPoint > maxWidthIndex)
            {
                errorMessages.Add("Position X value must be a number and between 0 and 4");

                return errorMessages;
            }

            int maxYIndex = AppConstants.TableHeight - 1;
            if (!int.TryParse(y, out int yPoint) || yPoint < 0 || yPoint > maxYIndex)
            {
                errorMessages.Add("Position Y value must be a number and between 0 and 4");

                return errorMessages;
            }

            return errorMessages;
        }
    }
}