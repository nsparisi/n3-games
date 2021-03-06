﻿using System;
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

        float friction = .00000001f;

        public Vector2 velocity;

        public DudeMovement(Humanoid body)
            : base()
        {
            this.body = body;
        }

        public override void Update()
        {
            base.Update();

            float movementForce = 3000;
            Vector2 acceleration = Vector2.Zero;
            float speedModifier = (LegCount() * .25f) + .5f;
            if (Keyboard.GetState().IsKeyDown(KeyBindings.MoveUp))
            {
                //blob.transform.Translate(0, -modifiedSpeed * Time.deltaTime);
                acceleration.Y = -movementForce;
                body.SetFacing(Facing.Up);
            }
            else if (Keyboard.GetState().IsKeyDown(KeyBindings.MoveDown))
            {
                //blob.transform.Translate(0, modifiedSpeed * Time.deltaTime);
                acceleration.Y = movementForce;
               body.SetFacing(Facing.Down);
            }

            if (Keyboard.GetState().IsKeyDown(KeyBindings.MoveLeft))
            {
                //blob.transform.Translate(-modifiedSpeed * Time.deltaTime, 0);
                acceleration.X = -movementForce;
                body.SetFacing(Facing.Left);
            }
            else if (Keyboard.GetState().IsKeyDown(KeyBindings.MoveRight))
            {
                //blob.transform.Translate(modifiedSpeed * Time.deltaTime, 0);
                acceleration.X = movementForce;
                body.SetFacing(Facing.Right);
            }
            velocity += acceleration*Time.deltaTime;
            blob.transform.Translate(velocity*Time.deltaTime*speedModifier);
            if (!GameManager.Instance.InVisualBounds(blob.collider.bounds))
            {
                blob.transform.Translate(-velocity*2 * Time.deltaTime * speedModifier);
                velocity *= -2f;
            }

            velocity *= (float)Math.Pow(friction, Time.deltaTime);
        }

        public int LegCount()
        {
            return (body.GetBodyPart(LimbType.LeftLeg)!=null?1:0) + (body.GetBodyPart(LimbType.RightLeg)!=null?1:0);
        }
    }
}
