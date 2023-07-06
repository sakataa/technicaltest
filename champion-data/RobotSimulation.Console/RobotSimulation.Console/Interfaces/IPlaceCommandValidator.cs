namespace RobotSimulation.Console.Interfaces
{
    public interface IPlaceCommandValidator
    {
        IEnumerable<string> Validate(IEnumerable<string> commandArguments);
    }
}