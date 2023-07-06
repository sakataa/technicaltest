using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;

namespace RobotSimulation.Console
{
    internal class FileModeRunner : IRunner
    {
        private readonly IRobotMovementService _robotMovementService;

        public FileModeRunner(IRobotMovementService robotMovementService)
        {
            _robotMovementService = robotMovementService;
        }

        public void Run()
        {
            string baseDirectory = AppContext.BaseDirectory;
            string fullFilePath = Path.Combine(baseDirectory, "TestFiles", "commands.txt");

            if (!File.Exists(fullFilePath))
            {
                System.Console.WriteLine("File not found");
                return;
            }

            Location? robotLocation = null;
            IEnumerable<string> commandLines = File.ReadLines(fullFilePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Trim());

            foreach (string commandLine in commandLines)
            {
                OperationResult result = _robotMovementService.Move(commandLine, robotLocation);

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
    }
}