using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SumoPongXNA
{
    public class Paddle : Sprite
    {
        public float movementSpeed = Constants.PADDLE_SPEED;

        bool isFiring;
        bool isMovingUp;
        bool isMovingDown;
        bool canFire;

        public enum PaddleType {Left, Right}
        private PaddleType type;

        private List<Bullet> bullets;

        public Paddle(PaddleType type)
            : base(ContentLoader.Texure_Paddle)
        {
            this.type = type;
            canFire = true;
            SetSize(Constants.PADDLE_WIDTH, Constants.PADDLE_HEIGHT);
            bullets = new List<Bullet>();
        }

        public void Fire()
        {
            isFiring = true;
        }

        public void MoveUp()
        {
            isMovingUp = true;
        }

        public void MoveDown()
        {
            isMovingDown = true;
        }

        public void Hit(int direction)
        {
            float deltaX = direction * Constants.KNOCKBACK_DISTANCE;
            float currentX = (type == PaddleType.Left) ? bounds.Right : bounds.Left;

            if (MathHelper.Passed(currentX, currentX + deltaX, GameManager.Instance.arena.Center.X))
            {
                return;
            }

            this.transform.Translate(deltaX, 0);

        }

        public void CleanUp()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                Destroy(bullets[i]);
            }

            Destroy(this);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 newPosition = this.transform.position;

            //move up
            if (isMovingUp && !isMovingDown)
            {
                newPosition.Y -= Time.deltaTime * movementSpeed;
            }

            //move down
            else if(!isMovingUp && isMovingDown)
            {
                newPosition.Y += Time.deltaTime * movementSpeed;
            }

            if (isFiring && canFire)
            {
                if (this.type == PaddleType.Right)
                    FireLeft();
                else
                    FireRight();
            }


            newPosition.Y = MathHelper.Clamp(GameManager.Instance.arena.Top, GameManager.Instance.arena.Bottom - height, newPosition.Y);

            this.transform.position = newPosition;

            Reset();
        }


        private void Reset()
        {
            canFire = !isFiring;

            isMovingUp = false;
            isMovingDown = false;
            isFiring = false;
        }

        private void FireLeft()
        {
            Bullet b = new Bullet(-1);
            b.transform.position = this.transform.position;
            b.transform.position.X -= b.width;
            b.transform.position.Y += height / 2 - b.height / 2;
            bullets.Add(b);
        }

        private void FireRight()
        {
            Bullet b = new Bullet(1);
            b.transform.position = this.transform.position;
            b.transform.position.X += width;
            b.transform.position.Y += height / 2 - b.height / 2;
            bullets.Add(b);
        }
    }
}
