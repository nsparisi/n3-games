using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;

namespace CyborgPunch.Game
{
    class Shake : Component
    {
        static float strength;
        static int shakeFrames;
        static Random rand { get { return RandomCore.random; } }
        public static void ShakeIt(float howHard, int forFrames)
        {
            if (howHard > strength)
                strength = howHard;
            if (forFrames > shakeFrames)
                shakeFrames = forFrames;
            RandomShake();
        }

        public static void RandomShake()
        {
            Vector2 shakeVector = new Vector2(((float)rand.NextDouble()-.5f)*2, ((float)rand.NextDouble()-.5f)*2);
            shakeVector.Normalize();
            shakeVector *= strength;
            BlobManager.Instance.RootBlob.transform.Position = shakeVector;
            strength /= 1.25f;
        }

        public static void StopShake()
        {
            shakeFrames = -1;
            BlobManager.Instance.RootBlob.transform.Position = Vector2.Zero;
        }

        public override void Update()
        {
            base.Update();
            if (shakeFrames > 0)
            {
                shakeFrames--;
                RandomShake();
            }
            else if (shakeFrames == 0)
            {
                StopShake();
            }

        }
        
    }
}
