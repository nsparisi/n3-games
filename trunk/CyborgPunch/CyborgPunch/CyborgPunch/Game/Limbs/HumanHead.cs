using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using CyborgPunch.Core;

namespace CyborgPunch.Game.Limbs
{
    class HumanHead : LimbPunch
    {
        Vector2 velocity;
        float maxThrowTime;
        float throwTime;
        float sweetMin;
        float sweetMax;
        float sweetBonus;

        public HumanHead(Dude body, LimbType limbType)
            : base(body, limbType)
        {
            velocity = new Vector2(0, 420);

            maxThrowTime = .75f;
            throwTime = 0f;
            sweetMin = .1f;
            sweetMax = .4f;
            sweetBonus = 1f;
            chargePower = 0f;
            chargeSpeed = 5f;
            chargeMax = .5f;
            chargeMove = new Vector2(0, -5);
        }

        public override void Throw()
        {
            base.Throw();
            velocity = VectorFacing.RotateVectorToFacing(velocity, body.GetFacing());

            if (IsSweet())
            {
                GameManager.Instance.SetSecondLabel("SWEET SHOT");
            }

            SoundManager.PlaySound(SoundManager.SFX_RIP_HUMAN_LIMB);

            velocity *= chargePower + (IsSweet() ? sweetBonus : 0f);
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
            blob.transform.Translate(velocity * Time.deltaTime);
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
            if (chargePower >= sweetMin)
            {
                Blob b = new Blob();
                b.AddComponent(new HitFlash(DamageValues.humanMelee+(chargePower==chargeMax?1:0), 0f, body.GetFacing(), DamageValues.humanPiercing));
                Vector2 position = VectorFacing.RotateVectorToFacing(new Vector2(0, 50), body.GetFacing());
                b.transform.Position = body.GetColliderCenter() + position + new Vector2(0, -20);

                SoundManager.PlaySound(SoundManager.SFX_PUNCH);
            }
        }
    }
}
