namespace RobotSimulation.Console.Extensions
{
    public static class EnumExtensions
    {
        public static bool IsValidValue<EnumType>(this EnumType value) where EnumType : Enum
            => Enum.IsDefined(typeof(EnumType), value);
    }
}