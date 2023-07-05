using Microsoft.Extensions.DependencyInjection;
using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;
using System.Text;

namespace RobotSimulation.Console
{
    public class AppRunner
    {
        private readonly IServiceProvider _serviceProvider;

        public AppRunner(IServiceProvider serviceProvider)
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
                System.Console.WriteLine("Please select input mode (enter 1 or 2):");
                System.Console.WriteLine("1.Command line");
                System.Console.WriteLine("2.File");

                string selectedModeFromCli = System.Console.ReadLine() ?? string.Empty;

                if (Enum.TryParse(selectedModeFromCli, true, out InputMode mode))
                {
                    isValidMode = true;
                    selectedMode = mode;
                }
            }

            try
            {
                IRobotMovementService robotMovementService = _serviceProvider.GetRequiredService<IRobotMovementService>();

                IRunner runner = selectedMode switch
                {
                    InputMode.CommandLine => new CommandLineModeRunner(robotMovementService),
                    InputMode.File => new FileModeRunner(robotMovementService),
                    _ => throw new InvalidOperationException("Invalid run mode")
                };

                runner.Run();
            }
            catch
            {
                System.Console.WriteLine("Oops, something went wrong. Please try again or contact the author ^_^");
            }

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey(true);
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
    }
}