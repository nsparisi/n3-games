using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework.Input;

namespace CyborgPunch.Game.Limbs
{
    abstract class LimbPunch : Component
    {
        float chargePower;
        float chargeSpeed;
        bool thrown;
        Dude body;
        Keys activationKey;
        LimbType limbType;

        bool keyWasDown;

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
                    StartPunch();
                }
                else if (keyWasDown && !keyIsDown)
                {
                    EndPunch();
                }
            }
            keyWasDown = keyState.IsKeyDown(activationKey);
        }

        void ThrowCharge(bool keyIsDown)
        {
            if (keyWasDown && keyIsDown)
            {
                chargePower += chargeSpeed;
            }
            else if (!keyWasDown && keyIsDown)
            {
                //???
            }
            else if (keyWasDown && !keyIsDown)
            {
                Throw();
            }
        }

        public abstract void ThrowUpdate();
        public abstract void StartPunch();
        public abstract void ContinuePunch();
        public abstract void EndPunch();

        public virtual void Throw()
        {
            thrown = true;
            body.RemoveBodyPart(limbType);
        }
    }
}
