using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CyborgPunch.Game.Limbs;

namespace CyborgPunch.Game
{
    class Dude : Humanoid
    {
        public Dude()
            : base()
        {
        }

        bool previousHead;
        bool previousRightLeg;
        bool previousLeftLeg;
        bool previousRightArm;
        bool previousLeftArm;

        public override void Start()
        {
            base.Start();

            AddLimb(LimbType.Head, true);
            AddLimb(LimbType.LeftArm, true);
            AddLimb(LimbType.LeftLeg, true);
            AddLimb(LimbType.RightArm, true);
            AddLimb(LimbType.RightLeg, true);
            AddLimb(LimbType.Torso, true);

            GetBodyPart(LimbType.Head).AddComponent(new HumanHead(this, LimbType.Head));
            GetBodyPart(LimbType.LeftArm).AddComponent(new HumanArm(this, LimbType.LeftArm));
            GetBodyPart(LimbType.RightArm).AddComponent(new HumanArm(this, LimbType.RightArm));

            GetBodyPart(LimbType.RightLeg).AddComponent(new GunLeg(this, LimbType.RightLeg));
            //GetBodyPart(LimbType.RightLeg).AddComponent(new HumanArm(this, LimbType.RightLeg, KeyBindings.LegRightAction));

        }


        public void AttachAbilityToPart(LimbType type, Limb newLimb)
        {
            Blob part = GetBodyPart(type);
            LimbPunch action = null;

            if (newLimb.type == Limb.LimbComponentType.Head)
            {
                if (newLimb.head == Limb.HeadSubType.Bite)
                {
                    action = new BiteHead(this, type);
                }
                else if (newLimb.head == Limb.HeadSubType.Bomb)
                {
                    action = new BombHead(this, type);
                }
                else if (newLimb.head == Limb.HeadSubType.Laser)
                {
                    action = new LaserHead(this, type);
                }
                else if (newLimb.head == Limb.HeadSubType.Human)
                {
                    action = new HumanHead(this, type);
                }
            }
            else if (newLimb.type == Limb.LimbComponentType.Arm)
            {
                if (newLimb.arm == Limb.ArmSubType.Hammer)
                {
                    action = new HammerArm(this, type);
                }
                else if (newLimb.arm == Limb.ArmSubType.Hook)
                {
                    action = new HookArm(this, type );
                }
                else if (newLimb.arm == Limb.ArmSubType.Long)
                {
                    action = new LongArm(this, type );
                }
                else if (newLimb.arm == Limb.ArmSubType.Human)
                {
                    action = new HumanArm(this, type);
                }
            }
            else if (newLimb.type == Limb.LimbComponentType.Leg)
            {
                if (newLimb.leg == Limb.LegSubType.Gun)
                {
                    action = new GunLeg(this, type);
                }
                else if (newLimb.leg == Limb.LegSubType.Rocket)
                {
                    action = new RocketLeg(this, type);
                }
                else if (newLimb.leg == Limb.LegSubType.Human)
                {
                    action = new HumanArm(this, type);
                }
            }

            part.AddComponent(action);
        }


        public override void Update()
        {
            base.Update();


            KeyboardState keyState = Keyboard.GetState();
            bool HeadKeyDown = keyState.IsKeyDown(KeyBindings.HeadAction);
            bool ArmLeftKeyDown = keyState.IsKeyDown(KeyBindings.ArmLeftAction);
            bool ArmRightKeyDown = keyState.IsKeyDown(KeyBindings.ArmRightAction);
            bool LegLeftKeyDown = keyState.IsKeyDown(KeyBindings.LegLeftAction);
            bool LegRightKeyDown = keyState.IsKeyDown(KeyBindings.LegRightAction);

            if (GetBodyPart(LimbType.Head) == null && 
                JustPressedKey(previousHead, HeadKeyDown))
            {
                TryPickupBodyPart(LimbType.Head);
            }

            if (GetBodyPart(LimbType.LeftArm) == null &&
                JustPressedKey(previousLeftArm, ArmLeftKeyDown))
            {
                TryPickupBodyPart(LimbType.LeftArm);
            }

            if (GetBodyPart(LimbType.RightArm) == null &&
                JustPressedKey(previousRightArm, ArmRightKeyDown))
            {
                TryPickupBodyPart(LimbType.RightArm);
            }

            if (GetBodyPart(LimbType.LeftLeg) == null &&
                JustPressedKey(previousLeftLeg, LegLeftKeyDown))
            {
                TryPickupBodyPart(LimbType.LeftLeg);
            }

            if (GetBodyPart(LimbType.RightLeg) == null &&
                JustPressedKey(previousRightLeg, LegRightKeyDown))
            {
                TryPickupBodyPart(LimbType.RightLeg);
            }

            previousHead = HeadKeyDown;
            previousLeftArm = ArmLeftKeyDown;
            previousRightArm = ArmRightKeyDown;
            previousLeftLeg = LegLeftKeyDown;
            previousRightLeg = LegRightKeyDown;
        }


        bool JustPressedKey(bool previous, bool current)
        {
            return !previous && current;
        }

        void TryPickupBodyPart(LimbType type)
        {
            List<LimbPickup> touchedParts = new List<LimbPickup>();
            List<LimbPickup> allParts = LimbManager.Instance.GetLimbs();

            //collide all parts
            for (int i = 0; i < allParts.Count; i++)
            {
                LimbPickup part = allParts[i];
                if (part.blob.Collides(blob))
                {
                    touchedParts.Add(part);
                }
            }

            //test for matching limb
            for (int i = 0; i < touchedParts.Count; i++)
            {
                LimbPickup part = touchedParts[i];
                Limb limb = part.blob.GetComponent<Limb>();

                if (limb.IsMatchingLimb(type))
                {
                    part.Pickup();
                    AddLimbFromLimb(type, limb);
                    AttachAbilityToPart(type, limb);
                    return;
                }
            }
        }
    }
}