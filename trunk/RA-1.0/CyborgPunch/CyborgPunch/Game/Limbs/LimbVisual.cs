using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyborgPunch.Game.Limbs
{
    class LimbVisual : Sprite
    {
        private Texture2D[] facings;
        private float[] depths;
        private Rectangle[] boundAreas;
        private Facing currentFacing;


        public LimbVisual(PartTypes part)
            : base()
        {
            facings = new Texture2D[4];
            boundAreas = new Rectangle[4];
            depths = new float[4];
            SetPartType(part);
            currentUpdate = Update_Nothing;
        }

        public override void Start()
        {
            base.Start();
            SetFacing(Facing.Down);
        }

        public void SetDepths(float[] depths)
        {
            this.depths = depths;
            RefreshDepth();
        }

        void RefreshDepth()
        {
            float iteration = 1f / (1024f * 1024f);
            float depth = iteration * this.depths[(int)currentFacing];
            this.z = depth;
        }

        public void SetFacing(Facing facing)
        {
            this.currentFacing = facing;
            texture = facings[(int)facing];
            SetSize(texture.Width, texture.Height);
            RefreshDepth();
        }

        public void SetPartType(PartTypes part)
        {
            Texture2D up = ResourceManager.GetTextureFromPartAndFacing(Facing.Up, part);
            Texture2D down = ResourceManager.GetTextureFromPartAndFacing(Facing.Down, part);
            Texture2D left = ResourceManager.GetTextureFromPartAndFacing(Facing.Left, part);
            Texture2D right = ResourceManager.GetTextureFromPartAndFacing(Facing.Right, part);
            SetTextures(up, down, left, right);
        }

        private void SetTextures(Texture2D up, Texture2D down, Texture2D left, Texture2D right)
        {
            facings[(int)Facing.Up] = up;
            facings[(int)Facing.Down] = down;
            facings[(int)Facing.Left ] = left;
            facings[(int)Facing.Right] = right;
        }


        /**
         * animation logic
         */
        Vector3 velocity, position;
        float friction = 0.5f;
        float initialPower = 200f;
        float gravity = 300;
        float rotateSpeed = (float)(Math.PI * 3);
        static Random random { get { return RandomCore.random; } }
        public void FlyInRandomDirection()
        {
            Vector3 randomDirection = new Vector3(
                (float)(random.NextDouble() - 0.5), 
                (float)(random.NextDouble() - 0.5), 
                (float)(random.NextDouble() / -2));

            randomDirection.Normalize();
            randomDirection *= initialPower;
            velocity = randomDirection;

            rotateSpeed *= (float)(random.NextDouble()-0.5);

            position = new Vector3(this.blob.transform.Position.X, this.blob.transform.Position.Y, 0);
            currentUpdate = Update_Flying;

            z = 0.98f;
        }

        private delegate void UpdateFunc();
        private UpdateFunc currentUpdate;

        public override void Update()
        {
            base.Update();
            currentUpdate();
        }

        void Update_Nothing()
        {
        }

        void Update_Flying()
        {
            velocity.Z += gravity * Time.deltaTime;
            position += velocity * Time.deltaTime;
            this.blob.transform.Position = new Vector2(position.X, position.Y);
            this.scale = MathHelper.Lerp(1, 3, position.Z / -100);
            GameManager.Instance.secondLabel.GetComponent<Label>().text = position.Z.ToString();
            //this.rotation += rotateSpeed * Time.deltaTime;

            //bounced
            if (position.Z >= 0)
            {
                velocity *= friction;
                velocity = Vector3.Reflect(velocity, new Vector3(0, 0, -1));

                if (!GameManager.Instance.InVisualBounds(rectangle))
                {
                    currentUpdate = Update_Nothing;
                    this.blob.AddComponent(new Falling(Fell, this, 2));
                }
            }

            if (velocity.LengthSquared() < 10)
            {
                currentUpdate = Update_OnGround;
                //blob.AddComponent(new FadeInSeconds(128));
                blob.AddComponent(new DieInSeconds(8));
                color = Color.Lerp(color, Color.Black, .5f);

                LimbPickup pickup = new LimbPickup();
                this.blob.AddComponent(pickup);
            }
        }

        void Fell()
        {
            this.blob.Destroy();
        }

        void Update_OnGround()
        {

        }
    }
}
