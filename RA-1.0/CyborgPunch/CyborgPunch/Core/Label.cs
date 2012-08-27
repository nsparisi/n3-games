
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CyborgPunch.Game;

namespace CyborgPunch.Core
{
    class Label : Component
    {
        SpriteFont font;
        public Color color;

        public string text;

        public Vector2 scale;

        private AlignType align;

        private AnchorType anchor;

        private Vector2 origin;
        public enum AlignType { Left, Center, Right }
        public enum AnchorType { Upper, Middle, Bottom }


        public Label() : base()
        {
            font = ResourceManager.font_Common;
            text = "Label";
            color = Color.Black;
            scale = Vector2.One;
        }

        public void SetAlign(AlignType align)
        {
            this.align = align;
        }

        public void SetAnchor(AnchorType anchor)
        {
            this.anchor = anchor;
        }

        private void Refresh()
        {
            Vector2 measure = font.MeasureString(text);
            if (align == AlignType.Left)
            {
                origin.Y = 0;
            }
            else if(align == AlignType.Center)
            {
                origin.X = measure.X / 2;
            }
            else if (align == AlignType.Right)
            {
                origin.X = measure.X;
            }

            if (anchor == AnchorType.Upper)
            {
                origin.Y = 0;
            }
            else if (anchor == AnchorType.Middle)
            {
                origin.Y = measure.Y / 2;
            }
            else if (anchor == AnchorType.Bottom)
            {
                origin.Y = measure.Y;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Refresh();
            spriteBatch.DrawString(font, text, blob.transform.Position, color, 0, origin, scale, SpriteEffects.None, 0);
        }
    }
}
