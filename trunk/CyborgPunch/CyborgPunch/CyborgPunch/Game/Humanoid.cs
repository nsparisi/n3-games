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

            Random rand = new Random();
            float iteration = 1f / (1024f * 1024f);
            float randomDepth = (float)rand.NextDouble();

            head = new Blob();
            Sprite headSprite = new Sprite(ResourceManager.TEXTURE_HUMAN_DOWN_HEAD);
            head.AddComponent(headSprite);
            head.transform.Parent = blob.transform;
            head.transform.Translate(0, 0);
            headSprite.z = randomDepth + iteration;
            randomDepth += iteration;

            leftArm = new Blob();
            Sprite laSprite = new Sprite(ResourceManager.TEXTURE_HUMAN_DOWN_LEFTARM);
            leftArm.AddComponent(laSprite);
            leftArm.transform.Parent = blob.transform;
            leftArm.transform.Translate(0, 35);
            laSprite.z = randomDepth + iteration;
            randomDepth += iteration;

            rightArm = new Blob();
            Sprite raSprite = new Sprite(ResourceManager.TEXTURE_HUMAN_DOWN_RIGHTARM);
            rightArm.AddComponent(raSprite);
            rightArm.transform.Parent = blob.transform;
            rightArm.transform.Translate(0, 35);
            raSprite.z = randomDepth + iteration;
            randomDepth += iteration;

            torso = new Blob();
            Sprite torsoSprite = new Sprite(ResourceManager.TEXTURE_HUMAN_DOWN_TORSO);
            torso.AddComponent(torsoSprite);
            torso.transform.Parent = blob.transform;
            torso.transform.Translate(0, 29);
            torsoSprite.z = randomDepth + iteration;
            randomDepth += iteration;

            leftLeg = new Blob();
            Sprite llSprite = new Sprite(ResourceManager.TEXTURE_HUMAN_DOWN_LEFTLEG);
            leftLeg.AddComponent(llSprite);
            leftLeg.transform.Parent = blob.transform;
            leftLeg.transform.Translate(0, 55);
            llSprite.z = randomDepth + iteration;
            randomDepth += iteration;

            rightLeg = new Blob();
            Sprite rlSprite = new Sprite(ResourceManager.TEXTURE_HUMAN_DOWN_RIGHTLEG);
            rightLeg.AddComponent(rlSprite);
            rightLeg.transform.Parent = blob.transform;
            rightLeg.transform.Translate(0, 55);
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

        public void SetFacing(Facing face)
        {
            this.facing = face;

            Texture2D texTorso = null;
            Texture2D texHead = null;
            Texture2D texLeftArm = null;
            Texture2D texRightArm = null;
            Texture2D texLeftLeg = null;
            Texture2D texRightLeg = null;

            if (face == Facing.Down)
            {
                texTorso = ResourceManager.TEXTURE_HUMAN_DOWN_TORSO;
                texHead = ResourceManager.TEXTURE_HUMAN_DOWN_HEAD;
                texLeftArm = ResourceManager.TEXTURE_HUMAN_DOWN_LEFTARM;
                texRightArm = ResourceManager.TEXTURE_HUMAN_DOWN_RIGHTARM;
                texLeftLeg = ResourceManager.TEXTURE_HUMAN_DOWN_LEFTLEG;
                texRightLeg = ResourceManager.TEXTURE_HUMAN_DOWN_RIGHTLEG;
            }
            else if (face == Facing.Up)
            {
                texTorso = ResourceManager.TEXTURE_HUMAN_UP_TORSO;
                texHead = ResourceManager.TEXTURE_HUMAN_UP_HEAD;
                texLeftArm = ResourceManager.TEXTURE_HUMAN_UP_LEFTARM;
                texRightArm = ResourceManager.TEXTURE_HUMAN_UP_RIGHTARM;
                texLeftLeg = ResourceManager.TEXTURE_HUMAN_UP_LEFTLEG;
                texRightLeg = ResourceManager.TEXTURE_HUMAN_UP_RIGHTLEG;
            }
            else if (face == Facing.Left)
            {
                texTorso = ResourceManager.TEXTURE_HUMAN_LEFT_TORSO;
                texHead = ResourceManager.TEXTURE_HUMAN_LEFT_HEAD;
                texLeftArm = ResourceManager.TEXTURE_HUMAN_LEFT_LEFTARM;
                texRightArm = null;
                texLeftLeg = ResourceManager.TEXTURE_HUMAN_LEFT_LEFTLEG;
                texRightLeg = null;
            }
            else if (face == Facing.Right)
            {
                texTorso = ResourceManager.TEXTURE_HUMAN_RIGHT_TORSO;
                texHead = ResourceManager.TEXTURE_HUMAN_RIGHT_HEAD;
                texLeftArm = null;
                texRightArm = ResourceManager.TEXTURE_HUMAN_RIGHT_RIGHTARM;
                texLeftLeg = null;
                texRightLeg = ResourceManager.TEXTURE_HUMAN_RIGHT_RIGHTLEG;
            }

            SetBodyPartTexture(LimbType.Torso, texTorso);
            SetBodyPartTexture(LimbType.Head, texHead);
            SetBodyPartTexture(LimbType.LeftArm, texLeftArm);
            SetBodyPartTexture(LimbType.RightArm, texRightArm);
            SetBodyPartTexture(LimbType.LeftLeg, texLeftLeg);
            SetBodyPartTexture(LimbType.RightLeg, texRightLeg);
        }

        void SetBodyPartTexture(LimbType type, Texture2D tex)
        {
            if (GetBodyPart(type) != null)
            {
                GetBodyPart(type).GetComponent<Sprite>().texture = tex;
            }
        }
    }
}
