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
    public class ActorManager 
    {
        private static ActorManager instance;
        public static ActorManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new ActorManager();
                }

                return instance; 
            }
        }

        private HashSet<int> actorIDs;
        private List<Actor> actors;

        private ActorManager()
        {
            actors = new List<Actor>();
            actorIDs = new HashSet<int>();
        }

        public void RegisterActor(Actor actor)
        {
            if (actorIDs.Add(actor.ID))
            {
                actors.Add(actor);
            }
        }

        public void UnregisterActor(Actor actor)
        {
            if (actorIDs.Contains(actor.ID))
            {
                actors.Remove(actor);
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < actors.Count; i++)
            {
                if (actors[i].enabled)
                {
                    actors[i].Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int i = 0; i < actors.Count; i++)
            {
                if (actors[i].enabled)
                {
                    actors[i].Draw(spriteBatch);
                }
            }
            spriteBatch.End();
        }
    }
}
