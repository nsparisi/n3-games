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
    public class Label : Actor 
    {
        public Color color;
        public string text;

        public Label()
        {
            color = Color.White;
            transform = new Transform();
            ActorManager.Instance.RegisterActor(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(ContentLoader.Font_Main, text, transform.position, color);
        }
    }
}
