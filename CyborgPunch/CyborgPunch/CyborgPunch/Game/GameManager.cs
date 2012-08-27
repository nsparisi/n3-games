﻿using System;
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

            secondLabel = new Blob();
            secondLabel.AddComponent(new Label());
            secondLabel.transform.Position = new Vector2(300, 40);
            secondLabel.GetComponent<Label>().SetAlign(Label.AlignType.Right);

            dude = new Blob();
            Dude dudeComp = new Dude();
            dude.AddComponent(dudeComp);
            DudeMovement movement = new DudeMovement(dudeComp);
            dude.AddComponent(movement);
            dude.transform.Translate(Constants.GAME_WIDTH / 2 - 30, 550);

            blobShake = new Blob();
            blobShake.AddComponent(new Shake());

            Blob background = new Blob();
            background.AddComponent(new Sprite(ResourceManager.texture_BG));
            background.GetComponent<Sprite>().z = 1;

            Blob enemySpawner = new Blob();
            enemySpawner.AddComponent(new EnemySpawner());
            enemySpawner.transform.Position = new Vector2(Constants.GAME_WIDTH + 100, Constants.GAME_HEIGHT / 2);

            enemySpawner = new Blob();
            enemySpawner.AddComponent(new EnemySpawner());
            enemySpawner.transform.Position = new Vector2(-100, Constants.GAME_HEIGHT / 2);

            enemySpawner = new Blob();
            enemySpawner.AddComponent(new EnemySpawner());
            enemySpawner.transform.Position = new Vector2(Constants.GAME_WIDTH / 2, -120);
            enemySpawner.GetComponent<EnemySpawner>().SpawnEnemy();

            Blob high = new Blob();
            high.transform.Position = new Vector2(0, 0);
            high.AddComponent(new CloudRepeater(ResourceManager.cloudTop, -5, 0.9999f));

            Blob mid = new Blob();
            mid.transform.Position = new Vector2(0, 90);
            mid.AddComponent(new CloudRepeater(ResourceManager.cloudMid, -10, 0.9995f));

            Blob bottom = new Blob();
            bottom.transform.Position = new Vector2(0, 330);
            bottom.AddComponent(new CloudRepeater(ResourceManager.cloudBottom, -15, 0.9990f));
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

        float gutter = 50f;
        public bool InVisualBounds(Vector2 checkPosition)
        {
            return checkPosition.X > 0 - gutter && checkPosition.Y > 0 - gutter &&
                checkPosition.X < Constants.GAME_WIDTH + gutter && checkPosition.Y < Constants.GAME_HEIGHT + gutter;
        }

        public bool InVisualBounds(Rectangle rect)
        {
            return rect.Left > 0 && rect.Top > 0 && rect.Right < Constants.GAME_WIDTH && rect.Top < Constants.GAME_HEIGHT;
        }
    }
}