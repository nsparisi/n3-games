using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CyborgPunch.Game
{
    public enum Facing { Up = 0, Left = 1, Down = 2, Right = 3}
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

        public static Vector2 RotateVector(Vector2 vector, float rotation)
        {
            return Vector2.Transform(vector, Quaternion.CreateFromAxisAngle(Vector3.UnitZ, rotation));
        }

        //turn left or right
        public static Facing FacingTurnFacing(Facing facing, Facing turn)
        {
            Facing retVal = Facing.Down;
            switch (facing)
            {
                case Facing.Up:
                    if (turn == Facing.Left)
                        retVal = Facing.Left;
                    else
                        retVal = Facing.Right;
                    break;
                case Facing.Left:
                    if (turn == Facing.Left)
                        retVal = Facing.Down;
                    else
                        retVal = Facing.Up;
                    break;
                case Facing.Right:
                    if (turn == Facing.Left)
                        retVal = Facing.Up;
                    else
                        retVal = Facing.Down;
                    break;
                case Facing.Down:
                    if (turn == Facing.Left)
                        retVal = Facing.Right;
                    else
                        retVal = Facing.Left;
                    break;
            }

            return retVal;
        }
    }
}
