using Microsoft.Xna.Framework;
using System;

namespace CyborgPunch.Core
{
    class Debug
    {
        public static void Log(Object obj)
        {
            Console.WriteLine(obj.ToString());
        }
    }
}
