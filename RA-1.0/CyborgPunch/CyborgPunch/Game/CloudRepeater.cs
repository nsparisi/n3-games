using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyborgPunch.Game
{
    class CloudRepeater : Component
    {
        float speed;
        Blob one;
        Blob two;
        Blob three;
        Texture2D tex;
        float z;

        public CloudRepeater(Texture2D tex, float speed, float z)
        {
            this.speed = speed;
            this.tex = tex;
            this.z = z;
        }

        public override void Start()
        {
            base.Start();

            one = new Blob();
            two = new Blob();
            three = new Blob();

            one.transform.Parent = this.blob.transform;
            two.transform.Parent = this.blob.transform;
            three.transform.Parent = this.blob.transform;

            one.transform.LocalPosition = Vector2.Zero;
            two.transform.LocalPosition = Vector2.Zero;
            three.transform.LocalPosition = Vector2.Zero;

            Sprite oneSprite = new Sprite(tex);
            Sprite twoSprite = new Sprite(tex);
            Sprite threeSprite = new Sprite(tex);

            oneSprite.z = z;
            twoSprite.z = z;
            threeSprite.z = z;

            one.AddComponent(oneSprite);
            two.AddComponent(twoSprite);
            three.AddComponent(threeSprite);

            two.transform.Position = one.transform.Position + new Vector2(oneSprite.width, 0);
            three.transform.Position = one.transform.Position + new Vector2(oneSprite.width * 2, 0);
        }

        public override void Update()
        {
            this.blob.transform.Translate(speed * Time.deltaTime, 0);

            CheckOffScreen(one);
            CheckOffScreen(two);
            CheckOffScreen(three);
        }

        void CheckOffScreen(Blob cloud)
        {
            if (cloud.transform.Position.X <= -tex.Width)
            {
                cloud.transform.Translate(tex.Width * 2, 0);
            }
        }
    }
}
