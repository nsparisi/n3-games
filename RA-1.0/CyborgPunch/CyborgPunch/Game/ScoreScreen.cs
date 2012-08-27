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
    class ScoreScreen : Component
    {
        Label label;
        Label pressAgain;
        Blob background;

        Blob instructions;

        public static ScoreScreen Instance { get; private set; }

        public ScoreScreen()
        {
            Instance = this;

            background = new Blob();
            background.AddComponent(new Sprite(ResourceManager.DeadHead));
            background.GetComponent<Sprite>().z = 1;
            background.GetComponent<Sprite>().color = Color.White;
            background.GetComponent<Sprite>().SetAnchor(Sprite.AnchorType.Middle_Center);
            background.transform.Translate(Constants.GAME_WIDTH / 2, Constants.GAME_HEIGHT / 2);

            Blob labelblob = new Blob();
            label = new Label();
            label.SetAlign(Label.AlignType.Center);
            label.color = Color.White;
            label.text = "Score: " + ScoreManager.Instance.Score.ToString();
            labelblob.AddComponent(label);
            labelblob.transform.Position = background.transform.Position + new Vector2(-0, 30);

            Blob pressAgainBlob = new Blob();
            pressAgain = new Label();
            pressAgain.SetAlign(Label.AlignType.Center);
            pressAgain.color = Color.Black;
            pressAgain.text = "Press Enter To Play Again\n   Press 'F1' For Help.";
            pressAgainBlob.AddComponent(pressAgain);
            pressAgainBlob.transform.Position = background.transform.Position + new Vector2(-0, 100);

            instructions = new Blob();
            instructions.AddComponent(new Sprite(ResourceManager.Instructions));
            instructions.GetComponent<Sprite>().z = 0.1f;
            instructions.GetComponent<Sprite>().color = Color.White;
            instructions.enabled = false;
        }

        void GoToInstructions()
        {
            label.enabled = false;
            pressAgain.enabled = false;
            background.enabled = false;

            instructions.enabled = true;
        }

        float timer = 0;
        public override void Update()
        {
            base.Update();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ScoreManager.Instance.ResetScore();
                BlobManager.Instance.ResetRoot();
                Game1.Instance.GoToNewGame();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                GoToInstructions();
            }

            timer += Time.deltaTime;
            if (timer < 2 && timer > 1)
            {
                pressAgain.color = Color.Yellow;
            }
            else if (timer < 3)
            {
                pressAgain.color = Color.Black;
            }
            else if(timer > 3)
            {
                timer -= 2;
            }
        }
    }
}
