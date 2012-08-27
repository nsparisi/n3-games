using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyborgPunch.Game.Limbs
{
    class Falling : Component
    {
        public delegate void FellCallback();
        FellCallback fellCallback;
        Sprite sprite;
        float timer = 0;
        float duration;

        public Falling(FellCallback callback, Sprite sprite, float duration)
            : base()
        {
            fellCallback = callback;
            this.sprite = sprite;
            this.duration = duration;
        }

        public override void Start()
        {
            base.Start();

            timer = duration;
        }

        public override void Update()
        {
            base.Update();

            timer -= Time.deltaTime;

            float scale = MathHelper.Lerp(0.01f, 1, timer / duration);
            sprite.scale = scale;

            if (timer <= 0)
            {
                fellCallback();
                this.enabled = false;
            }
        }
    }
}
