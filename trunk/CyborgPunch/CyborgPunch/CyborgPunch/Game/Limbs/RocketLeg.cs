using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using CyborgPunch.Core;

namespace CyborgPunch.Game.Limbs
{
    class RocketLeg : LimbPunch
    {
        float maxThrowTime;
        float throwTime;
        Vector2 velocity;
        float sweetMin;
        float sweetMax;
        float sweetBonus;
        Random random { get { return RandomCore.random; } }

        public RocketLeg(Dude body, LimbType limbType)
            : base(body, limbType)
        {
            velocity = new Vector2(0, 400);

            maxThrowTime = 10f;
            throwTime = 0f;
            chargePower = 0f;
            chargeSpeed = .4f;
            chargeMax = 1;
        }

        public override void Throw()
        {
            base.Throw();
            velocity = VectorFacing.RotateVectorToFacing(velocity, body.GetFacing());
            body.movement.velocity += -velocity * 5;
            SoundManager.PlaySound(SoundManager.SFX_ROCKET_BLAST);
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

            float acceleration = 200;
            double rotateSpeed = Math.PI * 10;
            float angleChange = (float)((random.NextDouble() - .5) * rotateSpeed);

            velocity += Vector2.Normalize(velocity)*acceleration*Time.deltaTime;
            velocity = VectorFacing.RotateVector(velocity, angleChange*Time.deltaTime);
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
            body.movement.velocity += -DamageValues.rocketKick * VectorFacing.RotateVectorToFacing(velocity, body.GetFacing());
            
        }
    }
}
