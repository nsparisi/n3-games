
using Microsoft.Xna.Framework;

namespace CyborgPunch.Core
{
    class Time
    {
        public static float deltaTime;

        public Time()
        {
        }

        public void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
