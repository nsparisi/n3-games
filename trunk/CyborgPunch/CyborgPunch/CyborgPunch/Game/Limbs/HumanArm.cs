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
        float maxThrowTime;
        float throwTime;
        Vector2 velocity;
        float sweetMin;
        float sweetMax;
        float sweetBonus;

        public HumanArm(Dude body, LimbType limbType)
            : base(body, limbType)
        {
            velocity = new Vector2(0, 420);

            maxThrowTime = .75f;
            throwTime = 0f;
            sweetMin = .7f;
            sweetMax = .9f;
            sweetBonus = 2f;
            chargePower = 0f;
            chargeSpeed = 2f;
            chargeMax = 1;
        }

        public override void Throw()
        {
            base.Throw();
            velocity = VectorFacing.RotateVectorToFacing(velocity, body.GetFacing());

            if (IsSweet())
            {
                GameManager.Instance.SetSecondLabel("SWEET SHOT");
            }
            velocity *= chargePower + (IsSweet()?sweetBonus:0f);
        }

        public bool IsSweet()
        {
            return chargePower > sweetMin && chargePower < sweetMax;
        }

        public override void ThrowUpdate()
        {
            throwTime += Time.deltaTime;
            if (throwTime > maxThrowTime)
            {
                velocity *= .75f;
                FadeAway();
            }
            blob.transform.Translate(velocity*Time.deltaTime);
        }

        public override void StartPunch()
        {
        }

        public override void ContinuePunch()
        {
            IncreaseCharge();
        }

        public override void EndPunch()
        {
        }
    }
}
