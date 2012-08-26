using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyborgPunch.Core
{
    public class Collider : Component
    {
        public Rectangle bounds;
        public Vector2 offset;

        public Collider() : base()
        {
        }

        public virtual bool Collides(Collider actor)
        {
            if (actor == null)
                return false;

            return actor.bounds.Intersects(bounds);
        }

        public override void Update()
        {
            base.Update();
            RefreshRectangle();
        }

        private void RefreshRectangle()
        {
            bounds.X = (int)(blob.transform.Position.X + offset.X);
            bounds.Y = (int)(blob.transform.Position.Y + offset.Y);
        }

        public override void DrawDebug(SpriteBatch spriteBatch)
        {
            base.DrawDebug(spriteBatch);

            Gizmos.color = Color.Green;
            Gizmos.DrawRectangle(spriteBatch, bounds);
        }
    }
}
