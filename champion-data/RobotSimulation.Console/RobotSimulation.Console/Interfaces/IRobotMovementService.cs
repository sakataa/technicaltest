using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Interfaces
{
    public interface IRobotMovementService
    {
        OperationResult Move(string commandLine, Location? currentLocation);
    }
}