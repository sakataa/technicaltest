using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;

namespace RobotSimulation.Console
{
    internal class CommandLineModeRunner : IRunner
    {
        private readonly IRobotMovementService _robotMovementService;

        public CommandLineModeRunner(IRobotMovementService robotMovementService)
        {
            _robotMovementService = robotMovementService;
        }

        public void Run()
        {
            Location? robotLocation = null;

            do
            {
                string promptMessage = robotLocation is null ? "Please enter the PLACE command first:" : "Please enter your next command:";
                System.Console.WriteLine(promptMessage);

                string commandLine = System.Console.ReadLine()?.Trim() ?? string.Empty;

                OperationResult result = _robotMovementService.Move(commandLine, robotLocation);

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
    }
}