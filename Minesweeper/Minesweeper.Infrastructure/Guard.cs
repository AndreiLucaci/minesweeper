using System;

namespace Minesweeper.Infrastructure
{
    public class Guard
    {
        public static void ArgumentNotNull(object obj, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName, $"The {paramName} should not be null.");
            }
        }
    }
}
