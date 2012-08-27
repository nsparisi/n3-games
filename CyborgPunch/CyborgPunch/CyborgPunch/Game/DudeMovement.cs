using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CyborgPunch.Game
{
    class DudeMovement : Component
    {
        Humanoid body;
        //public float speed = 300;

        float friction = .002f;

        public Vector2 speed;

        public DudeMovement(Humanoid body)
            : base()
        {
            this.body = body;
        }

        public override void Update()
        {
            base.Update();

            float movementForce = 1400;
            Vector2 force = Vector2.Zero;
            float speedModifier = Math.Max((LegCount() / 2f), .25f);
            if (Keyboard.GetState().IsKeyDown(KeyBindings.MoveUp))
            {
                //blob.transform.Translate(0, -modifiedSpeed * Time.deltaTime);
                force.Y = -movementForce;
                body.SetFacing(Facing.Up);
            }
            else if (Keyboard.GetState().IsKeyDown(KeyBindings.MoveDown))
            {
                //blob.transform.Translate(0, modifiedSpeed * Time.deltaTime);
                force.Y = movementForce;
               body.SetFacing(Facing.Down);
            }

            if (Keyboard.GetState().IsKeyDown(KeyBindings.MoveLeft))
            {
                //blob.transform.Translate(-modifiedSpeed * Time.deltaTime, 0);
                force.X = -movementForce;
                body.SetFacing(Facing.Left);
            }
            else if (Keyboard.GetState().IsKeyDown(KeyBindings.MoveRight))
            {
                //blob.transform.Translate(modifiedSpeed * Time.deltaTime, 0);
                force.X = movementForce;
                body.SetFacing(Facing.Right);
            }
            speed += force*Time.deltaTime;
            speed *= (float)Math.Pow(friction, Time.deltaTime);
            blob.transform.Translate(speed*Time.deltaTime*speedModifier);
        }

        public int LegCount()
        {
            return (body.GetBodyPart(LimbType.LeftLeg)!=null?1:0) + (body.GetBodyPart(LimbType.RightLeg)!=null?1:0);
        }
    }
}
