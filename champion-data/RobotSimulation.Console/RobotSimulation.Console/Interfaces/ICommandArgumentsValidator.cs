namespace RobotSimulation.Console.Interfaces
{
    public interface ICommandArgumentsValidator
    {
        IEnumerable<string> Validate(string commandArg);
    }
}