using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CyborgPunch.Game.Limbs;
using Microsoft.Xna.Framework.Graphics;

namespace CyborgPunch.Game
{
    public class Humanoid : Component
    {
        private Blob[] bodyIndex;
        private Facing facing = Facing.Down;

        private int[] yCoordinates = {   35, //left arm
                                         35, //right arm
                                         55, //right leg
                                         55, //left leg
                                         0, //head
                                         29 //torso
                                     };

        static Random rand = new Random();

        protected Collider collider;

        public float randomDepth = 0;

        public Humanoid()
            : base()
        {
            bodyIndex = new Blob[Enum.GetValues(typeof(LimbType)).Length];
        }

        public override void Start()
        {
            randomDepth = (float)rand.NextDouble();

            this.blob.ToString();

            AddLimb(LimbType.Head);
            AddLimb(LimbType.LeftArm);
            AddLimb(LimbType.LeftLeg);
            AddLimb(LimbType.RightArm);
            AddLimb(LimbType.RightLeg);
            AddLimb(LimbType.Torso);


            collider = new Collider();
            collider.bounds = new Rectangle(0, 0, 55, 86);
            blob.AddComponent(collider);
        }

        public void AddLimb(LimbType limbType)
        {
            if (GetBodyPart(limbType) == null)
            {
                Blob limb = new Blob();
                limb.transform.Parent = blob.transform;
                limb.transform.LocalPosition = new Vector2(0, yCoordinates[(int)limbType]);

                Limb.LimbComponentType componentType = Limb.LimbComponentType.Head;
                if(limbType == LimbType.Head)componentType = Limb.LimbComponentType.Head;
                else if (limbType == LimbType.LeftArm || limbType == LimbType.RightArm) componentType = Limb.LimbComponentType.Arm;
                else if (limbType == LimbType.LeftLeg || limbType == LimbType.RightLeg) componentType = Limb.LimbComponentType.Leg;
                else if (limbType == LimbType.Torso) componentType = Limb.LimbComponentType.Torso;

                Limb.LimbPosition positionType = Limb.LimbPosition.Left;
                if (limbType == LimbType.RightLeg || limbType == LimbType.RightArm) positionType = Limb.LimbPosition.Right;
                
                Limb visual = new Limb(componentType, positionType, randomDepth);
                limb.AddComponent(visual);

                SetBodyPart(limbType, limb);
                SetFacing(facing);
            }
        }

        public void AddLimbFromLimb(LimbType limbType, Limb previousLimb)
        {
            if (GetBodyPart(limbType) == null)
            {
                Blob limb = new Blob();
                limb.transform.Parent = blob.transform;
                limb.transform.LocalPosition = new Vector2(0, yCoordinates[(int)limbType]);
                
                Limb.LimbPosition positionType = Limb.LimbPosition.Left;
                if (limbType == LimbType.RightLeg || limbType == LimbType.RightArm) positionType = Limb.LimbPosition.Right;

                Limb visual = new Limb(previousLimb, positionType, randomDepth);
                limb.AddComponent(visual);

                SetBodyPart(limbType, limb);
                SetFacing(facing);
            }
        }

        void SetupLimb(Blob bodyPart, Limb.LimbComponentType type, Limb.LimbPosition position)
        {
            Limb visual = new Limb(type, position, randomDepth);
            bodyPart.AddComponent(visual);
        }

        public void DiscardLimb(LimbType whichLimb)
        {
            SetBodyPart(whichLimb, null);
        }

        public void SetBodyPart(LimbType limbType, Blob newPart)
        {
            bodyIndex[(int)limbType] = newPart;
        }

        public Blob GetBodyPart(LimbType whichLimb)
        {
            return bodyIndex[(int)whichLimb];
        }

        public Blob RemoveBodyPart(LimbType whichLimb)
        {
            Blob removed = GetBodyPart(whichLimb);
            SetBodyPart(whichLimb, null);
            return removed;
        }

        public Facing GetFacing()
        {
            return facing;
        }

        public void SetFacing(Facing face)
        {
            this.facing = face;

            SetBodyPartFacing(LimbType.Torso, face);
            SetBodyPartFacing(LimbType.Head, face);
            SetBodyPartFacing(LimbType.LeftArm, face);
            SetBodyPartFacing(LimbType.RightArm, face);
            SetBodyPartFacing(LimbType.LeftLeg, face);
            SetBodyPartFacing(LimbType.RightLeg, face);
        }

        void SetBodyPartFacing(LimbType type, Facing face)
        {
            if (GetBodyPart(type) != null)
            {
                GetBodyPart(type).GetComponent<LimbVisual>().SetFacing(face);
            }
        }
    }
}
