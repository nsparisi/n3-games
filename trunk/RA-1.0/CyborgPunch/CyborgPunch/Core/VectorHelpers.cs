using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CyborgPunch.Core
{
    static class VectorHelpers
    {
        public static Vector2 MoveTowards(Vector2 from, Vector2 to, float speed)
        {
            if (from == to)
                return to;

            Vector2 directionVector = (to - from);
            float magnitude = directionVector.Length();
            Vector2 moveVector = directionVector / magnitude;
            moveVector *= speed;

            Vector2 resultVector = from + moveVector;

            Vector2 afterDirectionVector = (to - resultVector);

            int oldXDiffSign = Math.Sign(directionVector.X);
            int oldYDiffSign = Math.Sign(directionVector.Y);
            int newXDiffSign = Math.Sign(afterDirectionVector.X);
            int newYDiffSign = Math.Sign(afterDirectionVector.Y);

            if (oldXDiffSign != newXDiffSign || oldYDiffSign != newYDiffSign)
            {
                return to;
            }
            else
            {
                return resultVector;
            }
        }
    }
}
