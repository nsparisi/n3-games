using System;
using System.Collections.Generic;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;
using CyborgPunch.Game.Limbs;

namespace CyborgPunch.Game.Enemies
{
    public class Enemy : Humanoid
    {
        Blob dude;
        public float speed = 70;
        int health = 1;


        public Enemy()
            : base()
        {
        }

        public override void Start()
        {
            base.Start();
            EnemyManager.Instance.RegisterEnemy(this);

            dude = GameManager.Instance.dude;

            /*
            GetBodyPart(LimbType.Head).GetComponent<Sprite>().texture =
                ResourceManager.GetSpritePart(LimbType.Head, PartType.Robot);

            GetBodyPart(LimbType.Torso).GetComponent<Sprite>().texture =
                ResourceManager.GetSpritePart(LimbType.Torso, PartType.Robot);

            GetBodyPart(LimbType.LeftArm).GetComponent<Sprite>().texture =
                ResourceManager.GetSpritePart(LimbType.LeftArm, PartType.Robot);

            GetBodyPart(LimbType.LeftLeg).GetComponent<Sprite>().texture =
                ResourceManager.GetSpritePart(LimbType.LeftLeg, PartType.Robot);

            GetBodyPart(LimbType.RightArm).GetComponent<Sprite>().texture =
                ResourceManager.GetSpritePart(LimbType.RightArm, PartType.Robot);

            GetBodyPart(LimbType.RightLeg).GetComponent<Sprite>().texture =
                ResourceManager.GetSpritePart(LimbType.RightLeg, PartType.Robot);
             */

        }

        public void Die()
        {
            EnemyManager.Instance.UnregisterEnemy(this);
            blob.Destroy();
        }

        public void Hit(Damage damage)
        {
            health -= damage.damageValue;

            if (health <= 0)
            {
                //die
                Die();
            }
            else
            {
                //knockback
                Vector2 direction = blob.transform.Position - damage.blob.transform.Position;
                direction.Normalize();
                blob.transform.Translate(direction * 50);
            }
        }

        public override void Update()
        {
            base.Update();

            Vector2 direction = dude.transform.Position - blob.transform.Position;
            direction.Normalize();
            blob.transform.Translate(direction * speed * Time.deltaTime);

            if (Math.Abs(direction.X) > Math.Abs(direction.Y))
            {
                if (direction.X <= 0)
                {
                    SetFacing(Facing.Left);
                }
                else
                {
                    SetFacing(Facing.Right);
                }
            }
            else
            {
                if (direction.Y <= 0)
                {
                    SetFacing(Facing.Up);
                }
                else
                {
                    SetFacing(Facing.Down);
                }
            }

            if (blob.Collides(GameManager.Instance.dude))
            {
                //kill dude
                GameManager.Instance.dude.transform.Translate(direction * 50);
                Die();
            }
        }

    }
}
