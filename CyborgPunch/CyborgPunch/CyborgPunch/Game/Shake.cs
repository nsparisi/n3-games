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
        static Random rand = new Random();
        public static void ShakeIt(float howHard, int forFrames)
        {
            strength = howHard;
            shakeFrames = forFrames;
            RandomShake();
        }

        public static void RandomShake()
        {
            Vector2 shakeVector = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble());
            shakeVector.Normalize();
            shakeVector *= strength;
            BlobManager.Instance.RootBlob.transform.Position = shakeVector;
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
