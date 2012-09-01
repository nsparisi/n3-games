using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;
using CyborgPunch.Game.Enemies;
using Microsoft.Xna.Framework.Input;

namespace CyborgPunch.Game
{
    class TitleScreen : Component
    {
        Blob instructions;

        public static TitleScreen Instance { get; private set; }

        public TitleScreen()
        {
            Instance = this;
            
            instructions = new Blob();
            instructions.AddComponent(new Sprite(ResourceManager.Instructions));
            instructions.GetComponent<Sprite>().z = 0.1f;
            instructions.GetComponent<Sprite>().color = Color.White;
            lastState = Keyboard.GetState();
        }

        public bool KeyJustDown(Keys key, KeyboardState thisState, KeyboardState lastState)
        {
            return !lastState.IsKeyDown(key) && thisState.IsKeyDown(key);
        }

        KeyboardState lastState;
        public override void Update()
        {
            base.Update();

            KeyboardState thisState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ScoreManager.Instance.ResetScore();
                BlobManager.Instance.ResetRoot();
                Game1.Instance.GoToNewGame();
            }

            if (KeyJustDown(Keys.M, thisState, lastState))
            {
                SoundManager.TogglePause();
            }

            lastState = thisState;
        }
    }
}
