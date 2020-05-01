namespace Core.Framework.Extensions
{
    public static class NumberExtensions
    {
        public static bool InRange(this int value, int lowerRange = 0, int upperRange = 30)
        {
            return lowerRange <= value && value <= upperRange;
        }

        public static bool InRange(this decimal value, decimal lowerRange = 0, decimal upperRange = 30)
        {
            return lowerRange <= value && value <= upperRange;
        }
    }
}
