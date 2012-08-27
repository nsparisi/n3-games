using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CyborgPunch.Core;
using CyborgPunch.Game.Enemies;

namespace CyborgPunch.Game.Limbs
{
    class BombHead : LimbPunch
    {
        float baseExplosionSize = 200f;
        Vector2 velocity;
        float maxThrowTime;
        float throwTime;
        float sweetMin;
        float sweetMax;
        float sweetBonus;
        float storedCharge;

        bool hasBeenUnpressed;
        public BombHead(Dude body, LimbType limbType)
            : base(body, limbType)
        {
            hasBeenUnpressed = false;
            velocity = new Vector2(0, 420);

            maxThrowTime = .75f;
            throwTime = 0f;
            sweetMin = .7f;
            sweetMax = .9f;
            sweetBonus = 2f;
            chargePower = 0f;
            chargeSpeed = 10f;
            chargeMax = 5f;
        }

        public override void Throw()
        {
            base.Throw();
            blob.RemoveComponent<Damage>();
            storedCharge = chargePower+(IsSweet()?sweetBonus:0);
        }

        public override void ThrowUpdate()
        {
            KeyboardState keyState = Keyboard.GetState();

            hasBeenUnpressed |= !keyState.IsKeyDown(activationKey);

            throwTime += Time.deltaTime;
            if (throwTime > maxThrowTime)
            {
                velocity *= .75f;
                FadeAway();
            }
            blob.transform.Translate(velocity * Time.deltaTime);
            List<Enemy> enemies = EnemyManager.Instance.GetEnemies();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].blob.Collides(this.blob))
                {
                    Explode(collider.Center());
                    Shake.ShakeIt(10, 20);
                    BlobManager.Instance.PauseForDuration(0.10f);
                    break;
                }
            }
        }

        public override void Update()
        {
            base.Update();
            hasBeenUnpressed |= !keyWasDown;
        }

        public override void StartPunch()
        {
        }

        public bool IsSweet()
        {
            return chargePower > sweetMin && chargePower < sweetMax;
        }

        public override void ContinuePunch()
        {
            IncreaseCharge();
        }

        public override void EndPunch()
        {
            if (hasBeenUnpressed)
            {
                Explode(blob.transform.Position);
                Shake.ShakeIt(10, 20);
                BlobManager.Instance.PauseForDuration(0.10f);
            }
        }

        public void Explode(Vector2 atPosition)
        {
            Blob explosion = new Blob();
            Sprite explosionSprite = new Sprite(ResourceManager.explosion);
            Damage bombDamage = new Damage(2);
            Collider newCollider = new Collider();
            
            float chargedExplosionSize = baseExplosionSize * chargePower;
            newCollider.bounds = new Rectangle((int)-chargedExplosionSize / 2, (int)-chargedExplosionSize / 2,
                (int)chargedExplosionSize / 2, (int)chargedExplosionSize / 2);

            //bombDamage.shakeStrength = 0f;
            //bombDamage.stickLength = .1;
            explosionSprite.SetSize(newCollider.bounds.Width, newCollider.bounds.Height);
            explosion.AddComponent(explosionSprite);
            explosion.AddComponent(bombDamage);
            explosion.AddComponent(newCollider);
            explosion.AddComponent(new DieInSeconds(.5f));
            explosion.AddComponent(new FadeInSeconds(.7f));
            explosion.transform.Position = atPosition -(explosionSprite.GetSize() / 2) + (limbSprite.GetSize()/2);

            if (!thrown)
                body.RemoveBodyPart(limbType);
            blob.Destroy();
        }
    }
}
