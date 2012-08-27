using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using CyborgPunch.Game;

namespace CyborgPunch.Core
{
    class Sprite : Component
    {
        public Texture2D texture;
        public Color color;
        public int width;
        public int height;
        public Vector2 origin;
        public float rotation;
        public float z;
        public float scale;
        public bool drawNonPremultiply;

        protected Rectangle rectangle;
        private Rectangle source;


        public Sprite()
            : base()
        {
            color = Color.White;
            rectangle = new Rectangle();
            source = new Rectangle(0, 0, 1, 1);
            scale = 1;
        }

        public Sprite(Texture2D tex)
            : this()
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

        public Vector2 GetSize()
        {
            return new Vector2(width, height);
        }

        private void RefreshRectangle()
        {
            rectangle = new Rectangle(
                (int)(blob.transform.Position.X + offset.X),
                (int)(blob.transform.Position.Y + offset.Y),
                (int)(width * scale),
                (int)(height * scale));

            if (color.A < byte.MaxValue)
            {
                drawNonPremultiply = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null && !drawNonPremultiply)
            {
                RefreshRectangle();
                spriteBatch.Draw(texture, rectangle, null, color, rotation, origin, SpriteEffects.None, z);
            }
        }

        public override void DrawNonPreMult(SpriteBatch spriteBatch)
        {
            base.DrawNonPreMult(spriteBatch);

            if (texture != null && drawNonPremultiply)
            {
                RefreshRectangle();
                spriteBatch.Draw(texture, rectangle, null, color, rotation, origin, SpriteEffects.None, z);
            }
        }
        /*
        public override void DrawDebug(SpriteBatch spriteBatch)
        {
            base.DrawDebug(spriteBatch);
            spriteBatch.Draw(ResourceManager.texture_White,
                new Rectangle((int)blob.transform.Position.X, (int)blob.transform.Position.Y, 2, 2), 
                null, color);
        }
         * */



        public enum AnchorType { Upper_Left, Upper_Center, Upper_Right, Middle_Left, Middle_Center, Middle_Right, Bottom_Left, Bottom_Center, Bottom_Right }
        private AnchorType anchor = AnchorType.Upper_Left;
        private Vector2 offset;

        public void SetAnchor(AnchorType anchor)
        {
            this.anchor = anchor;
            Refresh();
        }

        private void Refresh()
        {
            if (anchor == AnchorType.Upper_Left)
            {
                origin = new Vector2(0, 0);
            }
            else if (anchor == AnchorType.Upper_Center)
            {
                origin = new Vector2(width / 2, 0);
            }
            else if (anchor == AnchorType.Upper_Right)
            {
                origin = new Vector2(width, 0);
            }
            else if (anchor == AnchorType.Middle_Left)
            {
                origin = new Vector2(0, height / 2);
            }
            else if (anchor == AnchorType.Middle_Center)
            {
                origin = new Vector2(width / 2, height / 2);
            }
            else if (anchor == AnchorType.Middle_Right)
            {
                origin = new Vector2(width, height / 2);
            }
            else if (anchor == AnchorType.Bottom_Left)
            {
                origin = new Vector2(0, height);
            }
            else if (anchor == AnchorType.Bottom_Center)
            {
                origin = new Vector2(width / 2, height);
            }
            else if (anchor == AnchorType.Bottom_Right)
            {
                origin = new Vector2(width, height);
            }
        }
    }
}
