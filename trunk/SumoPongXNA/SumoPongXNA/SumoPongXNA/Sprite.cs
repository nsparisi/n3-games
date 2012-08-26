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
    public class Sprite : Actor
    {
        public Texture2D texture;
        public Color color;

        public int width;
        public int height;

        private Rectangle rectangle;

        public Sprite() : base()
        {
            color = Color.White;
            rectangle = new Rectangle();
        }

        public Sprite(Texture2D tex) : base()
        {
            texture = tex;
            color = Color.White;
            width = tex.Width;
            height = tex.Height;
        }

        public void SetSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        private void RefreshRectangle()
        {
            rectangle = new Rectangle((int)transform.position.X, 
                (int)transform.position.Y, width, height);
            this.bounds = rectangle;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                RefreshRectangle();
                spriteBatch.Draw(texture, rectangle, color);
            }
        }
    }
}
