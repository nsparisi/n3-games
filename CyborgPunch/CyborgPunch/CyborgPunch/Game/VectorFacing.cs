using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CyborgPunch.Game
{
    public enum Facing { Up, Down, Left, Right }
    public class VectorFacing
    {
        //assumes vector given is for facing down
        public static Vector2 RotateVectorToFacing(Vector2 vector, Facing facing)
        {
            Vector2 newVector = Vector2.Zero;
            switch (facing)
            {
                case Facing.Up:
                    newVector.X = -vector.X;
                    newVector.Y = -vector.Y;
                    break;
                case Facing.Down:
                    newVector.X = vector.X;
                    newVector.Y = vector.Y;
                    break;
                case Facing.Left:
                    newVector.X = -vector.Y;
                    newVector.Y = -vector.X;
                    break;
                case Facing.Right:
                    newVector.X = vector.Y;
                    newVector.Y = -vector.X;
                    break;
            }

            return newVector;
        }
    }
}
