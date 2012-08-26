using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CyborgPunch.Game
{
    class Dude : Humanoid
    {

        public Dude()
            : base()
        {
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
            }
        }
    }
}
