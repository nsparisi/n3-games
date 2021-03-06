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

        public static Keys HeadAction = Keys.O;
        public static Keys ArmRightAction = Keys.K;
        public static Keys ArmLeftAction = Keys.OemSemicolon;
        public static Keys LegRightAction = Keys.OemComma;
        public static Keys LegLeftAction = Keys.OemPeriod;

        public static Keys LimbChangeModifier = Keys.LeftShift;
        public static Keys LimbChangeAlternate = Keys.RightShift;


        public static Keys KeyBindingFromLimbType(LimbType type)
        {
            if (type == LimbType.Head)
                return HeadAction;

            if (type == LimbType.RightArm)
                return ArmRightAction;

            if (type == LimbType.LeftArm)
                return ArmLeftAction;

            if (type == LimbType.LeftLeg)
                return LegLeftAction;

            if (type == LimbType.RightLeg)
                return LegRightAction;

            return Keys.F24; 
        }
    }
}
