using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CyborgPunch.Core;

namespace CyborgPunch.Game.Limbs
{
    public class LimbManager
    {
        private static LimbManager instance;
        public static LimbManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new LimbManager();
                }

                return instance; 
            }
        }

        private HashSet<int> blobIDs;
        private List<LimbPickup> allLimbs;

        private LimbManager()
        {
            allLimbs = new List<LimbPickup>();
            blobIDs = new HashSet<int>();
        }

        public void Clean()
        {
            allLimbs.Clear();
            blobIDs.Clear();
        }

        public void RegisterLimb(LimbPickup limb)
        {
            if (blobIDs.Add(limb.blob.ID))
            {
                allLimbs.Add(limb);
            }
        }

        public void UnregisterLimb(LimbPickup limb)
        {
            if (blobIDs.Contains(limb.blob.ID))
            {
                allLimbs.Remove(limb);
                blobIDs.Remove(limb.blob.ID);
            }
        }

        public List<LimbPickup> GetLimbs()
        {
            return allLimbs;
        }
    }
}
