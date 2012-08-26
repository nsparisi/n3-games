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
    public class Limb : Component
    {
        public enum LimbComponentType { Head, Arm, Leg, Torso }
        public enum LimbPosition { Left, Right }
        public enum ArmSubType { Hook = 0, Long, Hammer, Human }
        public enum HeadSubType { Bite = 0, Laser, Bomb, Human }
        public enum LegSubType { Gun = 0, Rocket, Human }
        public enum TorsoSubType { Robot = 0, Human }

        public LimbComponentType type { get; private set; }
        public LimbPosition position { get; private set; }
        public ArmSubType arm { get; private set; }
        public HeadSubType head { get; private set; }
        public LegSubType leg { get; private set; }
        public TorsoSubType torso { get; private set; }

        float humanoidZ = 0;

        static Random random = new Random();

        //make a random limb of this type
        public Limb(LimbComponentType type, LimbPosition position, float humanoidZ)
            : base()
        {
            this.type = type;
            this.position = position;
            this.arm = (ArmSubType)random.Next(0, 3);
            this.head = (HeadSubType)random.Next(0, 3);
            this.leg = (LegSubType)random.Next(0, 2);
            this.torso = (TorsoSubType)random.Next(0, 1);
            this.humanoidZ = humanoidZ;
        }

        public Limb(LimbComponentType type, LimbPosition position, float humanoidZ, bool isHuman)
            : this(type, position, humanoidZ)
        {
            if (isHuman)
            {
                this.arm = ArmSubType.Human;
                this.head = HeadSubType.Human;
                this.leg = LegSubType.Human;
                this.torso = TorsoSubType.Human;
            }
        }

        public Limb(Limb other, LimbPosition newPosition, float humanoidZ)
            : base()
        {
            this.type = other.type;
            this.position = newPosition;
            this.arm = other.arm;
            this.head = other.head;
            this.leg = other.leg;
            this.torso = other.torso;
            this.humanoidZ = humanoidZ;
        }

        public bool IsMatchingLimb(LimbType limbType)
        {
            if (limbType == LimbType.Head)
            {
                return type == LimbComponentType.Head;
            }

            else if (limbType == LimbType.LeftArm || limbType == LimbType.RightArm)
            {
                return type == LimbComponentType.Arm;
            }

            else if (limbType == LimbType.LeftLeg || limbType == LimbType.RightLeg)
            {
                return type == LimbComponentType.Leg;
            }

            return false;
        }

        public override void Start()
        {
            base.Start();

            PartTypes theType = PartTypes.Torso;

            if (type == LimbComponentType.Head)
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
            else if (type == LimbComponentType.Arm)
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
            else if (type == LimbComponentType.Leg)
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
            else if (type == LimbComponentType.Torso)
            {
                if (torso == TorsoSubType.Human)
                {
                    theType = PartTypes.Torso;
                }
                else if (torso == TorsoSubType.Robot)
                {
                    theType = PartTypes.RobotTorso;
                }
            }

            LimbVisual visual = new LimbVisual(theType);
            this.blob.AddComponent(visual);
            PrepareDepth(visual);
        }

        void PrepareDepth(LimbVisual visual)
        {

            float[] headZ = new float[4];
            float[] torsoZ = new float[4];
            float[] leftArmZ = new float[4];
            float[] rightArmZ = new float[4];
            float[] leftLegZ = new float[4];
            float[] rightLegZ = new float[4];

            headZ[(int)Facing.Down] = 1;
            torsoZ[(int)Facing.Down] = 2;
            leftArmZ[(int)Facing.Down] = 3;
            rightArmZ[(int)Facing.Down] = 4;
            leftLegZ[(int)Facing.Down] = 5;
            rightLegZ[(int)Facing.Down] = 6;

            headZ[(int)Facing.Up] = 1;
            torsoZ[(int)Facing.Up] = 2;
            leftArmZ[(int)Facing.Up] = 3;
            rightArmZ[(int)Facing.Up] = 4;
            leftLegZ[(int)Facing.Up] = 5;
            rightLegZ[(int)Facing.Up] = 6;

            headZ[(int)Facing.Left] = 1;
            leftArmZ[(int)Facing.Left] = 2;
            torsoZ[(int)Facing.Left] = 3;
            leftLegZ[(int)Facing.Left] = 4;
            rightLegZ[(int)Facing.Left] = 5;
            rightArmZ[(int)Facing.Left] = 6;

            headZ[(int)Facing.Right] = 1;
            rightArmZ[(int)Facing.Right] = 2;
            torsoZ[(int)Facing.Right] = 3;
            rightLegZ[(int)Facing.Right] = 4;
            leftLegZ[(int)Facing.Right] = 5;
            leftArmZ[(int)Facing.Right] = 6;

            float[] result = new float[4];

            if (type == LimbComponentType.Head)
            {
                result = headZ;
            }
            else if (type == LimbComponentType.Arm)
            {
                result = position == LimbPosition.Left ? leftArmZ : rightArmZ;
            }
            else if (type == LimbComponentType.Leg)
            {
                result = position == LimbPosition.Left ? leftLegZ : rightLegZ;
            }
            else if (type == LimbComponentType.Torso)
            {
                result = torsoZ;
            }

            for (int i = 0; i < 4; i++)
            {
                result[i] += humanoidZ;
            }

            visual.SetDepths(result);
        }
    }
}
