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

        public float speed = 140;

        public DudeMovement()
            : base()
        {
        }

        public override void Update()
        {
            base.Update();

            if (Keyboard.GetState().IsKeyDown(KeyBindings.MoveUp))
            {
                blob.transform.Translate(0, -speed * Time.deltaTime);
            }

            if (Keyboard.GetState().IsKeyDown(KeyBindings.MoveDown))
            {
                blob.transform.Translate(0, speed * Time.deltaTime);
            }

            if (Keyboard.GetState().IsKeyDown(KeyBindings.MoveLeft))
            {
                blob.transform.Translate(-speed * Time.deltaTime, 0);
            }

            if (Keyboard.GetState().IsKeyDown(KeyBindings.MoveRight))
            {
                blob.transform.Translate(speed * Time.deltaTime, 0);
            }
        }
    }
}
