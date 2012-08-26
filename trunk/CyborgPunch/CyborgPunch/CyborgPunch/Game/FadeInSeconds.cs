using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;

namespace CyborgPunch.Game
{
    class FadeInSeconds : Component
    {
        float fadeTime;
        Sprite sprite;

        public FadeInSeconds(float fadeTime) : base()
        {
            this.fadeTime = fadeTime;
        }

        public override void Start()
        {
            base.Start();

            sprite = blob.GetComponent<Sprite>();
        }

        public override void Update()
        {
            base.Update();

            Color spriteColor = sprite.color;
            float newAlpha = (spriteColor.A - (Time.deltaTime * (1f / fadeTime) * byte.MaxValue));
            spriteColor.A = (byte)newAlpha;
            sprite.color = spriteColor;
        }
    }
}
