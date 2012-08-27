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
        Facing face;

        public HitFlash(int damage, float knockback, Facing face)
            : base()
        {
            this.damage = damage;
            this.face = face;
            this.knockback = knockback;
        }

        public override void Start()
        {
            base.Start();

            Damage damageComp = new Damage(damage);
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

        public override void Update()
        {
            base.Update();

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                blob.Destroy();
            }
        }
    }
}
