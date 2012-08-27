using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Audio;

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

        public static SpriteFont font_Common;


        //sounds
        public static SoundEffect SFX_ATTACH_LIMB;
        public static SoundEffect SFX_BOMB_EXPLODE;
        public static SoundEffect SFX_ENEMY_DEATH;
        public static SoundEffect SFX_GUN_LEG_SHOT;
        public static SoundEffect SFX_HIT_1;
        public static SoundEffect SFX_HIT_2;
        public static SoundEffect SFX_KICK;
        public static SoundEffect SFX_LASER_FIRE;
        public static SoundEffect SFX_PUNCH;
        public static SoundEffect SFX_RIP_HUMAN_LIMB;
        public static SoundEffect SFX_ROCKET_BLAST;
        public static SoundEffect SFX_THROW_LIMB;

        public static void LoadAll(ContentManager manager)
        {
            TEXTURE_HUMAN_UP_HEAD = manager.Load<Texture2D>("Images//Sprite//Human//Up_Head");
            TEXTURE_HUMAN_UP_TORSO = manager.Load<Texture2D>("Images//Sprite//Human//Up_Torso");
            TEXTURE_HUMAN_UP_LEFTARM = manager.Load<Texture2D>("Images//Sprite//Human//Up_LeftArm");
            TEXTURE_HUMAN_UP_RIGHTARM = manager.Load<Texture2D>("Images//Sprite//Human//Up_RightArm");
            TEXTURE_HUMAN_UP_LEFTLEG = manager.Load<Texture2D>("Images//Sprite//Human//Up_LeftLeg");
            TEXTURE_HUMAN_UP_RIGHTLEG = manager.Load<Texture2D>("Images//Sprite//Human//Up_RightLeg");

            TEXTURE_HUMAN_DOWN_HEAD = manager.Load<Texture2D>("Images//Sprite//Human//Down_Head");
            TEXTURE_HUMAN_DOWN_TORSO = manager.Load<Texture2D>("Images//Sprite//Human//Down_Torso");
            TEXTURE_HUMAN_DOWN_LEFTARM = manager.Load<Texture2D>("Images//Sprite//Human//Down_LeftArm");
            TEXTURE_HUMAN_DOWN_RIGHTARM = manager.Load<Texture2D>("Images//Sprite//Human//Down_RightArm");
            TEXTURE_HUMAN_DOWN_LEFTLEG = manager.Load<Texture2D>("Images//Sprite//Human//Down_LeftLeg");
            TEXTURE_HUMAN_DOWN_RIGHTLEG = manager.Load<Texture2D>("Images//Sprite//Human//Down_RightLeg");

            TEXTURE_HUMAN_LEFT_HEAD = manager.Load<Texture2D>("Images//Sprite//Human//Left_Head");
            TEXTURE_HUMAN_LEFT_TORSO = manager.Load<Texture2D>("Images//Sprite//Human//Left_Torso");
            TEXTURE_HUMAN_LEFT_LEFTARM = manager.Load<Texture2D>("Images//Sprite//Human//Left_LeftArm");
            TEXTURE_HUMAN_LEFT_LEFTLEG = manager.Load<Texture2D>("Images//Sprite//Human//Left_LeftLeg");

            TEXTURE_HUMAN_RIGHT_HEAD = manager.Load<Texture2D>("Images//Sprite//Human//Right_Head");
            TEXTURE_HUMAN_RIGHT_TORSO = manager.Load<Texture2D>("Images//Sprite//Human//Right_Torso");
            TEXTURE_HUMAN_RIGHT_RIGHTARM = manager.Load<Texture2D>("Images//Sprite//Human//Right_RightArm");
            TEXTURE_HUMAN_RIGHT_RIGHTLEG = manager.Load<Texture2D>("Images//Sprite//Human//Right_RightLeg");

            /* Heads */
            TEXTURE_ROBOT_BITEHEAD_UP = manager.Load<Texture2D>("Images//Sprite//Robot//BiteHead//BiteHead_Up");
            TEXTURE_ROBOT_BITEHEAD_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//BiteHead//BiteHead_Down");
            TEXTURE_ROBOT_BITEHEAD_LEFT = manager.Load<Texture2D>("Images//Sprite//Robot//BiteHead//BiteHead_Left");
            TEXTURE_ROBOT_BITEHEAD_RIGHT = manager.Load<Texture2D>("Images//Sprite//Robot//BiteHead//BiteHead_Right");

            TEXTURE_ROBOT_LASERHEAD_UP = manager.Load<Texture2D>("Images//Sprite//Robot//LaserHead//LaserHead_Up");
            TEXTURE_ROBOT_LASERHEAD_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//LaserHead//LaserHead_Down");
            TEXTURE_ROBOT_LASERHEAD_LEFT = manager.Load<Texture2D>("Images//Sprite//Robot//LaserHead//LaserHead_Left");
            TEXTURE_ROBOT_LASERHEAD_RIGHT = manager.Load<Texture2D>("Images//Sprite//Robot//LaserHead//LaserHead_Right");

            TEXTURE_ROBOT_BOMBHEAD_UP = manager.Load<Texture2D>("Images//Sprite//Robot//BombHead//BombHead_Up");
            TEXTURE_ROBOT_BOMBHEAD_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//BombHead//BombHead_Down");
            TEXTURE_ROBOT_BOMBHEAD_LEFT = manager.Load<Texture2D>("Images//Sprite//Robot//BombHead//BombHead_Left");
            TEXTURE_ROBOT_BOMBHEAD_RIGHT = manager.Load<Texture2D>("Images//Sprite//Robot//BombHead//BombHead_Right");

            
            TEXTURE_ROBOT_TORSO_UP = manager.Load<Texture2D>("Images//Sprite//Robot//Torso_Up");
            TEXTURE_ROBOT_TORSO_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//Torso_Down");
            TEXTURE_ROBOT_TORSO_LEFT = manager.Load<Texture2D>("Images//Sprite//Robot//Torso_Left");
            TEXTURE_ROBOT_TORSO_RIGHT = manager.Load<Texture2D>("Images//Sprite//Robot//Torso_Right");
            

            /* Long Arm */
            TEXTURE_ROBOT_LONGARM_LEFTARM_UP = manager.Load<Texture2D>("Images//Sprite//Robot//LongArm//LongArm_Up_LeftArm");
            TEXTURE_ROBOT_LONGARM_RIGHTARM_UP = manager.Load<Texture2D>("Images//Sprite//Robot//LongArm//LongArm_Up_RightArm");

            TEXTURE_ROBOT_LONGARM_LEFTARM_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//LongArm//LongArm_Down_LeftArm");
            TEXTURE_ROBOT_LONGARM_RIGHTARM_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//LongArm//LongArm_Down_RightArm");

            TEXTURE_ROBOT_LONGARM_LEFTARM_LEFT = manager.Load<Texture2D>("Images//Sprite//Robot//LongArm//LongArm_Left_LeftArm");

            TEXTURE_ROBOT_LONGARM_RIGHTARM_RIGHT = manager.Load<Texture2D>("Images//Sprite//Robot//LongArm//LongArm_Right_RightArm");


            /* Hammer Arm */
            TEXTURE_ROBOT_HAMMERARM_LEFTARM_UP = manager.Load<Texture2D>("Images//Sprite//Robot//HammerArm//HammerArm_Up_LeftArm");
            TEXTURE_ROBOT_HAMMERARM_RIGHTARM_UP = manager.Load<Texture2D>("Images//Sprite//Robot//HammerArm//HammerArm_Up_RightArm");

            TEXTURE_ROBOT_HAMMERARM_LEFTARM_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//HammerArm//HammerArm_Down_LeftArm");
            TEXTURE_ROBOT_HAMMERARM_RIGHTARM_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//HammerArm//HammerArm_Down_RightArm");

            TEXTURE_ROBOT_HAMMERARM_LEFTARM_LEFT = manager.Load<Texture2D>("Images//Sprite//Robot//HammerArm//HammerArm_Left_LeftArm");

            TEXTURE_ROBOT_HAMMERARM_RIGHTARM_RIGHT = manager.Load<Texture2D>("Images//Sprite//Robot//HammerArm//HammerArm_Right_RightArm");

            /* Hook Arm */
            TEXTURE_ROBOT_HOOKARM_LEFTARM_UP = manager.Load<Texture2D>("Images//Sprite//Robot//HookArm//HookArm_Up_LeftArm");
            TEXTURE_ROBOT_HOOKARM_RIGHTARM_UP = manager.Load<Texture2D>("Images//Sprite//Robot//HookArm//HookArm_Up_RightArm");

            TEXTURE_ROBOT_HOOKARM_LEFTARM_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//HookArm//HookArm_Down_LeftArm");
            TEXTURE_ROBOT_HOOKARM_RIGHTARM_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//HookArm//HookArm_Down_RightArm");

            TEXTURE_ROBOT_HOOKARM_LEFTARM_LEFT = manager.Load<Texture2D>("Images//Sprite//Robot//HookArm//HookArm_Left_LeftArm");

            TEXTURE_ROBOT_HOOKARM_RIGHTARM_RIGHT = manager.Load<Texture2D>("Images//Sprite//Robot//HookArm//HookArm_Right_RightArm");

            /* Rocket Leg */
            
            TEXTURE_ROBOT_ROCKETLEG_LEFTLEG_UP = manager.Load<Texture2D>("Images//Sprite//Robot//RocketLeg//RocketLeg_Up_LeftLeg");
            TEXTURE_ROBOT_ROCKETLEG_RIGHTLEG_UP = manager.Load<Texture2D>("Images//Sprite//Robot//RocketLeg//RocketLeg_Up_RightLeg");

            TEXTURE_ROBOT_ROCKETLEG_LEFTLEG_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//RocketLeg//RocketLeg_Down_LeftLeg");
            TEXTURE_ROBOT_ROCKETLEG_RIGHTLEG_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//RocketLeg//RocketLeg_Down_RightLeg");

            TEXTURE_ROBOT_ROCKETLEG_LEFTLEG_LEFT = manager.Load<Texture2D>("Images//Sprite//Robot//RocketLeg//RocketLeg_Left_LeftLeg");

            TEXTURE_ROBOT_ROCKETLEG_RIGHTLEG_RIGHT = manager.Load<Texture2D>("Images//Sprite//Robot//RocketLeg//RocketLeg_Right_RightLeg");
            
            /* Gun Leg */
            
            TEXTURE_ROBOT_GUNLEG_LEFTLEG_UP = manager.Load<Texture2D>("Images//Sprite//Robot//GunLeg//GunLeg_Up_LeftLeg");
            TEXTURE_ROBOT_GUNLEG_RIGHTLEG_UP = manager.Load<Texture2D>("Images//Sprite//Robot//GunLeg//GunLeg_Up_RightLeg");

            TEXTURE_ROBOT_GUNLEG_LEFTLEG_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//GunLeg//GunLeg_Down_LeftLeg");
            TEXTURE_ROBOT_GUNLEG_RIGHTLEG_DOWN = manager.Load<Texture2D>("Images//Sprite//Robot//GunLeg//GunLeg_Down_RightLeg");

            TEXTURE_ROBOT_GUNLEG_LEFTLEG_LEFT = manager.Load<Texture2D>("Images//Sprite//Robot//GunLeg//GunLeg_Left_LeftLeg");

            TEXTURE_ROBOT_GUNLEG_RIGHTLEG_RIGHT = manager.Load<Texture2D>("Images//Sprite//Robot//GunLeg//GunLeg_Right_RightLeg");
            

            //additional
            bullet = manager.Load<Texture2D>("Images//Sprite//Bullet");
            explosion = manager.Load<Texture2D>("Images//Sprite//Explosion");
            laserBeam = manager.Load<Texture2D>("Images//Sprite//LaserBeam");
            hitFlash = manager.Load<Texture2D>("Images//Sprite//HitFlash");
            texture_White = manager.Load<Texture2D>("Images//white");
            texture_BG = manager.Load<Texture2D>("Images/CrappyBG");
            cloudBottom = manager.Load<Texture2D>("Images/Clouds_BottomFast");
            cloudMid = manager.Load<Texture2D>("Images/Clouds_Middle_Medium");
            cloudTop = manager.Load<Texture2D>("Images/Clouds_Top_Small");
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
        }

        public static Rectangle GetBounds(Texture2D tex)
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
    }
}
