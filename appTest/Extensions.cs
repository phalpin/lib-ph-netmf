using System;
using Microsoft.SPOT;

namespace appTest
{
    public static class Extensions
    {
        public static bool InRange(this float f, int bottom, int top)
        {
            return f >= bottom && f <= top;
        }
    }
}
