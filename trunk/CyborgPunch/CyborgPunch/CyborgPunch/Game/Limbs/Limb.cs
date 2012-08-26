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
    class Limb : Component
    {
        public enum LimbType { Head, Arm, Leg, Torso }
        public enum LimbPosition { Left, Right }
        public enum ArmSubType { Hook = 0, Long, Hammer, Human }
        public enum HeadSubType { Bite = 0, Laser, Bomb, Human }
        public enum LegSubType { Gun = 0, Rocket, Human }
        public enum TorsoSubType { Human = 0, Robot }

        private LimbType type;
        private LimbPosition position;
        private ArmSubType arm;
        private HeadSubType head;
        private LegSubType leg;
        private TorsoSubType torso;

        static Random random = new Random();

        //make a random limb of this type
        public Limb(LimbType type, LimbPosition position)
            : base()
        {
            this.type = type;
            this.position = position;
            this.arm = (ArmSubType)random.Next(0, 3);
            this.head = (HeadSubType)random.Next(0, 3);
            this.leg = (LegSubType)random.Next(0, 2);
            this.torso = (TorsoSubType)random.Next(0, 2);
        }

        public override void Start()
        {
            base.Start();

            PartTypes theType = PartTypes.Torso;

            if (type == LimbType.Head)
            {
                if (head == HeadSubType.Bite)
                {
                    theType = PartTypes.BiteHead;
                }
                else if (head == HeadSubType.Bomb)
                {
                    theType = PartTypes.BombHead;
                }
                else if (head == HeadSubType.Laser)
                {
                    theType = PartTypes.LaserHead;
                }
                else if (head == HeadSubType.Human)
                {
                    theType = PartTypes.Head;
                }
            }
            else if (type == LimbType.Arm)
            {
                if (arm == ArmSubType.Hammer)
                {
                    theType = position == LimbPosition.Left ? PartTypes.HammerArmLeft : PartTypes.HammerArmRight;
                }
                else if (arm == ArmSubType.Hook)
                {
                    theType = position == LimbPosition.Left ? PartTypes.HookArmLeft : PartTypes.HookArmRight;
                }
                else if (arm == ArmSubType.Long)
                {
                    theType = position == LimbPosition.Left ? PartTypes.LongArmLeft : PartTypes.LongArmRight;
                }
                else if (arm == ArmSubType.Human)
                {
                    theType = position == LimbPosition.Left ? PartTypes.LeftArm : PartTypes.RightArm;
                }
            }
            else if (type == LimbType.Leg)
            {
                if (leg == LegSubType.Gun)
                {
                    theType = position == LimbPosition.Left ? PartTypes.GunLegLeft : PartTypes.GunLegRight;
                }
                else if (leg == LegSubType.Rocket)
                {
                    theType = position == LimbPosition.Left ? PartTypes.RocketLegLeft : PartTypes.RocketLegRight;
                }
                else if (leg == LegSubType.Human)
                {
                    theType = position == LimbPosition.Left ? PartTypes.LeftLeg : PartTypes.RightLeg;
                }
            }
            else if (type == LimbType.Torso)
            {
                if (torso == TorsoSubType.Human)
                {
                    theType = PartTypes.RobotTorso;
                }
                else if (torso == TorsoSubType.Robot)
                {
                    theType = PartTypes.Torso;
                }
            }

            LimbVisual visual = new LimbVisual(theType);
            this.blob.AddComponent(visual);
        }
    }
}
