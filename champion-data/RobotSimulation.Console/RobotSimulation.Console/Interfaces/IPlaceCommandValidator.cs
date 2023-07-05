namespace RobotSimulation.Console.Interfaces
{
    public interface IPlaceCommandValidator
    {
        IEnumerable<string> Validate(string commandArguments);
    }
}