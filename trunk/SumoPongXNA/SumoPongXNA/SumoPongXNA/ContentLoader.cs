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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ContentLoader
    {
        public static Texture2D Texure_Paddle;
        public static Texture2D Texure_White;
        public static SpriteFont Font_Main;

        public static void LoadAll(ContentManager manager)
        {
            Texure_Paddle = manager.Load<Texture2D>("Sprites//test");
            Texure_White = manager.Load<Texture2D>("Sprites//white");
            Font_Main = manager.Load<SpriteFont>("Font//MainFont");
        }
    }
}
