using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using CyborgPunch.Game.Limbs;

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
                LimbPickup limbPickup = blob.GetComponent<LimbPickup>();
                if (limbPickup != null)
                    limbPickup.Remove();
                blob.transform.Parent = null;
                blob.Destroy();
            }
        }
    }
}
