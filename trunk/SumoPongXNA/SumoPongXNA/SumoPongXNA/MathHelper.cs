using System;
using System.Collections.Generic;
using System.Linq;

namespace SumoPongXNA
{
    public class MathHelper
    {
        public static bool Passed(float start, float end, float target)
        {
            return Math.Abs(end - start) > Math.Abs(target - start) &&
                (end - start > 0) == (target - start > 0);
        }


        public static float Clamp(float min, float max, float val)
        {
            return Math.Min(Math.Max(min, val), max);
        }
    }
}
