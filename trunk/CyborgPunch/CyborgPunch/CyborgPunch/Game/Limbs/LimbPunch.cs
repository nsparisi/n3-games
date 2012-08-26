using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace CyborgPunch.Game.Limbs
{
    abstract class LimbPunch : Component
    {
        protected float chargePower;
        protected float chargeSpeed;
        protected float chargeMax;
        protected bool thrown;
        protected bool keyWasDown;
        protected Dude body;
        protected Keys activationKey;
        protected LimbType limbType;

        public float restitutionSpeed = 4f;
        public Vector2 currentAnchor;
        public Vector2 offset;

        Vector2 chargeMove = new Vector2(0,-10);

        public override void Start()
        {
            base.Start();

            currentAnchor = blob.transform.LocalPosition;
        }

        public LimbPunch(Dude body, LimbType limbType, Keys activationKey) : base()
        {
            this.limbType = limbType;
            this.activationKey = activationKey;
            this.body = body;
        }

        public override void Update()
        {
            base.Update();

            if (thrown)
                ThrowUpdate();
            else
                PunchUpdate();
        }

        public void PunchUpdate()
        {
            KeyboardState keyState = Keyboard.GetState();
            bool keyIsDown = keyState.IsKeyDown(activationKey);

            if (!keyIsDown)
            {
                offset = Vector2.Lerp(offset, Vector2.Zero, Time.deltaTime*restitutionSpeed);
                blob.transform.LocalPosition = currentAnchor + offset;
            }

            if (keyState.IsKeyDown(KeyBindings.LimbChangeModifier) || keyState.IsKeyDown(KeyBindings.LimbChangeAlternate))
            {
                ThrowCharge(keyIsDown);
            }
            else
            {
                if (keyWasDown && keyIsDown)
                {
                    ContinuePunch();
                }
                else if (!keyWasDown && keyIsDown)
                {
                    StartCharge();
                    StartPunch();
                }
                else if (keyWasDown && !keyIsDown)
                {
                    ReleaseCharge();
                    EndPunch();
                    chargePower = 0;
                    GameManager.Instance.SetSecondLabel(chargePower.ToString("0.00"));
                }
            }
            keyWasDown = keyState.IsKeyDown(activationKey);
        }

        void ThrowCharge(bool keyIsDown)
        {
            if (keyWasDown && keyIsDown)
            {
                IncreaseCharge();
            }
            else if (!keyWasDown && keyIsDown)
            {
                StartCharge();
            }
            else if (keyWasDown && !keyIsDown)
            {
                ReleaseCharge();
                Throw();
            }
        }

        public void StartCharge()
        {
            offset = Vector2.Zero;
        }

        public void IncreaseCharge()
        {
            chargePower += chargeSpeed * Time.deltaTime;
            chargePower = MathHelper.Min(chargePower, chargeMax);
            GameManager.Instance.SetSecondLabel(chargePower.ToString("0.00"));

            Vector2 chargeTranslationVector = VectorFacing.RotateVectorToFacing(chargeMove, body.GetFacing());
            offset = chargeTranslationVector*(chargePower / chargeMax);
            blob.transform.LocalPosition = offset + currentAnchor;
        }

        public void ReleaseCharge()
        {
            offset *= -1;
            blob.transform.LocalPosition = currentAnchor + offset;
        }

        public abstract void ThrowUpdate();
        public abstract void StartPunch();
        public abstract void ContinuePunch();
        public abstract void EndPunch();

        public virtual void Throw()
        {
            Collider collider = new Collider();
            collider.bounds = ResourceManager.GetBounds(body.GetBodyPart(limbType).GetComponent<Sprite>().texture);
            collider.offset = new Vector2(collider.bounds.X, collider.bounds.Y);
            body.GetBodyPart(limbType).AddComponent(collider);
            body.GetBodyPart(limbType).AddComponent(new Damage(1));


            thrown = true;
            body.RemoveBodyPart(limbType);
            blob.transform.Parent = null;
        }
    }
}
