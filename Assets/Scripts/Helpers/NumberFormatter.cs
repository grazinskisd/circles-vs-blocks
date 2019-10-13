using System.Text;

namespace CvB
{
    public class NumberFormatter
    {
        private const string FORMAT = "{0:0.##}{1}";

        private static string[] FIRST_SUFFIXES = new string[]
        {
            "K", "M", "B", "T", "Q"
        };

        public static string AsSufixed(float num)
        {
            int numBase = 0;
            while(num > 1000)
            {
                numBase += 1;
                num /= 1000;
            }
            return string.Format(FORMAT, num, GetSuffix(numBase));
        }

        /// <summary>
        /// Get suffix string for specified number base
        /// </summary>
        /// <param name="numBase">is the number base but as a multiple of 3</param>
        /// <returns></returns>
        private static string GetSuffix(int numBase)
        {
            if(numBase == 0)
            {
                return "";
            }

            if(numBase-1 < FIRST_SUFFIXES.Length)
            {
                return FIRST_SUFFIXES[numBase-1];
            }
            else
            {
                return IntoToBase26(numBase - FIRST_SUFFIXES.Length - 1);
            }
        }

        /// <summary>
        /// Custom base class that converts integer to base 26 represented in letters 'a' to 'z'
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string IntoToBase26(int value)
        {
            StringBuilder builder = new StringBuilder();
            string result = string.Empty;
            int targetBase = 26;

            while (value >= 0)
            {
                builder.Insert(0, (char)('a' + (value % targetBase)));
                value = (value / targetBase) - 1;
            };

            return builder.ToString();
        }
    }
}
