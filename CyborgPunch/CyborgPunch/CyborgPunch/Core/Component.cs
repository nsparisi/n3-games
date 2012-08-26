using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyborgPunch.Core
{
    public abstract class Component
    {
        public bool enabled = true;
        public Blob blob;

        protected Component()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void DrawDebug(SpriteBatch spriteBatch)
        {
        }

        public virtual void Start()
        {
        }
    }
}
