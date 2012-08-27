using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using CyborgPunch.Core;

namespace CyborgPunch.Game.Limbs
{
    class HammerArm : LimbPunch
    {
        int ammo = DamageValues.robotMeleeAmmo;
        float maxThrowTime;
        float throwTime;
        Vector2 velocity;
        float sweetMin;
        float sweetMax;
        float sweetBonus;

        public HammerArm(Dude body, LimbType limbType)
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
            if (chargePower > 0 && ammo-- >= 0)
            {
                Blob b = new Blob();
                b.AddComponent(new HitFlash(DamageValues.robotMelee, 20f,body.GetFacing(), DamageValues.robotPiercing, body.Collider));
                b.transform.Parent = this.blob.transform;

                Vector2 position = VectorFacing.RotateVectorToFacing(new Vector2(0, 50), body.GetFacing());
                b.transform.LocalPosition = position + new Vector2(25, 0);

                SoundManager.PlaySound(SoundManager.SFX_PUNCH);
            }
        }
    }
}
