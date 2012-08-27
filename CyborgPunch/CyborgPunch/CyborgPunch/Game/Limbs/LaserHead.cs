using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using CyborgPunch.Core;

namespace CyborgPunch.Game.Limbs
{
    class LaserHead : LimbPunch
    {
        Vector2 meleeAhead;
        Vector2 meleeSize;

        int ammo = 1;
        Facing facing;
        Vector2 velocity;
        float maxThrowTime;
        float throwTime;
        float sweetMin;
        float sweetMax;
        float sweetBonus;
        bool hasBeenUnpressed;

        Vector2 bulletFireTrajectory;

        public LaserHead(Dude myBody, LimbType whichLimb)
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
            chargeSpeed = 2f;
            chargeMax = 3;

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
                FadeAway();
            }
            blob.transform.Translate(velocity * Time.deltaTime);
            KeyboardState keyState = Keyboard.GetState();
            bool keyIsDown = keyState.IsKeyDown(activationKey);
            if (keyIsDown && ! keyWasDown)
            {
                FireGun(collider.Center());
            }

            keyWasDown = keyIsDown;
        }

        public override void Update()
        {
            base.Update();
            hasBeenUnpressed |= !keyWasDown;
        }

        public void FireGun(Vector2 position)
        {
            if (ammo > 0 && hasBeenUnpressed)
            {
                if (!thrown)
                {
                    facing = body.GetFacing();
                }

                chargePower = Math.Max(chargePower, 1);
                int laserWidth = (int)(30 * chargePower);
                Blob bulletBlob = new Blob();
                bulletBlob.transform.Position = position;

                Sprite laserSprite = new Sprite(ResourceManager.laserBeam);
                laserSprite.SetAnchor(Sprite.AnchorType.Upper_Center);
                laserSprite.rotation = VectorFacing.FacingToPi(facing);
                laserSprite.SetSize(laserWidth*2, 1000);
                bulletBlob.AddComponent(laserSprite);
                Collider collider = new Collider();
                bulletBlob.AddComponent(collider);

                laserWidth /= 2;
                Rectangle colliderRectangle = new Rectangle();
                switch (facing)
                {
                    case Facing.Down:
                        colliderRectangle = new Rectangle(0,0,laserWidth,1000);
                        collider.offset = new Vector2(-laserWidth/20, 0);
                        break;
                    case Facing.Left:
                        colliderRectangle = new Rectangle(0,0,1000,laserWidth);
                        collider.offset = new Vector2(-1000, -laserWidth/2);
                        break;
                    case Facing.Right:
                        colliderRectangle = new Rectangle(0,0,1000,laserWidth);
                        collider.offset = new Vector2(0, -laserWidth/2);
                        break;
                    case Facing.Up:
                        colliderRectangle = new Rectangle(0,0,laserWidth,1000);
                        collider.offset = new Vector2(-laserWidth/2, -1000);
                        break;
                }

                collider.bounds = colliderRectangle;


                float dieTime = .3f * chargePower;
                bulletBlob.AddComponent(new DieInSeconds(dieTime));
                bulletBlob.AddComponent(new FadeInSeconds(dieTime));

                bulletBlob.AddComponent(new Damage(1));
                bulletBlob.AddComponent(new DieOutOfBounds());

                //bulletBlob.AddComponent(new Bullet(VectorFacing.RotateVectorToFacing(bulletFireTrajectory, facing)));
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
            Vector2 totalCenter = body.GetColliderCenter();
            totalCenter.Y = body.blob.transform.Position.Y +25;
            FireGun(totalCenter);
        }
    }
}
