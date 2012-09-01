using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace CyborgPunch.Game
{
    public enum LimbType { LeftArm, RightArm, RightLeg, LeftLeg, Head, Torso }
    public enum PartTypes
    {
        GunLegRight = 0, GunLegLeft, RocketLegRight, RocketLegLeft,
        HookArmRight, HookArmLeft, HammerArmRight, HammerArmLeft, LongArmRight, LongArmLeft,
        BombHead, LaserHead, BiteHead, RightArm, LeftArm, RightLeg, LeftLeg, Head, Torso, RobotTorso
    }

    public class ResourceManager
    {
        public static Texture2D TEXTURE_HUMAN_UP_HEAD;
        public static Texture2D TEXTURE_HUMAN_UP_TORSO;
        public static Texture2D TEXTURE_HUMAN_UP_LEFTARM;
        public static Texture2D TEXTURE_HUMAN_UP_RIGHTARM;
        public static Texture2D TEXTURE_HUMAN_UP_LEFTLEG;
        public static Texture2D TEXTURE_HUMAN_UP_RIGHTLEG;


        public static Texture2D TEXTURE_HUMAN_DOWN_HEAD;
        public static Texture2D TEXTURE_HUMAN_DOWN_TORSO;
        public static Texture2D TEXTURE_HUMAN_DOWN_LEFTARM;
        public static Texture2D TEXTURE_HUMAN_DOWN_RIGHTARM;
        public static Texture2D TEXTURE_HUMAN_DOWN_LEFTLEG;
        public static Texture2D TEXTURE_HUMAN_DOWN_RIGHTLEG;

        public static Texture2D TEXTURE_HUMAN_LEFT_HEAD;
        public static Texture2D TEXTURE_HUMAN_LEFT_TORSO;
        public static Texture2D TEXTURE_HUMAN_LEFT_LEFTARM;
        public static Texture2D TEXTURE_HUMAN_LEFT_LEFTLEG;

        public static Texture2D TEXTURE_HUMAN_RIGHT_HEAD;
        public static Texture2D TEXTURE_HUMAN_RIGHT_TORSO;
        public static Texture2D TEXTURE_HUMAN_RIGHT_RIGHTARM;
        public static Texture2D TEXTURE_HUMAN_RIGHT_RIGHTLEG;


        /* ROBOT PARTS */

        /*  Bite Head */
        public static Texture2D TEXTURE_ROBOT_BITEHEAD_UP;
        public static Texture2D TEXTURE_ROBOT_BITEHEAD_DOWN;
        public static Texture2D TEXTURE_ROBOT_BITEHEAD_LEFT;
        public static Texture2D TEXTURE_ROBOT_BITEHEAD_RIGHT;

        /* Laser Head */
        public static Texture2D TEXTURE_ROBOT_LASERHEAD_UP;
        public static Texture2D TEXTURE_ROBOT_LASERHEAD_DOWN;
        public static Texture2D TEXTURE_ROBOT_LASERHEAD_LEFT;
        public static Texture2D TEXTURE_ROBOT_LASERHEAD_RIGHT;

        /* Bomb Head */
        public static Texture2D TEXTURE_ROBOT_BOMBHEAD_UP;
        public static Texture2D TEXTURE_ROBOT_BOMBHEAD_DOWN;
        public static Texture2D TEXTURE_ROBOT_BOMBHEAD_LEFT;
        public static Texture2D TEXTURE_ROBOT_BOMBHEAD_RIGHT;


        /* Torso */
        public static Texture2D TEXTURE_ROBOT_TORSO_UP;
        public static Texture2D TEXTURE_ROBOT_TORSO_DOWN;
        public static Texture2D TEXTURE_ROBOT_TORSO_LEFT;
        public static Texture2D TEXTURE_ROBOT_TORSO_RIGHT;


        /* Long Arm */
        public static Texture2D TEXTURE_ROBOT_LONGARM_LEFTARM_UP;
        public static Texture2D TEXTURE_ROBOT_LONGARM_RIGHTARM_UP;

        public static Texture2D TEXTURE_ROBOT_LONGARM_LEFTARM_DOWN;
        public static Texture2D TEXTURE_ROBOT_LONGARM_RIGHTARM_DOWN;

        public static Texture2D TEXTURE_ROBOT_LONGARM_LEFTARM_LEFT;
        
        public static Texture2D TEXTURE_ROBOT_LONGARM_RIGHTARM_RIGHT;


        /* Hammer Arm */
        public static Texture2D TEXTURE_ROBOT_HAMMERARM_LEFTARM_UP;
        public static Texture2D TEXTURE_ROBOT_HAMMERARM_RIGHTARM_UP;

        public static Texture2D TEXTURE_ROBOT_HAMMERARM_LEFTARM_DOWN;
        public static Texture2D TEXTURE_ROBOT_HAMMERARM_RIGHTARM_DOWN;

        public static Texture2D TEXTURE_ROBOT_HAMMERARM_LEFTARM_LEFT;

        public static Texture2D TEXTURE_ROBOT_HAMMERARM_RIGHTARM_RIGHT;


        /* Hook Arm */

        public static Texture2D TEXTURE_ROBOT_HOOKARM_LEFTARM_UP;
        public static Texture2D TEXTURE_ROBOT_HOOKARM_RIGHTARM_UP;

        public static Texture2D TEXTURE_ROBOT_HOOKARM_LEFTARM_DOWN;
        public static Texture2D TEXTURE_ROBOT_HOOKARM_RIGHTARM_DOWN;

        public static Texture2D TEXTURE_ROBOT_HOOKARM_LEFTARM_LEFT;

        public static Texture2D TEXTURE_ROBOT_HOOKARM_RIGHTARM_RIGHT;


        /* Rocket Leg */
        public static Texture2D TEXTURE_ROBOT_ROCKETLEG_LEFTLEG_UP;
        public static Texture2D TEXTURE_ROBOT_ROCKETLEG_RIGHTLEG_UP;

        public static Texture2D TEXTURE_ROBOT_ROCKETLEG_LEFTLEG_DOWN;
        public static Texture2D TEXTURE_ROBOT_ROCKETLEG_RIGHTLEG_DOWN;

        public static Texture2D TEXTURE_ROBOT_ROCKETLEG_LEFTLEG_LEFT;

        public static Texture2D TEXTURE_ROBOT_ROCKETLEG_RIGHTLEG_RIGHT;


        /* Gun Leg */
        public static Texture2D TEXTURE_ROBOT_GUNLEG_LEFTLEG_UP;
        public static Texture2D TEXTURE_ROBOT_GUNLEG_RIGHTLEG_UP;

        public static Texture2D TEXTURE_ROBOT_GUNLEG_LEFTLEG_DOWN;
        public static Texture2D TEXTURE_ROBOT_GUNLEG_RIGHTLEG_DOWN;

        public static Texture2D TEXTURE_ROBOT_GUNLEG_LEFTLEG_LEFT;

        public static Texture2D TEXTURE_ROBOT_GUNLEG_RIGHTLEG_RIGHT;

        public static Texture2D[][] partTextureLookup;

        public static Texture2D GetTextureFromPartAndFacing(Facing facing, PartTypes partType)
        {
            Texture2D chosenTexture = null;

            chosenTexture = partTextureLookup[(int)partType][(int)facing];

            return chosenTexture;
        }

        public static Texture2D laserBeam;
        public static Texture2D bullet;
        public static Texture2D explosion;
        public static Texture2D hitFlash;
        public static Texture2D texture_White;
        public static Texture2D texture_BG;

        public static Texture2D cloudBottom;
        public static Texture2D cloudMid;
        public static Texture2D cloudTop;
        public static Texture2D Stage;
        public static Texture2D DeadHead;
        public static Texture2D Instructions;
        public static Texture2D Blood;

        public static SpriteFont font_Common;

        private static ContentManager manager;

        private static int resourceCount = 0;
        private static Texture2D LoadTexture(string name)
        {
            Texture2D tex = manager.Load<Texture2D>(name);

            texToBoundsMap.Add(tex, boundsInOrder[resourceCount]);
            resourceCount++;

            //records the bounds to a string to output
            //RecordBoundsAsString(tex, name);

            return tex;
        }


        public static void LoadAll(ContentManager manager)
        {
            ResourceManager.manager = manager;
            LoadAllBounds();

            //WARNING DON'T MESS UP THE ORDER
            TEXTURE_HUMAN_UP_HEAD = LoadTexture("Images//Sprite//Human//Up_Head");
            TEXTURE_HUMAN_UP_TORSO = LoadTexture("Images//Sprite//Human//Up_Torso");
            TEXTURE_HUMAN_UP_LEFTARM = LoadTexture("Images//Sprite//Human//Up_LeftArm");
            TEXTURE_HUMAN_UP_RIGHTARM = LoadTexture("Images//Sprite//Human//Up_RightArm");
            TEXTURE_HUMAN_UP_LEFTLEG = LoadTexture("Images//Sprite//Human//Up_LeftLeg");
            TEXTURE_HUMAN_UP_RIGHTLEG = LoadTexture("Images//Sprite//Human//Up_RightLeg");

            TEXTURE_HUMAN_DOWN_HEAD = LoadTexture("Images//Sprite//Human//Down_Head");
            TEXTURE_HUMAN_DOWN_TORSO = LoadTexture("Images//Sprite//Human//Down_Torso");
            TEXTURE_HUMAN_DOWN_LEFTARM = LoadTexture("Images//Sprite//Human//Down_LeftArm");
            TEXTURE_HUMAN_DOWN_RIGHTARM = LoadTexture("Images//Sprite//Human//Down_RightArm");
            TEXTURE_HUMAN_DOWN_LEFTLEG = LoadTexture("Images//Sprite//Human//Down_LeftLeg");
            TEXTURE_HUMAN_DOWN_RIGHTLEG = LoadTexture("Images//Sprite//Human//Down_RightLeg");

            TEXTURE_HUMAN_LEFT_HEAD = LoadTexture("Images//Sprite//Human//Left_Head");
            TEXTURE_HUMAN_LEFT_TORSO = LoadTexture("Images//Sprite//Human//Left_Torso");
            TEXTURE_HUMAN_LEFT_LEFTARM = LoadTexture("Images//Sprite//Human//Left_LeftArm");
            TEXTURE_HUMAN_LEFT_LEFTLEG = LoadTexture("Images//Sprite//Human//Left_LeftLeg");

            TEXTURE_HUMAN_RIGHT_HEAD = LoadTexture("Images//Sprite//Human//Right_Head");
            TEXTURE_HUMAN_RIGHT_TORSO = LoadTexture("Images//Sprite//Human//Right_Torso");
            TEXTURE_HUMAN_RIGHT_RIGHTARM = LoadTexture("Images//Sprite//Human//Right_RightArm");
            TEXTURE_HUMAN_RIGHT_RIGHTLEG = LoadTexture("Images//Sprite//Human//Right_RightLeg");

            //WARNING DON'T MESS UP THE ORDER
            /* Heads */
            TEXTURE_ROBOT_BITEHEAD_UP = LoadTexture("Images//Sprite//Robot//BiteHead//BiteHead_Up");
            TEXTURE_ROBOT_BITEHEAD_DOWN = LoadTexture("Images//Sprite//Robot//BiteHead//BiteHead_Down");
            TEXTURE_ROBOT_BITEHEAD_LEFT = LoadTexture("Images//Sprite//Robot//BiteHead//BiteHead_Left");
            TEXTURE_ROBOT_BITEHEAD_RIGHT = LoadTexture("Images//Sprite//Robot//BiteHead//BiteHead_Right");

            TEXTURE_ROBOT_LASERHEAD_UP = LoadTexture("Images//Sprite//Robot//LaserHead//LaserHead_Up");
            TEXTURE_ROBOT_LASERHEAD_DOWN = LoadTexture("Images//Sprite//Robot//LaserHead//LaserHead_Down");
            TEXTURE_ROBOT_LASERHEAD_LEFT = LoadTexture("Images//Sprite//Robot//LaserHead//LaserHead_Left");
            TEXTURE_ROBOT_LASERHEAD_RIGHT = LoadTexture("Images//Sprite//Robot//LaserHead//LaserHead_Right");

            TEXTURE_ROBOT_BOMBHEAD_UP = LoadTexture("Images//Sprite//Robot//BombHead//BombHead_Up");
            TEXTURE_ROBOT_BOMBHEAD_DOWN = LoadTexture("Images//Sprite//Robot//BombHead//BombHead_Down");
            TEXTURE_ROBOT_BOMBHEAD_LEFT = LoadTexture("Images//Sprite//Robot//BombHead//BombHead_Left");
            TEXTURE_ROBOT_BOMBHEAD_RIGHT = LoadTexture("Images//Sprite//Robot//BombHead//BombHead_Right");

            
            TEXTURE_ROBOT_TORSO_UP = LoadTexture("Images//Sprite//Robot//Torso_Up");
            TEXTURE_ROBOT_TORSO_DOWN = LoadTexture("Images//Sprite//Robot//Torso_Down");
            TEXTURE_ROBOT_TORSO_LEFT = LoadTexture("Images//Sprite//Robot//Torso_Left");
            TEXTURE_ROBOT_TORSO_RIGHT = LoadTexture("Images//Sprite//Robot//Torso_Right");


            //WARNING DON'T MESS UP THE ORDER
            /* Long Arm */
            TEXTURE_ROBOT_LONGARM_LEFTARM_UP = LoadTexture("Images//Sprite//Robot//LongArm//LongArm_Up_LeftArm");
            TEXTURE_ROBOT_LONGARM_RIGHTARM_UP = LoadTexture("Images//Sprite//Robot//LongArm//LongArm_Up_RightArm");

            TEXTURE_ROBOT_LONGARM_LEFTARM_DOWN = LoadTexture("Images//Sprite//Robot//LongArm//LongArm_Down_LeftArm");
            TEXTURE_ROBOT_LONGARM_RIGHTARM_DOWN = LoadTexture("Images//Sprite//Robot//LongArm//LongArm_Down_RightArm");

            TEXTURE_ROBOT_LONGARM_LEFTARM_LEFT = LoadTexture("Images//Sprite//Robot//LongArm//LongArm_Left_LeftArm");

            TEXTURE_ROBOT_LONGARM_RIGHTARM_RIGHT = LoadTexture("Images//Sprite//Robot//LongArm//LongArm_Right_RightArm");


            //WARNING DON'T MESS UP THE ORDER
            /* Hammer Arm */
            TEXTURE_ROBOT_HAMMERARM_LEFTARM_UP = LoadTexture("Images//Sprite//Robot//HammerArm//HammerArm_Up_LeftArm");
            TEXTURE_ROBOT_HAMMERARM_RIGHTARM_UP = LoadTexture("Images//Sprite//Robot//HammerArm//HammerArm_Up_RightArm");

            TEXTURE_ROBOT_HAMMERARM_LEFTARM_DOWN = LoadTexture("Images//Sprite//Robot//HammerArm//HammerArm_Down_LeftArm");
            TEXTURE_ROBOT_HAMMERARM_RIGHTARM_DOWN = LoadTexture("Images//Sprite//Robot//HammerArm//HammerArm_Down_RightArm");

            TEXTURE_ROBOT_HAMMERARM_LEFTARM_LEFT = LoadTexture("Images//Sprite//Robot//HammerArm//HammerArm_Left_LeftArm");

            TEXTURE_ROBOT_HAMMERARM_RIGHTARM_RIGHT = LoadTexture("Images//Sprite//Robot//HammerArm//HammerArm_Right_RightArm");

            //WARNING DON'T MESS UP THE ORDER
            /* Hook Arm */
            TEXTURE_ROBOT_HOOKARM_LEFTARM_UP = LoadTexture("Images//Sprite//Robot//HookArm//HookArm_Up_LeftArm");
            TEXTURE_ROBOT_HOOKARM_RIGHTARM_UP = LoadTexture("Images//Sprite//Robot//HookArm//HookArm_Up_RightArm");

            TEXTURE_ROBOT_HOOKARM_LEFTARM_DOWN = LoadTexture("Images//Sprite//Robot//HookArm//HookArm_Down_LeftArm");
            TEXTURE_ROBOT_HOOKARM_RIGHTARM_DOWN = LoadTexture("Images//Sprite//Robot//HookArm//HookArm_Down_RightArm");

            TEXTURE_ROBOT_HOOKARM_LEFTARM_LEFT = LoadTexture("Images//Sprite//Robot//HookArm//HookArm_Left_LeftArm");

            TEXTURE_ROBOT_HOOKARM_RIGHTARM_RIGHT = LoadTexture("Images//Sprite//Robot//HookArm//HookArm_Right_RightArm");

            //WARNING DON'T MESS UP THE ORDER
            /* Rocket Leg */
            
            TEXTURE_ROBOT_ROCKETLEG_LEFTLEG_UP = LoadTexture("Images//Sprite//Robot//RocketLeg//RocketLeg_Up_LeftLeg");
            TEXTURE_ROBOT_ROCKETLEG_RIGHTLEG_UP = LoadTexture("Images//Sprite//Robot//RocketLeg//RocketLeg_Up_RightLeg");

            TEXTURE_ROBOT_ROCKETLEG_LEFTLEG_DOWN = LoadTexture("Images//Sprite//Robot//RocketLeg//RocketLeg_Down_LeftLeg");
            TEXTURE_ROBOT_ROCKETLEG_RIGHTLEG_DOWN = LoadTexture("Images//Sprite//Robot//RocketLeg//RocketLeg_Down_RightLeg");

            TEXTURE_ROBOT_ROCKETLEG_LEFTLEG_LEFT = LoadTexture("Images//Sprite//Robot//RocketLeg//RocketLeg_Left_LeftLeg");

            TEXTURE_ROBOT_ROCKETLEG_RIGHTLEG_RIGHT = LoadTexture("Images//Sprite//Robot//RocketLeg//RocketLeg_Right_RightLeg");

            //WARNING DON'T MESS UP THE ORDER
            /* Gun Leg */
            
            TEXTURE_ROBOT_GUNLEG_LEFTLEG_UP = LoadTexture("Images//Sprite//Robot//GunLeg//GunLeg_Up_LeftLeg");
            TEXTURE_ROBOT_GUNLEG_RIGHTLEG_UP = LoadTexture("Images//Sprite//Robot//GunLeg//GunLeg_Up_RightLeg");

            TEXTURE_ROBOT_GUNLEG_LEFTLEG_DOWN = LoadTexture("Images//Sprite//Robot//GunLeg//GunLeg_Down_LeftLeg");
            TEXTURE_ROBOT_GUNLEG_RIGHTLEG_DOWN = LoadTexture("Images//Sprite//Robot//GunLeg//GunLeg_Down_RightLeg");

            TEXTURE_ROBOT_GUNLEG_LEFTLEG_LEFT = LoadTexture("Images//Sprite//Robot//GunLeg//GunLeg_Left_LeftLeg");

            TEXTURE_ROBOT_GUNLEG_RIGHTLEG_RIGHT = LoadTexture("Images//Sprite//Robot//GunLeg//GunLeg_Right_RightLeg");


            //WARNING DON'T MESS UP THE ORDER
            //additional
            bullet = LoadTexture("Images//Sprite//Bullet");
            explosion = LoadTexture("Images//Sprite//Explosion");
            laserBeam = LoadTexture("Images//Sprite//LaserBeam");
            hitFlash = LoadTexture("Images//Sprite//HitFlash");
            texture_White = LoadTexture("Images//white");
            texture_BG = LoadTexture("Images/CrappyBG");
            cloudBottom = LoadTexture("Images/Clouds_BottomFast");
            cloudMid = LoadTexture("Images/Clouds_Middle_Medium");
            cloudTop = LoadTexture("Images/Clouds_Top_Small");
            Stage = LoadTexture("Images//Stage");
            DeadHead = LoadTexture("Images//Sprite/Human/Dead_Head");
            Instructions = LoadTexture("Images//Instructions");
            Blood = LoadTexture("Images//Sprite//BloodSplatter");

            font_Common = manager.Load<SpriteFont>("Font//Common");



            partTextureLookup = new Texture2D[Enum.GetValues(typeof(PartTypes)).Length][];
            for (int i = 0; i < partTextureLookup.Length; i++)
            {
                partTextureLookup[i] = new Texture2D[Enum.GetValues(typeof(Facing)).Length];
            }

            //heads
            partTextureLookup[(int)PartTypes.BiteHead][(int)Facing.Up] = TEXTURE_ROBOT_BITEHEAD_UP;
            partTextureLookup[(int)PartTypes.BiteHead][(int)Facing.Down] = TEXTURE_ROBOT_BITEHEAD_DOWN;
            partTextureLookup[(int)PartTypes.BiteHead][(int)Facing.Left] = TEXTURE_ROBOT_BITEHEAD_LEFT;
            partTextureLookup[(int)PartTypes.BiteHead][(int)Facing.Right] = TEXTURE_ROBOT_BITEHEAD_RIGHT;

            partTextureLookup[(int)PartTypes.BombHead][(int)Facing.Up] = TEXTURE_ROBOT_BOMBHEAD_UP;
            partTextureLookup[(int)PartTypes.BombHead][(int)Facing.Down] = TEXTURE_ROBOT_BOMBHEAD_DOWN;
            partTextureLookup[(int)PartTypes.BombHead][(int)Facing.Left] = TEXTURE_ROBOT_BOMBHEAD_LEFT;
            partTextureLookup[(int)PartTypes.BombHead][(int)Facing.Right] = TEXTURE_ROBOT_BOMBHEAD_RIGHT;

            partTextureLookup[(int)PartTypes.LaserHead][(int)Facing.Up] = TEXTURE_ROBOT_LASERHEAD_UP;
            partTextureLookup[(int)PartTypes.LaserHead][(int)Facing.Down] = TEXTURE_ROBOT_LASERHEAD_DOWN;
            partTextureLookup[(int)PartTypes.LaserHead][(int)Facing.Left] = TEXTURE_ROBOT_LASERHEAD_LEFT;
            partTextureLookup[(int)PartTypes.LaserHead][(int)Facing.Right] = TEXTURE_ROBOT_LASERHEAD_RIGHT;

            partTextureLookup[(int)PartTypes.Head][(int)Facing.Up] = TEXTURE_HUMAN_UP_HEAD;
            partTextureLookup[(int)PartTypes.Head][(int)Facing.Down] = TEXTURE_HUMAN_DOWN_HEAD;
            partTextureLookup[(int)PartTypes.Head][(int)Facing.Left] = TEXTURE_HUMAN_LEFT_HEAD;
            partTextureLookup[(int)PartTypes.Head][(int)Facing.Right] = TEXTURE_HUMAN_RIGHT_HEAD;

            //left arm
            partTextureLookup[(int)PartTypes.HammerArmLeft][(int)Facing.Up] = TEXTURE_ROBOT_HAMMERARM_LEFTARM_UP;
            partTextureLookup[(int)PartTypes.HammerArmLeft][(int)Facing.Down] = TEXTURE_ROBOT_HAMMERARM_LEFTARM_DOWN;
            partTextureLookup[(int)PartTypes.HammerArmLeft][(int)Facing.Left] = TEXTURE_ROBOT_HAMMERARM_LEFTARM_LEFT;
            partTextureLookup[(int)PartTypes.HammerArmLeft][(int)Facing.Right] = TEXTURE_ROBOT_HAMMERARM_LEFTARM_LEFT;

            partTextureLookup[(int)PartTypes.HookArmLeft][(int)Facing.Up] = TEXTURE_ROBOT_HOOKARM_LEFTARM_UP;
            partTextureLookup[(int)PartTypes.HookArmLeft][(int)Facing.Down] = TEXTURE_ROBOT_HOOKARM_LEFTARM_DOWN;
            partTextureLookup[(int)PartTypes.HookArmLeft][(int)Facing.Left] = TEXTURE_ROBOT_HOOKARM_LEFTARM_LEFT;
            partTextureLookup[(int)PartTypes.HookArmLeft][(int)Facing.Right] = TEXTURE_ROBOT_HOOKARM_LEFTARM_LEFT;

            partTextureLookup[(int)PartTypes.LongArmLeft][(int)Facing.Up] = TEXTURE_ROBOT_LONGARM_LEFTARM_UP;
            partTextureLookup[(int)PartTypes.LongArmLeft][(int)Facing.Down] = TEXTURE_ROBOT_LONGARM_LEFTARM_DOWN;
            partTextureLookup[(int)PartTypes.LongArmLeft][(int)Facing.Left] = TEXTURE_ROBOT_LONGARM_LEFTARM_LEFT;
            partTextureLookup[(int)PartTypes.LongArmLeft][(int)Facing.Right] = TEXTURE_ROBOT_LONGARM_LEFTARM_LEFT;

            partTextureLookup[(int)PartTypes.LeftArm][(int)Facing.Up] = TEXTURE_HUMAN_UP_LEFTARM;
            partTextureLookup[(int)PartTypes.LeftArm][(int)Facing.Down] = TEXTURE_HUMAN_DOWN_LEFTARM;
            partTextureLookup[(int)PartTypes.LeftArm][(int)Facing.Left] = TEXTURE_HUMAN_LEFT_LEFTARM;
            partTextureLookup[(int)PartTypes.LeftArm][(int)Facing.Right] = TEXTURE_HUMAN_LEFT_LEFTARM;

            //right arm
            partTextureLookup[(int)PartTypes.HammerArmRight][(int)Facing.Up] = TEXTURE_ROBOT_HAMMERARM_RIGHTARM_UP;
            partTextureLookup[(int)PartTypes.HammerArmRight][(int)Facing.Down] = TEXTURE_ROBOT_HAMMERARM_RIGHTARM_DOWN;
            partTextureLookup[(int)PartTypes.HammerArmRight][(int)Facing.Left] = TEXTURE_ROBOT_HAMMERARM_RIGHTARM_RIGHT;
            partTextureLookup[(int)PartTypes.HammerArmRight][(int)Facing.Right] = TEXTURE_ROBOT_HAMMERARM_RIGHTARM_RIGHT;

            partTextureLookup[(int)PartTypes.HookArmRight][(int)Facing.Up] = TEXTURE_ROBOT_HOOKARM_RIGHTARM_UP;
            partTextureLookup[(int)PartTypes.HookArmRight][(int)Facing.Down] = TEXTURE_ROBOT_HOOKARM_RIGHTARM_DOWN;
            partTextureLookup[(int)PartTypes.HookArmRight][(int)Facing.Left] = TEXTURE_ROBOT_HOOKARM_RIGHTARM_RIGHT;
            partTextureLookup[(int)PartTypes.HookArmRight][(int)Facing.Right] = TEXTURE_ROBOT_HOOKARM_RIGHTARM_RIGHT;

            partTextureLookup[(int)PartTypes.LongArmRight][(int)Facing.Up] = TEXTURE_ROBOT_LONGARM_RIGHTARM_UP;
            partTextureLookup[(int)PartTypes.LongArmRight][(int)Facing.Down] = TEXTURE_ROBOT_LONGARM_RIGHTARM_DOWN;
            partTextureLookup[(int)PartTypes.LongArmRight][(int)Facing.Left] = TEXTURE_ROBOT_LONGARM_RIGHTARM_RIGHT;
            partTextureLookup[(int)PartTypes.LongArmRight][(int)Facing.Right] = TEXTURE_ROBOT_LONGARM_RIGHTARM_RIGHT;

            partTextureLookup[(int)PartTypes.RightArm][(int)Facing.Up] = TEXTURE_HUMAN_UP_RIGHTARM;
            partTextureLookup[(int)PartTypes.RightArm][(int)Facing.Down] = TEXTURE_HUMAN_DOWN_RIGHTARM;
            partTextureLookup[(int)PartTypes.RightArm][(int)Facing.Left] = TEXTURE_HUMAN_RIGHT_RIGHTARM;
            partTextureLookup[(int)PartTypes.RightArm][(int)Facing.Right] = TEXTURE_HUMAN_RIGHT_RIGHTARM;

            //left leg
            partTextureLookup[(int)PartTypes.LeftLeg][(int)Facing.Up] = TEXTURE_HUMAN_UP_LEFTLEG;
            partTextureLookup[(int)PartTypes.LeftLeg][(int)Facing.Down] = TEXTURE_HUMAN_DOWN_LEFTLEG;
            partTextureLookup[(int)PartTypes.LeftLeg][(int)Facing.Left] = TEXTURE_HUMAN_LEFT_LEFTLEG;
            partTextureLookup[(int)PartTypes.LeftLeg][(int)Facing.Right] = TEXTURE_HUMAN_RIGHT_RIGHTLEG;

            partTextureLookup[(int)PartTypes.GunLegLeft][(int)Facing.Up] = TEXTURE_ROBOT_GUNLEG_LEFTLEG_UP;
            partTextureLookup[(int)PartTypes.GunLegLeft][(int)Facing.Down] = TEXTURE_ROBOT_GUNLEG_LEFTLEG_DOWN;
            partTextureLookup[(int)PartTypes.GunLegLeft][(int)Facing.Left] = TEXTURE_ROBOT_GUNLEG_LEFTLEG_LEFT;
            partTextureLookup[(int)PartTypes.GunLegLeft][(int)Facing.Right] = TEXTURE_ROBOT_GUNLEG_LEFTLEG_LEFT;

            partTextureLookup[(int)PartTypes.RocketLegLeft][(int)Facing.Up] = TEXTURE_ROBOT_ROCKETLEG_LEFTLEG_UP;
            partTextureLookup[(int)PartTypes.RocketLegLeft][(int)Facing.Down] = TEXTURE_ROBOT_ROCKETLEG_LEFTLEG_DOWN;
            partTextureLookup[(int)PartTypes.RocketLegLeft][(int)Facing.Left] = TEXTURE_ROBOT_ROCKETLEG_LEFTLEG_LEFT;
            partTextureLookup[(int)PartTypes.RocketLegLeft][(int)Facing.Right] = TEXTURE_ROBOT_ROCKETLEG_LEFTLEG_LEFT;

            //right leg
            partTextureLookup[(int)PartTypes.RightLeg][(int)Facing.Up] = TEXTURE_HUMAN_UP_RIGHTLEG;
            partTextureLookup[(int)PartTypes.RightLeg][(int)Facing.Down] = TEXTURE_HUMAN_DOWN_RIGHTLEG;
            partTextureLookup[(int)PartTypes.RightLeg][(int)Facing.Left] = TEXTURE_HUMAN_LEFT_LEFTLEG;
            partTextureLookup[(int)PartTypes.RightLeg][(int)Facing.Right] = TEXTURE_HUMAN_RIGHT_RIGHTLEG;

            partTextureLookup[(int)PartTypes.GunLegRight][(int)Facing.Up] = TEXTURE_ROBOT_GUNLEG_RIGHTLEG_UP;
            partTextureLookup[(int)PartTypes.GunLegRight][(int)Facing.Down] = TEXTURE_ROBOT_GUNLEG_RIGHTLEG_DOWN;
            partTextureLookup[(int)PartTypes.GunLegRight][(int)Facing.Left] = TEXTURE_ROBOT_GUNLEG_RIGHTLEG_RIGHT;
            partTextureLookup[(int)PartTypes.GunLegRight][(int)Facing.Right] = TEXTURE_ROBOT_GUNLEG_RIGHTLEG_RIGHT;

            partTextureLookup[(int)PartTypes.RocketLegRight][(int)Facing.Up] = TEXTURE_ROBOT_ROCKETLEG_RIGHTLEG_UP;
            partTextureLookup[(int)PartTypes.RocketLegRight][(int)Facing.Down] = TEXTURE_ROBOT_ROCKETLEG_RIGHTLEG_DOWN;
            partTextureLookup[(int)PartTypes.RocketLegRight][(int)Facing.Left] = TEXTURE_ROBOT_ROCKETLEG_RIGHTLEG_RIGHT;
            partTextureLookup[(int)PartTypes.RocketLegRight][(int)Facing.Right] = TEXTURE_ROBOT_ROCKETLEG_RIGHTLEG_RIGHT;

            //torso
            partTextureLookup[(int)PartTypes.Torso][(int)Facing.Up] = TEXTURE_HUMAN_UP_TORSO;
            partTextureLookup[(int)PartTypes.Torso][(int)Facing.Down] = TEXTURE_HUMAN_DOWN_TORSO;
            partTextureLookup[(int)PartTypes.Torso][(int)Facing.Left] = TEXTURE_HUMAN_LEFT_TORSO;
            partTextureLookup[(int)PartTypes.Torso][(int)Facing.Right] = TEXTURE_HUMAN_RIGHT_TORSO;

            partTextureLookup[(int)PartTypes.RobotTorso][(int)Facing.Up] = TEXTURE_ROBOT_TORSO_UP;
            partTextureLookup[(int)PartTypes.RobotTorso][(int)Facing.Down] = TEXTURE_ROBOT_TORSO_DOWN;
            partTextureLookup[(int)PartTypes.RobotTorso][(int)Facing.Left] = TEXTURE_ROBOT_TORSO_LEFT;
            partTextureLookup[(int)PartTypes.RobotTorso][(int)Facing.Right] = TEXTURE_ROBOT_TORSO_RIGHT;

            

            //File.WriteAllText("output.txt", outputText);
        }

        public static Rectangle GetBoundsHack(Texture2D tex)
        {
            Color[] data = new Color[tex.Width * tex.Height];
            tex.GetData<Color>(data);
            int left = 100000;
            int right = 0;
            int up = 1000000;
            int down = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].A > 0)
                {
                    int x = i % tex.Width;
                    int y = i / tex.Width;

                    left = Math.Min(left, x);
                    up = Math.Min(up, y);
                    down = Math.Max(down, y);
                    right = Math.Max(right, x);
                }
            }

            return new Rectangle(left, up, right - left, down - up);
        }


        private static Dictionary<Texture2D, Rectangle> texToBoundsMap;
        private static List<Rectangle> boundsInOrder;
        public static Rectangle GetBounds(Texture2D tex)
        {
            return texToBoundsMap[tex];
        }

        //autogenerating code
        static string outputText = "";
        static void RecordBoundsAsString(Texture2D tex, string name)
        {
            Rectangle bounds = GetBoundsHack(tex);
            //outputText += String.Format("texToBoundsMap.Add(\"{0}\", new Rectangle({1},{2},{3},{4}));", name, bounds.X, bounds.Y, bounds.Width, bounds.Height) + "\n";
            outputText += String.Format("boundsInOrder.Add(new Rectangle({0},{1},{2},{3}));", bounds.X, bounds.Y, bounds.Width, bounds.Height) + "\n";
        }

        //autogenerated
        static void LoadAllBounds()
        {
            texToBoundsMap = new Dictionary<Texture2D, Rectangle>();
            boundsInOrder = new List<Rectangle>();
            
            boundsInOrder.Add(new Rectangle(4, 0, 43, 44));
            boundsInOrder.Add(new Rectangle(9, 1, 31, 33));
            boundsInOrder.Add(new Rectangle(3, 0, 12, 26));
            boundsInOrder.Add(new Rectangle(34, 0, 12, 26));
            boundsInOrder.Add(new Rectangle(9, 1, 13, 19));
            boundsInOrder.Add(new Rectangle(27, 1, 13, 19));
            boundsInOrder.Add(new Rectangle(1, 0, 43, 48));
            boundsInOrder.Add(new Rectangle(9, 0, 31, 34));
            boundsInOrder.Add(new Rectangle(34, 0, 12, 26));
            boundsInOrder.Add(new Rectangle(3, 0, 12, 26));
            boundsInOrder.Add(new Rectangle(27, 1, 14, 20));
            boundsInOrder.Add(new Rectangle(8, 1, 14, 20));
            boundsInOrder.Add(new Rectangle(0, 0, 43, 46));
            boundsInOrder.Add(new Rectangle(9, 3, 29, 31));
            boundsInOrder.Add(new Rectangle(19, 4, 11, 26));
            boundsInOrder.Add(new Rectangle(16, 1, 14, 20));
            boundsInOrder.Add(new Rectangle(7, 0, 41, 44));
            boundsInOrder.Add(new Rectangle(11, 3, 29, 31));
            boundsInOrder.Add(new Rectangle(19, 4, 11, 26));
            boundsInOrder.Add(new Rectangle(19, 1, 14, 20));
            boundsInOrder.Add(new Rectangle(5, 10, 39, 36));
            boundsInOrder.Add(new Rectangle(5, 10, 38, 36));
            boundsInOrder.Add(new Rectangle(7, 10, 34, 36));
            boundsInOrder.Add(new Rectangle(7, 10, 34, 36));
            boundsInOrder.Add(new Rectangle(3, 10, 42, 36));
            boundsInOrder.Add(new Rectangle(3, 10, 42, 36));
            boundsInOrder.Add(new Rectangle(4, 10, 37, 36));
            boundsInOrder.Add(new Rectangle(7, 10, 37, 36));
            boundsInOrder.Add(new Rectangle(7, 2, 42, 44));
            boundsInOrder.Add(new Rectangle(0, 2, 41, 44));
            boundsInOrder.Add(new Rectangle(7, 6, 34, 40));
            boundsInOrder.Add(new Rectangle(7, 6, 34, 40));
            boundsInOrder.Add(new Rectangle(12, 5, 26, 28));
            boundsInOrder.Add(new Rectangle(12, 5, 26, 28));
            boundsInOrder.Add(new Rectangle(18, 5, 20, 28));
            boundsInOrder.Add(new Rectangle(11, 5, 20, 28));
            boundsInOrder.Add(new Rectangle(1, 6, 15, 28));
            boundsInOrder.Add(new Rectangle(34, 6, 14, 28));
            boundsInOrder.Add(new Rectangle(34, 6, 14, 28));
            boundsInOrder.Add(new Rectangle(1, 6, 15, 28));
            boundsInOrder.Add(new Rectangle(21, 6, 14, 28));
            boundsInOrder.Add(new Rectangle(14, 6, 14, 28));
            boundsInOrder.Add(new Rectangle(0, 6, 16, 26));
            boundsInOrder.Add(new Rectangle(34, 6, 15, 26));
            boundsInOrder.Add(new Rectangle(34, 6, 15, 26));
            boundsInOrder.Add(new Rectangle(0, 6, 16, 26));
            boundsInOrder.Add(new Rectangle(21, 6, 16, 26));
            boundsInOrder.Add(new Rectangle(13, 6, 15, 26));
            boundsInOrder.Add(new Rectangle(1, 6, 15, 26));
            boundsInOrder.Add(new Rectangle(34, 6, 14, 26));
            boundsInOrder.Add(new Rectangle(34, 6, 14, 26));
            boundsInOrder.Add(new Rectangle(1, 6, 15, 26));
            boundsInOrder.Add(new Rectangle(21, 6, 15, 26));
            boundsInOrder.Add(new Rectangle(14, 6, 14, 26));
            boundsInOrder.Add(new Rectangle(12, 0, 9, 21));
            boundsInOrder.Add(new Rectangle(28, 0, 10, 21));
            boundsInOrder.Add(new Rectangle(28, 0, 10, 21));
            boundsInOrder.Add(new Rectangle(12, 0, 9, 21));
            boundsInOrder.Add(new Rectangle(24, 0, 9, 21));
            boundsInOrder.Add(new Rectangle(16, 0, 11, 21));
            boundsInOrder.Add(new Rectangle(7, 0, 19, 21));
            boundsInOrder.Add(new Rectangle(24, 0, 18, 21));
            boundsInOrder.Add(new Rectangle(24, 0, 18, 21));
            boundsInOrder.Add(new Rectangle(7, 0, 19, 21));
            boundsInOrder.Add(new Rectangle(21, 0, 13, 21));
            boundsInOrder.Add(new Rectangle(17, 0, 8, 20));
            boundsInOrder.Add(new Rectangle(0, 0, 18, 21));
            boundsInOrder.Add(new Rectangle(0, 0, 76, 68));
            boundsInOrder.Add(new Rectangle(11, 0, 13, 35));
            boundsInOrder.Add(new Rectangle(0, 7, 48, 34));
            boundsInOrder.Add(new Rectangle(0, 0, 1, 1));
            boundsInOrder.Add(new Rectangle(0, 0, 1279, 767));
            boundsInOrder.Add(new Rectangle(0, 0, 1279, 431));
            boundsInOrder.Add(new Rectangle(0, 0, 1279, 363));
            boundsInOrder.Add(new Rectangle(0, 0, 1279, 129));
            boundsInOrder.Add(new Rectangle(0, 0, 1042, 616));
            boundsInOrder.Add(new Rectangle(1, 0, 43, 48));
            boundsInOrder.Add(new Rectangle(0, 0, 1279, 767));
            boundsInOrder.Add(new Rectangle(0, 0, 76, 68));

        }
    }
}
