using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CyborgPunch.Game.Limbs;

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

            GetBodyPart(LimbType.LeftArm).AddComponent(new HumanArm(this, LimbType.LeftArm, KeyBindings.ArmLeftAction));
            GetBodyPart(LimbType.RightArm).AddComponent(new HumanArm(this, LimbType.RightArm, KeyBindings.ArmRightAction));
        }

        public override void Update()
        {
            base.Update();
        }
    }
}