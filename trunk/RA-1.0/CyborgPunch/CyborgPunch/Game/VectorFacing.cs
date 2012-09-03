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
            double angle = GetProperAngle(vector);
            angle += (double)rotation;
            Vector2 newDirection = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            return newDirection * vector.Length();
        }

        public static double GetProperAngle(Vector2 vector)
        {
            if (vector.X == 0 && vector.Y == 0)
            {
                return 0;
            }

            double angle = Math.Atan((double)(vector.Y / vector.X));

            //q1
            if (vector.X >= 0 && vector.Y >= 0)
            {
                angle += 0;
            }
            //q2
            else if (vector.X <= 0 && vector.Y >= 0)
            {
                angle += Math.PI;
            }
            //q3
            else if (vector.X < 0 && vector.Y <= 0)
            {
                angle += Math.PI;
            }
            //q4
            else if (vector.X >= 0 && vector.Y <= 0)
            {
                angle += 2 * Math.PI;
            }

            return angle;
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

        public static float FacingToPi(Facing facing)
        {
            switch(facing)
            {
                case Facing.Down:
                    return 0f;
                case Facing.Left:
                    return MathHelper.PiOver2;
                case Facing.Right:
                    return -MathHelper.PiOver2;
                case Facing.Up:
                    return MathHelper.Pi;
            }
            return 0f;
        }
    }
}
