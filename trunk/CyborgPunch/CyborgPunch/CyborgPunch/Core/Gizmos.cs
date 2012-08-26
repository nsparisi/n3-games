using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using CyborgPunch.Game;

namespace CyborgPunch.Core
{
    public class Gizmos
    {
        public static Color color;

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rect)
        {
            if (!Constants.DEBUG)
                return;

            Texture2D line = ResourceManager.texture_White;
            int thickness = 2;
            Rectangle left = new Rectangle(rect.X, rect.Y, thickness, rect.Height);
            Rectangle right = new Rectangle(rect.Right - thickness, rect.Y, thickness, rect.Height);
            Rectangle up = new Rectangle(rect.X, rect.Y, rect.Width, thickness);
            Rectangle down = new Rectangle(rect.X, rect.Bottom - thickness, rect.Width, thickness);

            spriteBatch.Draw(line, left, color);
            spriteBatch.Draw(line, right, color);
            spriteBatch.Draw(line, up, color);
            spriteBatch.Draw(line, down, color);
        }
    }
}
