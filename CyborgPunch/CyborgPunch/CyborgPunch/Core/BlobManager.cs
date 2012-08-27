using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyborgPunch.Core
{
    public class BlobManager
    {
        private static BlobManager instance;
        public static BlobManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new BlobManager();
                    instance.ResetRoot();
                }

                return instance; 
            }
        }

        public Blob RootBlob;

        public void ResetRoot()
        {
            if(RootBlob != null)
            {
                RootBlob.Destroy();
            }

            instance.RootBlob = new Blob(true);
        }

        private HashSet<int> blobIDs;
        private List<Blob> blobs;
        private bool paused;
        private float timer;

        private BlobManager()
        {
            blobs = new List<Blob>();
            blobIDs = new HashSet<int>();
        }

        public void PauseForDuration(float duration)
        {
            timer = duration;
            paused = true;
        }

        public void RegisterBlob(Blob blob)
        {
            if (blobIDs.Add(blob.ID))
            {
                blobs.Add(blob);
            }
        }

        public void UnregisterBlob(Blob blob)
        {
            if (blobIDs.Contains(blob.ID))
            {
                blobs.Remove(blob);
                blobIDs.Remove(blob.ID);
            }
        }

        public void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                paused = false;
            }

            if (!paused)
            {

                for (int i = 0; i < blobs.Count; i++)
                {
                    if (blobs[i].enabled)
                    {
                        blobs[i].Update();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            for (int i = 0; i < blobs.Count; i++)
            {
                if (blobs[i].enabled)
                {
                    blobs[i].Draw(spriteBatch);
                }
            }
            spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            for (int i = 0; i < blobs.Count; i++)
            {
                if (blobs[i].enabled)
                {
                    blobs[i].DrawNonPreMult(spriteBatch);
                }
            }
            spriteBatch.End();
        }
    }
}
