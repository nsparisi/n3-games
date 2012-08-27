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
        public Blob secondLabel;

        public Blob blobShake;

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
            label.enabled = false;

            secondLabel = new Blob();
            secondLabel.AddComponent(new Label());
            secondLabel.transform.Position = new Vector2(300, 40);
            secondLabel.GetComponent<Label>().SetAlign(Label.AlignType.Right);
            secondLabel.enabled = false;

            dude = new Blob();
            Dude dudeComp = new Dude();
            dude.AddComponent(dudeComp);
            DudeMovement movement = new DudeMovement(dudeComp);
            dude.AddComponent(movement);
            dude.transform.Translate(Constants.GAME_WIDTH / 2 - 30, 550);

            blobShake = new Blob();
            blobShake.AddComponent(new Shake());

            Blob platform = new Blob();
            platform.AddComponent(new Sprite(ResourceManager.Stage));
            platform.GetComponent<Sprite>().z = 0.998f;
            platform.GetComponent<Sprite>().SetAnchor(Sprite.AnchorType.Middle_Center);
            platform.transform.Position = new Vector2(Constants.GAME_WIDTH / 2, Constants.GAME_HEIGHT / 2);

            //right
            Rectangle landingZone = new Rectangle(Constants.GAME_WIDTH - 290, 80, 10, 500);
            Blob enemySpawner = new Blob();
            enemySpawner.AddComponent(new EnemySpawner(landingZone));
            enemySpawner.transform.Position = new Vector2(Constants.GAME_WIDTH + 100, Constants.GAME_HEIGHT / 2);

            //left
            landingZone = new Rectangle(260, 80, 10, 500);
            enemySpawner = new Blob();
            enemySpawner.AddComponent(new EnemySpawner(landingZone));
            enemySpawner.transform.Position = new Vector2(-100, Constants.GAME_HEIGHT / 2);

            //top
            enemySpawner = new Blob();
            landingZone = new Rectangle(260, 80, 700, 10);
            enemySpawner.AddComponent(new EnemySpawner(landingZone));
            enemySpawner.transform.Position = new Vector2(Constants.GAME_WIDTH / 2, -120);
            enemySpawner.GetComponent<EnemySpawner>().SpawnEnemy();

            
            Blob background = new Blob();
            background.transform.Position = new Vector2(0, 0);
            background.AddComponent(new CloudRepeater(ResourceManager.texture_BG, -100, 1));

            Blob high = new Blob();
            high.transform.Position = new Vector2(0, 50);
            high.AddComponent(new CloudRepeater(ResourceManager.cloudTop, -80, 0.9999f));

            Blob mid = new Blob();
            mid.transform.Position = new Vector2(0, 140);
            mid.AddComponent(new CloudRepeater(ResourceManager.cloudMid, -100, 0.9995f));

            Blob bottom = new Blob();
            bottom.transform.Position = new Vector2(0, 380);
            bottom.AddComponent(new CloudRepeater(ResourceManager.cloudBottom, -180, 0.9990f));
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

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);


            //Rectangle landingZone = new Rectangle(Constants.GAME_WIDTH - 290, 80, 10, 500);
            //Gizmos.DrawRectangle(spriteBatch, landingZone);
            //landingZone = new Rectangle(260, 80, 10, 500);
            //landingZone = new Rectangle(260, 80, 700, 10);
            //Gizmos.DrawRectangle(spriteBatch, landingZone);

            //Rectangle r = new Rectangle(150, 100, Constants.GAME_WIDTH - 300, Constants.GAME_HEIGHT - 200);
        }

        public void SetSecondLabel(string thing)
        {
            secondLabel.GetComponent<Label>().text = thing;
        }

        float gutter = 50f;
        public bool InVisualBounds(Vector2 checkPosition)
        {
            return checkPosition.X > 0 - gutter && checkPosition.Y > 0 - gutter &&
                checkPosition.X < Constants.GAME_WIDTH + gutter && checkPosition.Y < Constants.GAME_HEIGHT + gutter;
        }

        public bool InVisualBounds(Rectangle rect)
        {
            return rect.Left > 150 &&
                rect.Top > 50 &&
                rect.Right < Constants.GAME_WIDTH - 150 &&
                rect.Top < Constants.GAME_HEIGHT - 100;
        }
    }
}
