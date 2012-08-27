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
        public float speed = DamageValues.enemySpeed;
        int health = DamageValues.enemyHealth;

        Vector3 start, target;
        Vector3 initialLaunch;
        Vector3 position;

        float maxScoreMultiplier = 150;

        static Random rand { get { return RandomCore.random; } }
        float gravity = 300;
        float timer;

        public Enemy(Vector2 start, Vector2 target)
            : base()
        {
            float variation = (float)(RandomCore.random.NextDouble() - 0.5) * 0.5f;
            speed += speed * variation;

            float multiplier = MathHelper.Lerp(1, 2, (ScoreManager.Instance.Score + EnemyManager.Instance.GetEnemies().Count) / maxScoreMultiplier);
            multiplier = MathHelper.Clamp(multiplier, 1, 2);
            speed *= multiplier;

            this.start = new Vector3(start.X, start.Y, 0);
            this.target = new Vector3(target.X, target.Y, 0);

            float z = (float)(rand.NextDouble() * gravity * 1.5f);
            z = MathHelper.Clamp(z, gravity * 0.5f, gravity);
            float duration = 2 * z / gravity;
            timer = duration;

            Vector2 direction = target - start;
            direction.Normalize();
            float dist = (target - start).Length();
            direction *= dist / duration;

            position = this.start;
            initialLaunch = new Vector3(direction.X, direction.Y, -z);
            comingIn = true;

            typeA = RandomCore.random.Next(0, 2) == 1 ? false : true;
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
            if (head != null)
            {
                head.transform.Parent = null;
                head.GetComponent<LimbVisual>().FlyInRandomDirection();
            }

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

        public void Hit(Damage damage, Collider relativeCollider)
        {
            health -= damage.damageValue;

            if (damage.explosive)
            {
                Blob head = GetBodyPart(LimbType.Head);
                if (head != null)
                {
                    if (head.GetComponent<Limb>().head == Limb.HeadSubType.Bomb)
                    {
                        BombHead bombHead = (BombHead)head.AddComponent(new BombHead(null, LimbType.Head));
                        bombHead.MaxCharge();
                        bombHead.Explode(collider.Center());
                        RemoveBodyPart(LimbType.Head);
                    }
                }
            }

            if (health <= 0)
            {
                //die
                Die();
            }
            else
            {
                //knockback
                if (relativeCollider == null)
                {
                    relativeCollider = damage.blob.collider;
                }

                Vector2 direction = collider.Center() - relativeCollider.Center();
                direction.Normalize();
                blob.transform.Translate(direction * damage.knockbackPower);
                SoundManager.PlaySound(SoundManager.SFX_HIT_2);
            }
        }

        bool comingIn;
        bool isWait;
        float waitTimer;
        Vector2 destination;
        bool typeA = false;
        public override void Update()
        {
            base.Update();

            //spawning
            if (comingIn)
            {
                timer -= Time.deltaTime;
                if (timer > 0)
                {
                    this.initialLaunch.Z += gravity * Time.deltaTime;
                    this.position += initialLaunch * Time.deltaTime;
                    this.blob.transform.Position = new Vector2(this.position.X, this.position.Y + this.position.Z);
                }
                else if (timer > -0.6f)
                {
                }
                else
                {
                    comingIn = false;
                    destination = dude.transform.Position;
                }
            }

            //attacking
            else
            {
                if (!dude.GetComponent<Dude>().dying)
                {

                    if (isWait)
                    {
                        waitTimer -= Time.deltaTime;
                        if (waitTimer < 0)
                        {
                            isWait = false;
                            destination = dude.transform.Position;
                        }
                    }
                    else if (!isWait)
                    {
                        if (typeA)
                        {
                            destination = dude.transform.Position;
                        }

                        Vector2 direction = destination - blob.transform.Position;
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

                        if ((destination - blob.transform.Position).LengthSquared() < 5)
                        {
                            isWait = true;
                            float waitAverage = DamageValues.enemyWaitAverage;
                            waitTimer = (float)(RandomCore.random.NextDouble() + 0.5) * waitAverage;
                        }
                    }

                    if (blob.Collides(GameManager.Instance.dude))
                    {
                        //kill dude
                        GameManager.Instance.dude.GetComponent<Dude>().Hit();
                    }
                }
            }
        }

    }
}
