using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;

namespace CyborgPunch.Game
{
    class CharacterSheet : Component
    {

        Blob torso;
        Blob head;
        Blob leftArm;
        Blob rightArm;
        Blob leftLeg;
        Blob rightLeg;

        public CharacterSheet() : base()
        {
        }

        public override void Start()
        {
            /*
            torso = new Blob();
            Sprite torsoSprite = new Sprite(ResourceManager.GetSheetPart(LimbType.Torso, PartType.Human));
            torsoSprite.SetAnchor(Sprite.AnchorType.Middle_Center);
            torso.AddComponent(torsoSprite);
            torso.transform.Parent = blob.transform;
            torso.transform.Translate(0, 0);

            head = new Blob();
            Sprite headSprite = new Sprite(ResourceManager.GetSheetPart(LimbType.Head, PartType.Human));
            head.AddComponent(headSprite);
            headSprite.SetAnchor(Sprite.AnchorType.Bottom_Center);
            head.transform.Parent = blob.transform;
            head.transform.Translate(-3, -46);

            leftArm = new Blob();
            Sprite laSprite = new Sprite(ResourceManager.GetSheetPart(LimbType.LeftArm, PartType.Human));
            leftArm.AddComponent(laSprite);
            laSprite.SetAnchor(Sprite.AnchorType.Upper_Right);
            leftArm.transform.Parent = blob.transform;
            leftArm.transform.Translate(-25, -52);

            rightArm = new Blob();
            Sprite raSprite = new Sprite(ResourceManager.GetSheetPart(LimbType.RightArm, PartType.Human));
            rightArm.AddComponent(raSprite);
            raSprite.SetAnchor(Sprite.AnchorType.Upper_Left);
            rightArm.transform.Parent = blob.transform;
            rightArm.transform.Translate(25, -52);

            leftLeg = new Blob();
            Sprite llSprite = new Sprite(ResourceManager.GetSheetPart(LimbType.LeftLeg, PartType.Human));
            leftLeg.AddComponent(llSprite);
            llSprite.SetAnchor(Sprite.AnchorType.Upper_Right);
            leftLeg.transform.Parent = blob.transform;
            leftLeg.transform.Translate(-00, 50);

            rightLeg = new Blob();
            Sprite rlSprite = new Sprite(ResourceManager.GetSheetPart(LimbType.RightLeg, PartType.Human));
            rightLeg.AddComponent(rlSprite);
            rlSprite.SetAnchor(Sprite.AnchorType.Upper_Left);
            rightLeg.transform.Parent = blob.transform;
            rightLeg.transform.Translate(00, 50);
            */
        }

        public override void Update()
        {
            base.Update();

            //head.GetComponent<Sprite>().rotation += 0.01f;

        }
    }
}
