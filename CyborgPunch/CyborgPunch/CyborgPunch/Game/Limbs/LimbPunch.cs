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
        protected Collider collider;
        protected float fadeTime;

        protected float chargePower = 1;
        protected float chargeSpeed = 1;
        protected float chargeMax = 1;
        protected bool thrown;
        protected bool keyWasDown;
        protected Dude body;
        protected Keys activationKey;
        protected LimbType limbType;
        protected Sprite limbSprite;

        protected bool firstFade = true;

        public float restitutionSpeed = 60f;
        public Vector2 currentAnchor;
        public Vector2 offset = Vector2.Zero;

        protected Vector2 chargeMove = new Vector2(0,-10);

        public override void Start()
        {
            base.Start();
            fadeTime = .2f;
            limbSprite = blob.GetComponent<Sprite>();
            currentAnchor = blob.transform.LocalPosition;
        }

        public LimbPunch(Dude body, LimbType limbType) : base()
        {
            this.limbType = limbType;
            this.activationKey = KeyBindings.KeyBindingFromLimbType(limbType);
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
                offset = VectorHelpers.MoveTowards(offset, Vector2.Zero, Time.deltaTime*restitutionSpeed);
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
            collider = new Collider();
            collider.bounds = ResourceManager.GetBounds(body.GetBodyPart(limbType).GetComponent<Sprite>().texture);
            collider.offset = new Vector2(collider.bounds.X, collider.bounds.Y);
            body.GetBodyPart(limbType).AddComponent(collider);
            body.GetBodyPart(limbType).AddComponent(new Damage(DamageValues.throwDamage));
            this.blob.AddComponent(new DieOutOfBounds());

            thrown = true;
            body.RemoveBodyPart(limbType);
            blob.transform.Parent = null;

            if ( !(this is HumanArm || this is HumanHead))
            {
                SoundManager.PlaySound(SoundManager.SFX_THROW_LIMB);
            }
        }

        public void FadeAway()
        {
            if (firstFade)
            {
                collider.enabled = false;
                limbSprite.color = Color.Lerp(limbSprite.color, Color.Black, .8f);
                firstFade = false;
            }
            Color newLimbColor = limbSprite.color;

            float newAlpha = (newLimbColor.A - (Time.deltaTime * (1f / fadeTime) * byte.MaxValue));

            if (newAlpha < 0)
            {
                newAlpha = 0;
                blob.Destroy();
            }

            newLimbColor.A = (byte)newAlpha;
            limbSprite.color = newLimbColor;
        }
    }
}
