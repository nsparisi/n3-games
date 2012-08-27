﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using CyborgPunch.Core;

namespace CyborgPunch.Game.Limbs
{
    class RocketLeg : LimbPunch
    {

        public RocketLeg(Dude body, LimbType limbType)
            : base(body, limbType)
        {
        }

        public override void Throw()
        {
            base.Throw();
        }

        public override void ThrowUpdate()
        {
        }

        public override void StartPunch()
        {
        }

        public override void ContinuePunch()
        {
        }

        public override void EndPunch()
        {
        }
    }
}