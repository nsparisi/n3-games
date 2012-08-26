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
        int ammo = 5;
        Facing facing;
        Vector2 velocity;
        float sweetMin;
        float sweetMax;
        float sweetBonus;

        Vector2 bulletFireTrajectory;

        public GunLeg(Dude myBody, LimbType whichLimb, Keys activationKey)
            : base(myBody, whichLimb, activationKey)
        {
            velocity = new Vector2(0, 420);
            sweetMin = .7f;
            sweetMax = .9f;
            sweetBonus = 1f;
            chargePower = 0f;
            chargeSpeed = 2f;
            chargeMax = 1;

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
            blob.transform.Translate(velocity * Time.deltaTime);
            KeyboardState keyState = Keyboard.GetState();
            bool keyIsDown = keyState.IsKeyDown(activationKey);
            if (keyIsDown && ! keyWasDown)
            {
                FireGun();
            }

            keyWasDown = keyIsDown;
        }

        int num = 0;
        public void FireGun()
        {
            if (ammo > 0)
            {
                Blob bulletBlob = new Blob();
                bulletBlob.transform.Position = blob.transform.Position;
                Sprite headSprite = new Sprite(ResourceManager.TEXTURE_HUMAN_DOWN_HEAD);
                bulletBlob.AddComponent(headSprite);
                Collider collider = new Collider();
                collider.bounds = ResourceManager.GetBounds(headSprite.texture);
                collider.offset = new Vector2(collider.bounds.X, collider.bounds.Y);
                bulletBlob.AddComponent(collider);
                bulletBlob.AddComponent(new Damage(1));
                bulletBlob.AddComponent(new DieOutOfBounds());

                if (!thrown)
                {
                    facing = body.GetFacing();
                }
                bulletBlob.AddComponent(new Bullet(VectorFacing.RotateVectorToFacing(bulletFireTrajectory, facing)));
                ammo--;
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
