using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyborgPunch.Game
{
    static class DamageValues
    {
        public const int enemyHealth = 5;
        public const float enemySpeed = 70f;

        public const int throwDamage = enemyHealth;

        public const int robotMeleeAmmo = 5;
        public const int robotGunAmmo = 20;
        public const int robotLaserAmmo = 2;

        public const int humanMelee = 1;
        public const int humanPiercing = 2;

        public const int robotMelee = 3;
        public const int robotPiercing = -1;

        public const int gunLegPiercing = 1;

        public const float rocketKick = 5f;
    }
}
