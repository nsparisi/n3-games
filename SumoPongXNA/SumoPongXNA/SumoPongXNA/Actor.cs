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
    public abstract class Actor 
    {
        public int ID { get; private set; }
        public Transform transform;
        public bool enabled = true;
        public Rectangle bounds;

        private static int IDCount = 0;

        protected Actor()
        {
            transform = new Transform();
            this.ID = IDCount++;
            ActorManager.Instance.RegisterActor(this);
        }
        
        public static void Destroy(Actor actor)
        {
            ActorManager.Instance.UnregisterActor(actor);
        }

        public virtual bool Collides(Actor actor)
        {
            if (actor == null)
                return false;

            return actor.bounds.Intersects(bounds);
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
