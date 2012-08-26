using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;

namespace CyborgPunch.Game
{
    class DieOutOfBounds : Component
    {
        public DieOutOfBounds()
            : base()
        {
        }

        public override void Update()
        {
            base.Update();

            if (!GameManager.Instance.InVisualBounds(blob.transform.Position))
            {
                blob.Destroy();
                GameManager.Instance.SetSecondLabel("DEATH");
            }
        }
    }
}
