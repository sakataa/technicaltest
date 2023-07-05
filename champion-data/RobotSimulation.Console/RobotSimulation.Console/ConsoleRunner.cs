using Microsoft.Extensions.DependencyInjection;
using RobotSimulation.Console.Constants;
using RobotSimulation.Console.Extensions;
using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;
using RobotSimulation.Console.Utilities;
using System.Text;

namespace RobotSimulation.Console
{
    public class ConsoleRunner
    {
        private static IEnumerable<Point> _tablePositions = InitValidPositions();
        private readonly IServiceProvider _serviceProvider;

        public ConsoleRunner(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Run()
        {
            string introductionParagraph = BuildIntroductionParagraph();
            System.Console.WriteLine(introductionParagraph);

            bool isValidMode = false;
            InputMode selectedMode = InputMode.CommandLine;

            while (!isValidMode)
            {
                System.Console.WriteLine("Please select input mode:");
                System.Console.WriteLine("1.Command line");
                System.Console.WriteLine("2.File");

                string selectedModeFromCli = System.Console.ReadLine() ?? string.Empty;
                isValidMode = Enum.TryParse(selectedModeFromCli, true, out InputMode outMode);

                if (isValidMode)
                {
                    selectedMode = outMode;
                }
            }

            if (selectedMode == InputMode.CommandLine)
            {
                RunWithCommandLineMode();
            }
            else if (selectedMode == InputMode.File)
            {
                RunWithFileMode();
            }
            else
            {
                System.Console.WriteLine("Invalid run mode, please try again");
            }

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey(true);
        }

        private void RunWithCommandLineMode()
        {
            Location? robotLocation = null;

            do
            {
                string promptMessage = robotLocation is null ? "Please enter the PLACE command first:" : "Please enter your next command:";
                System.Console.WriteLine(promptMessage);

                string commandLine = System.Console.ReadLine() ?? string.Empty;

                OperationResult result = ProcessForNextMove(commandLine, robotLocation);

                if (!result.IsSuccessful && result.ShouldStopProcessing)
                {
                    System.Console.WriteLine(result.ErrorMessage);
                    continue;
                }

                if (result.IsSuccessful && !string.IsNullOrWhiteSpace(result.ReportMessage))
                {
                    System.Console.WriteLine(result.ReportMessage);
                }

                if (result.Value is not null)
                {
                    robotLocation = result.Value;
                }

                System.Console.WriteLine();
            } while (true);
        }

        private void RunWithFileMode()
        {
            string baseDirectory = AppContext.BaseDirectory;
            string fullFilePath = Path.Combine(baseDirectory, "TestFiles", "commands.txt");

            if (!File.Exists(fullFilePath))
            {
                System.Console.WriteLine("File not found");
            }

            Location? robotLocation = null;

            foreach (string enteredCommand in File.ReadLines(fullFilePath))
            {
                OperationResult result = ProcessForNextMove(enteredCommand, robotLocation);

                if (!result.IsSuccessful && result.ShouldStopProcessing)
                {
                    System.Console.WriteLine(result.ErrorMessage);
                    break;
                }

                if (result.IsSuccessful && !string.IsNullOrWhiteSpace(result.ReportMessage))
                {
                    System.Console.WriteLine(result.ReportMessage);
                }

                if (result.Value is not null)
                {
                    robotLocation = result.Value;
                }
            }
        }

        private OperationResult ProcessForNextMove(string commandLine, Location? currentLocation)
        {
            if (currentLocation is null)
            {
                OperationResult firstCommandValidationResult = ValidateFirstCommand(commandLine);

                if (!firstCommandValidationResult.IsSuccessful)
                {
                    return firstCommandValidationResult;
                }
            }

            ICommandArgumentsValidator validator = _serviceProvider.GetRequiredService<ICommandArgumentsValidator>();
            IEnumerable<string> validationMessages = validator.Validate(commandLine);

            if (validationMessages.Any())
            {
                return new OperationResult
                {
                    Value = currentLocation,
                    ErrorMessage = validationMessages.First(),
                    ShouldStopProcessing = false
                };
            }

            CommandArgument commandArgument = CommandArgumentParser.FromCommandLineInput(commandLine);

            if (commandArgument.CommandType == CommandType.Report && currentLocation is not null)
            {
                return new OperationResult
                {
                    Value = currentLocation,
                    ReportMessage = $"Current position: {currentLocation.X},{currentLocation.Y},{currentLocation.FacingType.ToString().ToUpperInvariant()}"
                };
            }

            Location? nextLocation = ProcessSingleMovement(commandArgument, currentLocation);
            return new OperationResult
            {
                Value = nextLocation,
            };
        }

        private Location? ProcessSingleMovement(CommandArgument commandArgument, Location? robotLocation)
        {
            ICommandFactory commandFactory = _serviceProvider.GetRequiredService<ICommandFactory>();
            ICommand command = commandFactory.GetCommand(commandArgument.CommandType);

            CommandArgument commandArgumentTohandle = commandArgument.CommandType != CommandType.Place && robotLocation is not null ?
                new(commandArgument.CommandType, robotLocation.X, robotLocation.Y, robotLocation.FacingType) :
                commandArgument;

            Location candidateLocation = command.Handle(commandArgumentTohandle);

            bool isValidLocation = _tablePositions.Any(p => p.X == candidateLocation.X && p.Y == candidateLocation.Y);

            return isValidLocation ? candidateLocation : robotLocation;
        }

        private OperationResult ValidateFirstCommand(string commandLine)
        {
            string[] commandNameAndArguments = commandLine.Split(" ");
            bool isPlaceCommand = commandNameAndArguments[0].Match(CommandType.Place.ToString());

            if (isPlaceCommand)
            {
                string commandArguments = commandNameAndArguments.Length > 1 ? commandNameAndArguments[1] : string.Empty;
                IPlaceCommandValidator placeCommandValidator = _serviceProvider.GetRequiredService<IPlaceCommandValidator>();
                IEnumerable<string> placeCommandValidationMessages = placeCommandValidator.Validate(commandArguments);

                if (placeCommandValidationMessages.Any())
                {
                    return new OperationResult
                    {
                        ErrorMessage = placeCommandValidationMessages.First(),
                        ShouldStopProcessing = true
                    };
                }
            }
            else
            {
                return new OperationResult
                {
                    ErrorMessage = "The first valid command to the robot is a PLACE command",
                    ShouldStopProcessing = true
                };
            }

            return new OperationResult();
        }

        private static string BuildIntroductionParagraph()
        {
            StringBuilder builder = new();
            builder.AppendLine("Welcome to the robot simulator!");
            builder.AppendLine();
            builder.AppendLine("Please enter one of the following command forms:");
            builder.AppendLine();
            builder.AppendLine("PLACE X,Y,F");
            builder.AppendLine("MOVE");
            builder.AppendLine("LEFT");
            builder.AppendLine("RIGHT");
            builder.AppendLine("REPORT");
            builder.AppendLine();
            builder.AppendLine("\tPLACE will put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST.");
            builder.AppendLine();
            builder.AppendLine("\tThe first valid command to the robot is a PLACE command, after that, any sequence of commands may be issued, in any order, including another PLACE command. The application will discard all commands in the sequence until a valid PLACE command has been executed");
            builder.AppendLine();
            builder.AppendLine("\tMOVE will move the toy robot one unit forward in the direction it is currently facing.");
            builder.AppendLine();
            builder.AppendLine("\tLEFT and RIGHT will rotate the robot 90 degrees in the specified direction without changing the position \r\nof the robot.");
            builder.AppendLine();
            builder.AppendLine("\tREPORT will announce the X,Y and F of the robot.");
            builder.AppendLine();

            return builder.ToString();
        }

        private static IEnumerable<Point> InitValidPositions()
        {
            List<Point> result = new();

            for (var x = 0; x < AppConstants.TableWidth; x++)
            {
                for (var y = 0; y < AppConstants.TableHeight; y++)
                {
                    result.Add(new Point(x, y));
                }
            }

            return result;
        }
    }
}