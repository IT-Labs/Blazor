using System;

namespace Core.Framework.Extensions
{
    public static class NumericExtensions
    {
        public static bool LessThanOrEqualToDecimalPlaces(this decimal dec, int decimalPlaces = 1)
        {
            double value = (double)dec * Math.Pow(10, 1);
            return value == Math.Floor(value);
        }    
    
        /// <summary>
        /// Converts the object to a intiger. If the object is null or not convertable then returns 0
        /// </summary>
        /// <param name="o">Object that shuld be returned</param>
        /// <returns>If Convertable Int value of the object, else 0</returns>
        public static Int32 ToInt32(this Object o)
        {
            Int32 result;
            if (o == null)
                return 0;
            else if (Int32.TryParse(o.ToString(), out result))
                return result;
            else
                return 0;
        }

        
    }
}
