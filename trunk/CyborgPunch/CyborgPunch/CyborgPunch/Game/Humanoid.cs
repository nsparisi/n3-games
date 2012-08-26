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
        Blob torso;
        Blob head;
        Blob leftArm;
        Blob rightArm;
        Blob leftLeg;
        Blob rightLeg;

        private Blob[] bodyIndex;
        private Facing facing = Facing.Down;

        static Random rand = new Random();

        public float randomDepth = 0;

        public Humanoid()
            : base()
        {
            bodyIndex = new Blob[Enum.GetValues(typeof(LimbType)).Length];
        }

        public override void Start()
        {
            head = new Blob();
            head.transform.Parent = blob.transform;
            head.transform.Translate(0, 0);

            leftArm = new Blob();
            leftArm.transform.Parent = blob.transform;
            leftArm.transform.Translate(0, 35);

            rightArm = new Blob();
            rightArm.transform.Parent = blob.transform;
            rightArm.transform.Translate(0, 35);

            torso = new Blob();
            torso.transform.Parent = blob.transform;
            torso.transform.Translate(0, 29);

            leftLeg = new Blob();
            leftLeg.transform.Parent = blob.transform;
            leftLeg.transform.Translate(0, 55);

            rightLeg = new Blob();
            rightLeg.transform.Parent = blob.transform;
            rightLeg.transform.Translate(0, 55);

            randomDepth = (float)rand.NextDouble();

            //visuals
            SetupLimb(head, Limb.LimbComponentType.Head, Limb.LimbPosition.Left);
            SetupLimb(torso, Limb.LimbComponentType.Torso, Limb.LimbPosition.Left);
            SetupLimb(leftArm, Limb.LimbComponentType.Arm, Limb.LimbPosition.Left);
            SetupLimb(rightArm, Limb.LimbComponentType.Arm, Limb.LimbPosition.Right);
            SetupLimb(leftLeg, Limb.LimbComponentType.Leg, Limb.LimbPosition.Left);
            SetupLimb(rightLeg, Limb.LimbComponentType.Leg, Limb.LimbPosition.Right);
            
            SetBodyPart(LimbType.Head, head);
            SetBodyPart(LimbType.LeftArm, leftArm);
            SetBodyPart(LimbType.LeftLeg, leftLeg);
            SetBodyPart(LimbType.RightArm, rightArm);
            SetBodyPart(LimbType.RightLeg, rightLeg);
            SetBodyPart(LimbType.Torso, torso);


            Collider collider = new Collider();
            collider.bounds = new Rectangle(0, 0, 55, 86);
            blob.AddComponent(collider);
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
