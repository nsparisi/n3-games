﻿using System;
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

        public DudeMovement movement { get { return blob.GetComponent<DudeMovement>(); } }

        public Collider Collider
        {
            get
            {
                if (collider == null)
                    collider = blob.GetComponent<Collider>();
                return collider;
            }
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
            GetBodyPart(LimbType.RightLeg).AddComponent(new HumanLeg(this, LimbType.RightLeg));
            GetBodyPart(LimbType.LeftLeg).AddComponent(new HumanLeg(this, LimbType.LeftLeg));
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


        public bool dying;
        float dieDuration = 2f;
        float timer;

        public override void Update()
        {
            base.Update();

            if (dying)
            {
                timer -= Time.deltaTime;

                if (timer < 0)
                {
                    BlobManager.Instance.ResetRoot();
                    Game1.Instance.GoToEndScreen();
                }
            }
            else if (!dying)
            {

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
                    SoundManager.PlaySound(SoundManager.SFX_ATTACH_LIMB);
                    return;
                }
            }
        }

        public void Hit()
        {
            if (!dying)
            {

                timer = dieDuration;
                dying = true;
                movement.enabled = false;

                Blob head = RemoveBodyPart(LimbType.Head);
                if (head != null)
                {
                    head.transform.Parent = null;
                    head.RemoveComponent<LimbPunch>();
                    head.GetComponent<LimbVisual>().FlyInRandomDirection();
                }

                Blob lLeg = RemoveBodyPart(LimbType.LeftArm);
                if (lLeg != null)
                {
                    lLeg.transform.Parent = null;
                    lLeg.RemoveComponent<LimbPunch>();
                    lLeg.GetComponent<LimbVisual>().FlyInRandomDirection();
                }

                Blob rLeg = RemoveBodyPart(LimbType.LeftLeg);
                if (rLeg != null)
                {
                    rLeg.transform.Parent = null;
                    rLeg.RemoveComponent<LimbPunch>();
                    rLeg.GetComponent<LimbVisual>().FlyInRandomDirection();
                }

                Blob rArm = RemoveBodyPart(LimbType.RightArm);
                if (rArm != null)
                {
                    rArm.transform.Parent = null;
                    rArm.RemoveComponent<LimbPunch>();
                    rArm.GetComponent<LimbVisual>().FlyInRandomDirection();
                }

                Blob lArm = RemoveBodyPart(LimbType.RightLeg);
                if (lArm != null)
                {
                    lArm.transform.Parent = null;
                    lArm.RemoveComponent<LimbPunch>();
                    lArm.GetComponent<LimbVisual>().FlyInRandomDirection();
                }


                Blob blood = new Blob();
                blood.AddComponent(new Sprite(ResourceManager.Blood));
                blood.transform.Parent = this.blob.transform;
                blood.transform.LocalPosition = new Vector2(-17,20);
                blood.GetComponent<Sprite>().SetAnchor(Sprite.AnchorType.Upper_Left);
                blood.GetComponent<Sprite>().z = 0.99f;

                blood = new Blob();
                blood.AddComponent(new Sprite(ResourceManager.Blood));
                blood.transform.Parent = this.blob.transform;
                blood.transform.LocalPosition = new Vector2(15, 40);
                blood.GetComponent<Sprite>().SetAnchor(Sprite.AnchorType.Middle_Center);
                blood.GetComponent<Sprite>().z = 0.0f;
                blood.GetComponent<Sprite>().scale = 3f;
                blood.AddComponent(new FadeInSeconds(1));

                SoundManager.PlaySound(SoundManager.SFX_PLAYER_DIES_MAYBE);
            }
        }
    }
}