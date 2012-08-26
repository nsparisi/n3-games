using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using CyborgPunch.Core;

namespace CyborgPunch.Game.Limbs
{
    class HumanArm : LimbPunch
    {
        Vector2 velocity;

        public HumanArm(Dude body, LimbType limbType, Keys activationKey)
            : base(body, limbType, activationKey)
        {
            velocity = new Vector2(0, 280);
        }

        public override void Throw()
        {
            base.Throw();
            velocity = VectorFacing.RotateVectorToFacing(velocity, body.GetFacing());
        }

        public override void ThrowUpdate()
        {
            blob.transform.Translate(velocity*Time.deltaTime);
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
