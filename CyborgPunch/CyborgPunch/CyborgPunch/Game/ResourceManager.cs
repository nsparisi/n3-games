using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace CyborgPunch.Game
{
    public enum LimbType { LeftArm, RightArm, RightLeg, LeftLeg, Head, Torso }
    public enum PartType { Human = 0, Robot = 1 }

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




        public static Texture2D texture_White;
        public static Texture2D texture_BG;

        public static SpriteFont font_Common;

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

            //additional
            texture_White = manager.Load<Texture2D>("Images//white");
            texture_BG = manager.Load<Texture2D>("Images/CrappyBG");
            font_Common = manager.Load<SpriteFont>("Font//Common");
        }
    }
}
