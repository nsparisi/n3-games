using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace CyborgPunch.Core
{
    public class SoundManager
    {

        public static void PlaySound(SoundEffect sound)
        {
            sound.Play();
        }

        public static void PlaySound(SoundEffect sound, float vol)
        {
            sound.Play(vol, 0f, 0);
        }

        public static void PlaySong(Song song)
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(song);
        }

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
        public static SoundEffect SFX_PLAYER_DIES_MAYBE;
        public static SoundEffect SFX_CHARGE_UP;
        public static SoundEffect SFX_EMPTY_GUN;
        public static SoundEffect SFX_SMASH_MEH;

        public static Song BGM_THEME;

        public static void LoadAll(ContentManager manager)
        {
            SFX_ATTACH_LIMB = manager.Load<SoundEffect>("Sounds//AttachLimb");
            SFX_BOMB_EXPLODE = manager.Load<SoundEffect>("Sounds//BombHeadExplode");
            SFX_ENEMY_DEATH = manager.Load<SoundEffect>("Sounds//EnemyDeath");
            SFX_GUN_LEG_SHOT = manager.Load<SoundEffect>("Sounds//GunLegShot");
            SFX_HIT_1 = manager.Load<SoundEffect>("Sounds//Hit1");
            SFX_HIT_2 = manager.Load<SoundEffect>("Sounds//Hit2");
            SFX_KICK = manager.Load<SoundEffect>("Sounds//Kick");
            SFX_LASER_FIRE = manager.Load<SoundEffect>("Sounds//LaserFire");
            SFX_PUNCH = manager.Load<SoundEffect>("Sounds//Punch");
            SFX_RIP_HUMAN_LIMB = manager.Load<SoundEffect>("Sounds//RipHumanLimb");
            SFX_ROCKET_BLAST = manager.Load<SoundEffect>("Sounds//RocketLegBlast");
            SFX_THROW_LIMB = manager.Load<SoundEffect>("Sounds//ThrowLimb");
            SFX_PLAYER_DIES_MAYBE = manager.Load<SoundEffect>("Sounds//PlayerDiesMaybe");
            SFX_SMASH_MEH = manager.Load<SoundEffect>("Sounds//SmashMeh");
            SFX_CHARGE_UP = manager.Load<SoundEffect>("Sounds//ChargeUp");
            SFX_EMPTY_GUN = manager.Load<SoundEffect>("Sounds//EmptyGun");

            BGM_THEME = manager.Load<Song>("Sounds//BGM");
        }
    }
}
