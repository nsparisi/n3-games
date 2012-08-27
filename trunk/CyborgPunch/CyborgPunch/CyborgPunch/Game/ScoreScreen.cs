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
        Label secondLabel;

        Label pressAgain;

        public static ScoreScreen Instance { get; private set; }

        public ScoreScreen()
        {
            Instance = this;

            Blob background = new Blob();
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
            pressAgain.text = "Press Enter To Play Again";
            pressAgainBlob.AddComponent(pressAgain);
            pressAgainBlob.transform.Position = background.transform.Position + new Vector2(-0, 100);

           /* Blob secondLabelBlob = new Blob();
            secondLabel = new Label();
            secondLabel.SetAlign(Label.AlignType.Left);
            secondLabel.color = Color.White;
            secondLabel.text = ScoreManager.Instance.Score.ToString();
            secondLabelBlob.AddComponent(secondLabel);
            secondLabelBlob.transform.Position = background.transform.Position + new Vector2(50, 30);
            * */
            
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

            timer += Time.deltaTime;
            if (timer < 3 && timer > 2)
            {
                pressAgain.color = Color.Yellow;
            }
            else if (timer < 4)
            {
                pressAgain.color = Color.Black;
            }
            else if(timer > 4)
            {
                timer -= 2;
            }
        }
    }
}
