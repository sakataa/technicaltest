namespace RobotSimulation.Console.Models
{
    public record CommandArgument(CommandType CommandType, int? X = null, int? Y = null, FacingType? FacingType = null);
}