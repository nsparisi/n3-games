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
            sweetMin = .8f;
            sweetMax = .9f;
            sweetBonus = 2f;
            chargePower = 0f;
            chargeSpeed = 5f;
            chargeMax = 1f;
        }

        public override void Throw()
        {
            base.Throw();
            velocity = VectorFacing.RotateVectorToFacing(velocity, body.GetFacing());

            if (IsSweet())
            {
                GameManager.Instance.SetSecondLabel("SWEET SHOT");
            }

            //ouch
            SoundManager.PlaySound(SoundManager.SFX_RIP_HUMAN_LIMB);


            Blob blood = new Blob();
            blood.AddComponent(new Sprite(ResourceManager.Blood));
            blood.transform.Position = this.blob.transform.Position;
            blood.GetComponent<Sprite>().SetAnchor(Sprite.AnchorType.Upper_Left);
            blood.GetComponent<Sprite>().z = 0.99f;
            blood.GetComponent<Sprite>().scale = 0.5f;

            //velocity *= chargePower + (IsSweet()?sweetBonus:0f);
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
            if (chargePower >= .7)
            {
                //make an attack
                int damage = IsSweet() ? 2 : 1;
                Blob b = new Blob();
                b.AddComponent(new HitFlash(DamageValues.humanArm*damage, 50f, body.GetFacing(), DamageValues.humanPiercing, body.Collider));
                b.transform.Parent = this.blob.transform;

                Vector2 position = VectorFacing.RotateVectorToFacing(new Vector2(0, 50), body.GetFacing());
                b.transform.LocalPosition = position + new Vector2(25,0);

                SoundManager.PlaySound(SoundManager.SFX_PUNCH);
                chargeSpeed -= .5f;
            }
        }
    }
}
