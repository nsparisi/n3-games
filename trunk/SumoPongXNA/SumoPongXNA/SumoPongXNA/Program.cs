using System;

namespace SumoPongXNA
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SumoPong game = new SumoPong())
            {
                game.Run();
            }
        }
    }
#endif
}

