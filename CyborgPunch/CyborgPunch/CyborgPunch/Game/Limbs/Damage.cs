using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using CyborgPunch.Game.Enemies;

namespace CyborgPunch.Game.Limbs
{
    public class Damage : Component
    {
        public int damageValue;

        public Damage(int damage)
            : base()
        {
            damageValue = damage;
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();

            List<Enemy> enemies = EnemyManager.Instance.GetEnemies();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].blob.Collides(this.blob))
                {
                    //hit enemy
                    enemies[i].Hit(this);
                    Shake.ShakeIt(7, 1);
                    BlobManager.Instance.PauseForDuration(0.15f);
                }
            }
        }
    }
}
