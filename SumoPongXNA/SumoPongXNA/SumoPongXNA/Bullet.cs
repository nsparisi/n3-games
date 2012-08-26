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
    public class Bullet : Sprite
    {
        public float movementSpeed = Constants.BULLET_SPEED;

        delegate void UpdateFunction(GameTime gameTime);
        UpdateFunction currentUpdate;

        private int direction;
        public int Direction {
            get
            {
                return direction;
            }
            private set
            {
                direction = value < 0 ? -1 : 1;
            }
        }

        public Bullet(int direction) : base(ContentLoader.Texure_White)
        {
            SetSize(Constants.BULLET_SIZE, Constants.BULLET_SIZE);
            this.color = Color.Black;
            this.Direction = direction;
            currentUpdate = ShootingAway;
        }
        
        public override void Update(GameTime gameTime)
        {
            currentUpdate(gameTime);
        }

        void ShootingAway(GameTime gameTime)
        {
            //basic movemnet
            Vector2 deltaPosition = Vector2.Zero;
            deltaPosition.X = Direction * movementSpeed * Time.deltaTime;

            //collision checking
            if (GameManager.Instance.left.Collides(this))
            {
                GameManager.Instance.left.Hit(direction);
                Destroy(this);
            }
            else if (GameManager.Instance.right.Collides(this))
            {
                GameManager.Instance.right.Hit(direction);
                Destroy(this);
            }

            //did bounds checking?
            Rectangle edge = GameManager.Instance.edge;
            float barrierX = Direction < 0 ? edge.X : edge.Right - width;
            float expectedX = transform.position.X + deltaPosition.X;
            if (MathHelper.Passed(transform.position.X, expectedX, barrierX) )
            {
                float toEdge = barrierX - transform.position.X;
                deltaPosition.X = toEdge * 2 - deltaPosition.X;
                Direction *= -1;
            }

            this.transform.Translate(deltaPosition);
        }

        void ComingBack(GameTime gameTime)
        {
        }
    }
}
