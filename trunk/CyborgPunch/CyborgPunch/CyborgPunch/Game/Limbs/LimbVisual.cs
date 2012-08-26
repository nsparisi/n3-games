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
        private Rectangle[] boundAreas;

        public LimbVisual(PartTypes part)
            : base()
        {
            facings = new Texture2D[4];
            boundAreas = new Rectangle[4];
            SetPartType(part);
            SetFacing(Facing.Down);
        }

        public void SetFacing(Facing facing)
        {
            texture = facings[(int)facing];
            SetSize(texture.Width, texture.Height);
        }

        public void SetPartType(PartTypes part)
        {
            Texture2D up = ResourceManager.GetTextureFromPartAndFacing(Facing.Up, part);
            Texture2D down = ResourceManager.GetTextureFromPartAndFacing(Facing.Down, part);
            Texture2D left = ResourceManager.GetTextureFromPartAndFacing(Facing.Left, part);
            Texture2D right = ResourceManager.GetTextureFromPartAndFacing(Facing.Right, part);
            SetTextures(up, down, left, right);

            //Rectangle upRect = ResourceManager.GetBounds(up);
            //Rectangle downRect = ResourceManager.GetBounds(down);
            //Rectangle leftRect = ResourceManager.GetBounds(left);
            //Rectangle rightRect = ResourceManager.GetBounds(right);
        }

        private void SetTextures(Texture2D up, Texture2D down, Texture2D left, Texture2D right)
        {
            facings[(int)Facing.Up] = up;
            facings[(int)Facing.Down] = down;
            facings[(int)Facing.Left ] = left;
            facings[(int)Facing.Right] = right;
        }
    }
}
