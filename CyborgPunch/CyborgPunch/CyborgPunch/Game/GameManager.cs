using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;
using CyborgPunch.Game.Enemies;

namespace CyborgPunch.Game
{
    class GameManager : Component
    {
        public Blob characterSheet;
        public Blob dude;
        public Blob enemySpawner;
        public Blob secondLabel;

        Blob label;

        public static GameManager Instance { get; private set; }

        public GameManager()
        {
            Instance = this;

            characterSheet = new Blob();
            CharacterSheet characterComp = new CharacterSheet();
            characterSheet.AddComponent(characterComp);
            characterSheet.transform.Translate(80, 150);

            label = new Blob();
            label.AddComponent(new Label());
            label.transform.Position = new Vector2(300, 20);
            label.GetComponent<Label>().SetAlign(Label.AlignType.Right);

            secondLabel = new Blob();
            secondLabel.AddComponent(new Label());
            secondLabel.transform.Position = new Vector2(300, 40);
            secondLabel.GetComponent<Label>().SetAlign(Label.AlignType.Right);

            dude = new Blob();
            Dude dudeComp = new Dude();
            dude.AddComponent(dudeComp);
            DudeMovement movement = new DudeMovement();
            dude.AddComponent(movement);
            dude.transform.Translate(400, 400);

            //Blob background = new Blob();
            //background.AddComponent(new Sprite(ResourceManager.texture_BG));
            //background.GetComponent<Sprite>().z = 1;

            enemySpawner = new Blob();
            enemySpawner.AddComponent(new EnemySpawner());
        }

        float timer;
        int counts = 0;
        public override void Update()
        {
            base.Update();

            timer += Time.deltaTime;
            counts++;
            if (timer >= 0.5f)
            {
                float fps = ((float)counts) / timer;
                timer -= 0.5f;
                counts = 0;
                label.GetComponent<Label>().text = fps.ToString("00.0");
            }
        }

        public void SetSecondLabel(string thing)
        {
            secondLabel.GetComponent<Label>().text = thing;
        }

        public bool InVisualBounds(Vector2 checkPosition)
        {
            return checkPosition.X > 0 && checkPosition.Y > 0 && checkPosition.X < Constants.GAME_WIDTH && checkPosition.Y < Constants.GAME_HEIGHT;
        }
    }
}
