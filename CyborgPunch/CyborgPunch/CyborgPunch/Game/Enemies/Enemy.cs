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
        int health = 2;


        public Enemy()
            : base()
        {
        }

        public override void Start()
        {
            base.Start();
            EnemyManager.Instance.RegisterEnemy(this);

            dude = GameManager.Instance.dude;

            AddLimb(LimbType.Head, false);
            AddLimb(LimbType.LeftArm, false);
            AddLimb(LimbType.LeftLeg, false);
            AddLimb(LimbType.RightArm, false);
            AddLimb(LimbType.RightLeg, false);
            AddLimb(LimbType.Torso, false);
        }

        public void Die()
        {
            EnemyManager.Instance.UnregisterEnemy(this);


            SoundManager.PlaySound(SoundManager.SFX_ENEMY_DEATH);

            Blob head = RemoveBodyPart(LimbType.Head);
            head.transform.Parent = null;
            head.GetComponent<LimbVisual>().FlyInRandomDirection();

            Blob lLeg = RemoveBodyPart(LimbType.LeftArm);
            lLeg.transform.Parent = null;
            lLeg.GetComponent<LimbVisual>().FlyInRandomDirection();

            Blob rLeg = RemoveBodyPart(LimbType.LeftLeg);
            rLeg.transform.Parent = null;
            rLeg.GetComponent<LimbVisual>().FlyInRandomDirection();

            Blob rArm = RemoveBodyPart(LimbType.RightArm);
            rArm.transform.Parent = null;
            rArm.GetComponent<LimbVisual>().FlyInRandomDirection();

            Blob lArm = RemoveBodyPart(LimbType.RightLeg);
            lArm.transform.Parent = null;
            lArm.GetComponent<LimbVisual>().FlyInRandomDirection();

            blob.Destroy();

            ScoreManager.Instance.IncrementScore();
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
                Vector2 direction = collider.Center() - damage.blob.collider.Center();
                direction.Normalize();
                blob.transform.Translate(direction * damage.knockbackPower);
                
                SoundManager.PlaySound(SoundManager.SFX_HIT_2);
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
                GameManager.Instance.dude.GetComponent<Dude>().Hit();
                Die();
            }
        }

    }
}
