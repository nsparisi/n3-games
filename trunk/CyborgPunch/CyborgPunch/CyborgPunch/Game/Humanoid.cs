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

            Random rand = new Random();
            float iteration = 1f / (1024f * 1024f);
            float randomDepth = (float)rand.NextDouble();

            head = new Blob();
            Sprite headSprite = new Sprite(ResourceManager.GetSpritePart(LimbType.Head, PartType.Human));
            head.AddComponent(headSprite);
            head.transform.Parent = blob.transform;
            head.transform.Translate(0, 0);
            headSprite.z = randomDepth + iteration;
            randomDepth += iteration;

            leftArm = new Blob();
            Sprite laSprite = new Sprite(ResourceManager.GetSpritePart(LimbType.LeftArm, PartType.Human));
            leftArm.AddComponent(laSprite);
            leftArm.transform.Parent = blob.transform;
            leftArm.transform.Translate(7, 41);
            laSprite.z = randomDepth + iteration;
            randomDepth += iteration;

            rightArm = new Blob();
            Sprite raSprite = new Sprite(ResourceManager.GetSpritePart(LimbType.RightArm, PartType.Human));
            rightArm.AddComponent(raSprite);
            rightArm.transform.Parent = blob.transform;
            rightArm.transform.Translate(42, 41);
            raSprite.z = randomDepth + iteration;
            randomDepth += iteration;

            torso = new Blob();
            Sprite torsoSprite = new Sprite(ResourceManager.GetSpritePart(LimbType.Torso, PartType.Human));
            torso.AddComponent(torsoSprite);
            torso.transform.Parent = blob.transform;
            torso.transform.Translate(12, 34);
            torsoSprite.z = randomDepth + iteration;
            randomDepth += iteration;

            leftLeg = new Blob();
            Sprite llSprite = new Sprite(ResourceManager.GetSpritePart(LimbType.LeftLeg, PartType.Human));
            leftLeg.AddComponent(llSprite);
            leftLeg.transform.Parent = blob.transform;
            leftLeg.transform.Translate(17, 53);
            llSprite.z = randomDepth + iteration;
            randomDepth += iteration;

            rightLeg = new Blob();
            Sprite rlSprite = new Sprite(ResourceManager.GetSpritePart(LimbType.RightLeg, PartType.Human));
            rightLeg.AddComponent(rlSprite);
            rightLeg.transform.Parent = blob.transform;
            rightLeg.transform.Translate(33, 53);
            rlSprite.z = randomDepth + iteration;
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
    }
}