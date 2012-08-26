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
    public class Transform 
    {
        public Vector2 position;

        public Transform()
        {
            position = Vector2.Zero;
        }

        public void Translate(float x, float y)
        {
            this.position += new Vector2(x, y);
        }

        public void Translate(Vector2 change)
        {
            this.position += change;
        }
    }
}
