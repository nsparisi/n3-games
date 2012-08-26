using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;

namespace CyborgPunch.Game
{
    class Bullet : Component
    {
        Vector2 trajectory;

        public Bullet(Vector2 trajectory) : base()
        {
            this.trajectory = trajectory;
        }

        public override void Update()
        {
            base.Update();
            blob.transform.Position += trajectory * Time.deltaTime;
        }
    }
}
