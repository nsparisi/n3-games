using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;

namespace CyborgPunch.Game
{
    class DieInSeconds : Component
    {
        float lifeSeconds;
        public DieInSeconds(float seconds) : base()
        {
            lifeSeconds = seconds;
        }

        public override void Update()
        {
            base.Update();

            lifeSeconds -= Time.deltaTime;
            if (lifeSeconds <= 0f)
            {
                blob.Destroy();
            }
        }
    }
}
