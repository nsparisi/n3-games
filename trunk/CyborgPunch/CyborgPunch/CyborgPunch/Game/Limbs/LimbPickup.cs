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
    public class LimbPickup : Component
    {

        public LimbPickup()
            : base()
        {
          
        }

        public override void Start()
        {
            base.Start();
            LimbManager.Instance.RegisterLimb(this);
            
            Collider collider = new Collider();
            collider.bounds = ResourceManager.GetBounds(blob.GetComponent<Sprite>().texture);
            collider.offset = new Vector2(collider.bounds.X, collider.bounds.Y);
            this.blob.AddComponent(collider);
        }

        void Pickup()
        {
            LimbManager.Instance.UnregisterLimb(this);
            this.blob.Destroy();
        }
    }
}
