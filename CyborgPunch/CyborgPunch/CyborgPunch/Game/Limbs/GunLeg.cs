using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using CyborgPunch.Core;

namespace CyborgPunch.Game.Limbs
{
    class GunLeg : LimbPunch
    {
        int ammo = 40;
        Facing facing;
        Vector2 velocity;
        float maxThrowTime;
        float throwTime;
        float sweetMin;
        float sweetMax;
        float sweetBonus;
        bool hasBeenUnpressed;

        Vector2 bulletFireTrajectory;

        public GunLeg(Dude myBody, LimbType whichLimb)
            : base(myBody, whichLimb)
        {
            hasBeenUnpressed = false;
            velocity = new Vector2(0, 420);
            throwTime = 0f;
            maxThrowTime = .75f;
            sweetMin = .7f;
            sweetMax = .9f;
            sweetBonus = 1f;
            chargePower = 0f;
            chargeSpeed = 1f;
            chargeMax = 1;

            //meleeAhead = 10f;

            bulletFireTrajectory = new Vector2(0, 500);
        }

        public override void Throw()
        {
            base.Throw();

            Facing fireFacing = body.GetFacing();
            if (limbType == LimbType.LeftArm || limbType == LimbType.LeftLeg)
            {
                fireFacing = VectorFacing.FacingTurnFacing(body.GetFacing(), Facing.Left);
            }
            else if (limbType == LimbType.RightArm || limbType == LimbType.RightLeg)
            {
                fireFacing = VectorFacing.FacingTurnFacing(body.GetFacing(), Facing.Right);
            }

            velocity = VectorFacing.RotateVectorToFacing(velocity, body.GetFacing());
            facing = fireFacing;
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
                if (ammo == 0)
                    FadeAway();
            }
            blob.transform.Translate(velocity * Time.deltaTime);
            KeyboardState keyState = Keyboard.GetState();
            bool keyIsDown = keyState.IsKeyDown(activationKey);
            if (keyIsDown && ! keyWasDown)
            {
                FireGun();
            }

            keyWasDown = keyIsDown;
        }

        public override void Update()
        {
            base.Update();
            hasBeenUnpressed |= !keyWasDown;
        }

        public void FireGun()
        {
            if (ammo > 0 && hasBeenUnpressed)
            {
                Blob bulletBlob = new Blob();
                bulletBlob.transform.Position = blob.transform.Position;
                Sprite headSprite = new Sprite(ResourceManager.bullet);
                bulletBlob.AddComponent(headSprite);
                Collider collider = new Collider();
                collider.bounds = ResourceManager.GetBounds(headSprite.texture);
                collider.offset = new Vector2(collider.bounds.X, collider.bounds.Y);
                bulletBlob.AddComponent(collider);
                bulletBlob.AddComponent(new Damage(DamageValues.throwDamage, DamageValues.gunLegPiercing));
                bulletBlob.AddComponent(new DieOutOfBounds());


                SoundManager.PlaySound(SoundManager.SFX_GUN_LEG_SHOT);

                if (!thrown)
                {
                    facing = body.GetFacing();
                }
                bulletBlob.AddComponent(new Bullet(VectorFacing.RotateVectorToFacing(bulletFireTrajectory, facing)));
                ammo--;
            }
            else if (ammo <= 0 && hasBeenUnpressed)
            {
                SoundManager.PlaySound(SoundManager.SFX_EMPTY_GUN);
            }
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
            FireGun();
        }
    }
}
