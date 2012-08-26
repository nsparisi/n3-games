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
