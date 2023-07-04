using RobotSimulation.Console.Models;

namespace RobotSimulation.Console.Extensions
{
    public static class CommandArgumentExtensions
    {
        public static void EnsureNotNullForPositionAndFacing(this CommandArgument source)
        {
            if (source is null)
            {
                throw new ArgumentNullException("source");
            }

            if (!source.X.HasValue)
            {
                throw new InvalidOperationException($"{nameof(source.X)} is null");
            }

            if (!source.Y.HasValue)
            {
                throw new InvalidOperationException($"{nameof(source.Y)} is null");
            }

            if (!source.FacingType.HasValue)
            {
                throw new InvalidOperationException($"{nameof(source.FacingType)} is null");
            }
        }
    }
}