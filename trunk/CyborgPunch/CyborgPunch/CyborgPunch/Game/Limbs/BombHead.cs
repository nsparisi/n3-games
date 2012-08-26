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
        float baseExplosionSize = 20f;
        Vector2 velocity;
        float maxThrowTime;
        float throwTime;
        float sweetMin;
        float sweetMax;
        float sweetBonus;
        float storedCharge;

        public BombHead(Dude body, LimbType limbType)
            : base(body, limbType)
        {
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

            storedCharge = chargePower;
        }

        public override void ThrowUpdate()
        {
            List<Enemy> enemies = EnemyManager.Instance.GetEnemies();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].blob.Collides(this.blob))
                {
                    Explode();
                    Shake.ShakeIt(10, 20);
                    BlobManager.Instance.PauseForDuration(0.10f);
                }
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
            Explode();
        }

        public void Explode()
        {
            Blob explosion = new Blob();
            Sprite explosionSprite = new Sprite(ResourceManager.texture_White);
            Damage bombDamage = new Damage(2);
            Collider collider = new Collider();

            float chargedExplosionSize = baseExplosionSize * chargePower;
            collider.bounds = new Rectangle((int)-chargedExplosionSize / 2, (int)-chargedExplosionSize / 2,
                (int)chargedExplosionSize / 2, (int)chargedExplosionSize / 2);

            //bombDamage.shakeStrength = 0f;
            //bombDamage.stickLength = .1;
            explosion.AddComponent(explosionSprite);
            explosion.AddComponent(bombDamage);
            explosion.AddComponent(collider);
        }
    }
}
