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
            Vector2 directionVector = (to - from);
            float magnitude = directionVector.Length();
            Vector2 moveVector = directionVector / magnitude;
            moveVector *= speed;

            Vector2 resultVector = from + directionVector;

            int oldXDiffSign;
            int ySign;

            return resultVector;
        }
    }
}
