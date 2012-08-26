﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace CyborgPunch.Game
{
    public class KeyBindings
    {
        public static Keys MoveUp = Keys.W;
        public static Keys MoveDown = Keys.S;
        public static Keys MoveLeft = Keys.A;
        public static Keys MoveRight = Keys.D;

        public static Keys HeadAction = Keys.I;
        public static Keys ArmRightAction = Keys.J;
        public static Keys ArmLeftAction = Keys.L;
        public static Keys LegRightAction = Keys.M;
        public static Keys LegLeftAction = Keys.OemComma;

        public static Keys LimbChangeModifier = Keys.LeftShift;
        public static Keys LimbChangeAlternate = Keys.RightShift;
    }
}
