using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyborgPunch.Game
{
    public class ScoreManager
    {
        private static ScoreManager instance;
        public static ScoreManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new ScoreManager();
                }

                return instance; 
            }
        }

        public int Score { get; private set; }

        private ScoreManager()
        {
            Score = 0;
        }

        public void IncrementScore()
        {
            Score++;
        }

        public void ResetScore()
        {
            Score = 0;
        }

    }
}
