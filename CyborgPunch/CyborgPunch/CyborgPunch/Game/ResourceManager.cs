using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace CyborgPunch.Game
{
    public enum LimbType { LeftArm, RightArm, RightLeg, LeftLeg, Head, Torso }
    public enum PartType { Human = 0, Robot = 1 }

    public class ResourceManager
    {

        public static List<Texture2D> sheetHeads;
        public static List<Texture2D> sheetLeftArms;
        public static List<Texture2D> sheetRightArms;
        public static List<Texture2D> sheetLeftLegs;
        public static List<Texture2D> sheetRightLegs;
        public static List<Texture2D> sheetTorsos;

        public static List<Texture2D> spriteHeads;
        public static List<Texture2D> spriteLeftArms;
        public static List<Texture2D> spriteRightArms;
        public static List<Texture2D> spriteLeftLegs;
        public static List<Texture2D> spriteRightLegs;
        public static List<Texture2D> spriteTorsos;

        public static Texture2D GetSpritePart(LimbType limb, PartType type)
        {
            if (limb == LimbType.Head)
            {
                return spriteHeads[(int)type];
            }
            else if (limb == LimbType.Torso)
            {
                return spriteTorsos[(int)type];
            }
            else if (limb == LimbType.LeftArm)
            {
                return spriteLeftArms[(int)type];
            }
            else if (limb == LimbType.LeftLeg)
            {
                return spriteLeftLegs[(int)type];
            }
            else if (limb == LimbType.RightArm)
            {
                return spriteRightArms[(int)type];
            }
            else if (limb == LimbType.RightLeg)
            {
                return spriteRightLegs[(int)type];
            }

            return null;
        }

        public static Texture2D GetSheetPart(LimbType limb, PartType type)
        {
            if (limb == LimbType.Head)
            {
                return sheetHeads[(int)type];
            }
            else if (limb == LimbType.Torso)
            {
                return sheetTorsos[(int)type];
            }
            else if (limb == LimbType.LeftArm)
            {
                return sheetLeftArms[(int)type];
            }
            else if (limb == LimbType.LeftLeg)
            {
                return sheetLeftLegs[(int)type];
            }
            else if (limb == LimbType.RightArm)
            {
                return sheetRightArms[(int)type];
            }
            else if (limb == LimbType.RightLeg)
            {
                return sheetRightLegs[(int)type];
            }

            return null;
        }

        public static Texture2D texture_White;
        public static Texture2D texture_BG;
        public static SpriteFont font_Common;

        public static void LoadAll(ContentManager manager)
        {
            //character sheet pieces
            sheetHeads = new List<Texture2D>();
            sheetHeads.Add(manager.Load<Texture2D>("Images//CharacterSheet//Head1"));

            sheetLeftArms = new List<Texture2D>();
            sheetLeftArms.Add(manager.Load<Texture2D>("Images//CharacterSheet//LeftArm1"));

            sheetRightArms = new List<Texture2D>();
            sheetRightArms.Add(manager.Load<Texture2D>("Images//CharacterSheet//RightArm1"));

            sheetLeftLegs = new List<Texture2D>();
            sheetLeftLegs.Add(manager.Load<Texture2D>("Images//CharacterSheet//LeftLeg1"));

            sheetRightLegs = new List<Texture2D>();
            sheetRightLegs.Add(manager.Load<Texture2D>("Images//CharacterSheet//RightLeg1"));

            sheetTorsos = new List<Texture2D>();
            sheetTorsos.Add(manager.Load<Texture2D>("Images//CharacterSheet//Torso1"));

            //sprite sprite pieces
            spriteHeads = new List<Texture2D>();
            spriteHeads.Add(manager.Load<Texture2D>("Images//Sprite//Human//head"));
            spriteHeads.Add(manager.Load<Texture2D>("Images//Sprite//Robo//BiteHead"));

            spriteLeftArms = new List<Texture2D>();
            spriteLeftArms.Add(manager.Load<Texture2D>("Images//Sprite//Human//LeftArm"));
            spriteLeftArms.Add(manager.Load<Texture2D>("Images//Sprite//Robo//LeftHookArm"));

            spriteRightArms = new List<Texture2D>();
            spriteRightArms.Add(manager.Load<Texture2D>("Images//Sprite//Human//RightArm"));
            spriteRightArms.Add(manager.Load<Texture2D>("Images//Sprite//Robo//RightHookArm"));

            spriteLeftLegs = new List<Texture2D>();
            spriteLeftLegs.Add(manager.Load<Texture2D>("Images//Sprite//Human//LeftFoot"));
            spriteLeftLegs.Add(manager.Load<Texture2D>("Images//Sprite//Robo//RoboLeftFoot"));

            spriteRightLegs = new List<Texture2D>();
            spriteRightLegs.Add(manager.Load<Texture2D>("Images//Sprite//Human//RightFoot"));
            spriteRightLegs.Add(manager.Load<Texture2D>("Images//Sprite//Robo//RoboRightFoot"));

            spriteTorsos = new List<Texture2D>();
            spriteTorsos.Add(manager.Load<Texture2D>("Images//Sprite//Human//Torso"));
            spriteTorsos.Add(manager.Load<Texture2D>("Images//Sprite//Robo//RoboTorso"));

            //additional
            texture_White = manager.Load<Texture2D>("Images//white");
            texture_BG = manager.Load<Texture2D>("Images/CrappyBG");
            font_Common = manager.Load<SpriteFont>("Font//Common");
        }
    }
}
