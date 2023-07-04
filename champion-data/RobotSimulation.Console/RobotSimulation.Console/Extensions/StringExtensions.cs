namespace RobotSimulation.Console.Extensions
{
    public static class StringExtensions
    {
        public static bool Match(this string source, string destination, bool caseSensitive = false)
        {
            if (string.IsNullOrWhiteSpace(source) && string.IsNullOrWhiteSpace(destination))
            {
                return true;
            }
            else if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }

            var comparision = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            return source.Equals(destination, comparision);
        }
    }
}