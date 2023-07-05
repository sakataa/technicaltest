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
            System.Console.WriteLine();
            System.Console.WriteLine("Welcome to the robot simulator!");

            string introductionParagraph = BuildIntroductionParagraph();
            System.Console.WriteLine(introductionParagraph);

            //ConsoleKeyInfo consoleKey;

            Location? robotLocation = null;

            do
            {
                string promptMessage = robotLocation is null ? "Please enter the PLACE command first:" : "Please enter your next command:";
                System.Console.WriteLine(promptMessage);

                string enteredCommand = System.Console.ReadLine() ?? string.Empty;

                bool isPlaceCommand = enteredCommand.Split(" ")[0].Match(CommandType.Place.ToString());
                if (robotLocation is null && !isPlaceCommand)
                {
                    System.Console.WriteLine("The first valid command to the robot is a PLACE command");
                    continue;
                }

                ICommandArgumentsValidator validator = _serviceProvider.GetRequiredService<ICommandArgumentsValidator>();
                IEnumerable<string> validationMessages = validator.Validate(enteredCommand);

                if (validationMessages.Any())
                {
                    string validationMessage = validationMessages.First();
                    System.Console.WriteLine(validationMessage);
                    continue;
                }

                CommandArgument commandArgument = CommandArgumentParser.FromCommandLineInput(enteredCommand);

                if (commandArgument.CommandType == CommandType.Report && robotLocation is not null)
                {
                    System.Console.WriteLine($"Current position: {robotLocation.X},{robotLocation.Y},{robotLocation.FacingType.ToString().ToUpperInvariant()}");
                    break;
                }

                ICommandFactory commandFactory = _serviceProvider.GetRequiredService<ICommandFactory>();
                ICommand command = commandFactory.GetCommand(commandArgument.CommandType);

                if (commandArgument.CommandType != CommandType.Place && robotLocation is not null)
                {
                    commandArgument = new(commandArgument.CommandType, robotLocation.X, robotLocation.Y, robotLocation.FacingType);
                }

                Location candidateLocation = command.Handle(commandArgument);

                bool isValidLocation = _tablePositions.Any(p => p.X == candidateLocation.X && p.Y == candidateLocation.Y);

                if (isValidLocation)
                {
                    robotLocation = candidateLocation;
                }
                else
                {
                    System.Console.WriteLine($"Invalid location. The moving is ignored");
                }
                System.Console.WriteLine();
            } while (true);

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey(true);
        }

        private static string BuildIntroductionParagraph()
        {
            StringBuilder builder = new();
            builder.AppendLine("Please enter one of the following command forms:");
            builder.AppendLine();
            builder.AppendLine("PLACE X,Y,F");
            builder.AppendLine("MOVE");
            builder.AppendLine("LEFT");
            builder.AppendLine("RIGHT");
            builder.AppendLine("REPORT");
            builder.AppendLine();
            builder.AppendLine("\tPLACE will put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST.");
            builder.AppendLine("\tThe first valid command to the robot is a PLACE command, after that, any sequence of commands may be issued, in any order, including another PLACE command. The application will discard all commands in the sequence until a valid PLACE command has been executed");
            builder.AppendLine("\tMOVE will move the toy robot one unit forward in the direction it is currently facing.");
            builder.AppendLine("\tLEFT and RIGHT will rotate the robot 90 degrees in the specified direction without changing the position \r\nof the robot.");
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