using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using CyborgPunch.Game.Enemies;

namespace CyborgPunch.Game.Limbs
{
    public class HitFlash : Component
    {
        public float duration = 0.2f;
        float timer;
        float knockback;
        int damage;
        int piercing;
        Facing face;
        Damage damageComp;

        public HitFlash(int damage, float knockback, Facing face, int piercing)
            : base()
        {
            this.piercing = piercing;
            this.damage = damage;
            this.face = face;
            this.knockback = knockback;
        }

        public override void Start()
        {
            base.Start();

            damageComp = new Damage(damage, piercing);
            damageComp.knockbackPower = knockback;
            Sprite sprite = new Sprite(ResourceManager.hitFlash);
            sprite.SetAnchor(Sprite.AnchorType.Middle_Center);
            float rotation = VectorFacing.FacingToPi(face);
            sprite.rotation = rotation;

            FadeInSeconds fade = new FadeInSeconds(duration * 1.6f);

            Collider collider = new Collider();
            collider.bounds = new Rectangle(0,0,sprite.width,sprite.height);
            collider.offset = new Vector2(-sprite.origin.X, -sprite.origin.Y);

            this.blob.AddComponent(damageComp);
            this.blob.AddComponent(sprite);
            this.blob.AddComponent(collider);
            this.blob.AddComponent(fade);
            timer = duration;
        }
        int frameCount = 0;
        public override void Update()
        {
            base.Update();

            timer -= Time.deltaTime;

            if (frameCount++ > 1)
            {
                damageComp.enabled = false;
            }

            if (timer <= 0)
            {
                blob.Destroy();
            }
        }
    }
}
