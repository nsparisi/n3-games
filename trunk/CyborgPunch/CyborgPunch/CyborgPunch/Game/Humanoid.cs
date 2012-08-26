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


            Random rand = new Random();
            float iteration = 1f / (1024f * 1024f);
            float randomDepth = (float)rand.NextDouble();

            //visuals
            SetupLimb(head, Limb.LimbType.Head, Limb.LimbPosition.Left, randomDepth);
            SetupLimb(torso, Limb.LimbType.Torso, Limb.LimbPosition.Left, randomDepth);
            SetupLimb(leftArm, Limb.LimbType.Arm, Limb.LimbPosition.Left, randomDepth);
            SetupLimb(rightArm, Limb.LimbType.Arm, Limb.LimbPosition.Right, randomDepth);
            SetupLimb(leftLeg, Limb.LimbType.Leg, Limb.LimbPosition.Left, randomDepth);
            SetupLimb(rightLeg, Limb.LimbType.Leg, Limb.LimbPosition.Right, randomDepth);
            randomDepth += iteration;
            
            SetBodyPart(LimbType.Head, head);
            SetBodyPart(LimbType.LeftArm, leftArm);
            SetBodyPart(LimbType.LeftLeg, leftLeg);
            SetBodyPart(LimbType.RightArm, rightArm);
            SetBodyPart(LimbType.RightLeg, rightLeg);
            SetBodyPart(LimbType.Torso, torso);


            Collider collider = new Collider();
            collider.bounds = new Rectangle(0, 0, 55, 66);
            blob.AddComponent(collider);
        }

        void SetupLimb(Blob bodyPart, Limb.LimbType type, Limb.LimbPosition position, float z)
        {
            Limb visual = new Limb(type, position);
            bodyPart.AddComponent(visual);
            bodyPart.GetComponent<LimbVisual>().z = z;
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
