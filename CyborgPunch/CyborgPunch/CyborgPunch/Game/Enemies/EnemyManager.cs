using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CyborgPunch.Core;

namespace CyborgPunch.Game.Enemies
{
    public class EnemyManager
    {
        private static EnemyManager instance;
        public static EnemyManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new EnemyManager();
                }

                return instance; 
            }
        }

        private HashSet<int> blobIDs;
        private List<Enemy> enemies;

        private EnemyManager()
        {
            enemies = new List<Enemy>();
            blobIDs = new HashSet<int>();
        }

        public void RegisterEnemy(Enemy enemy)
        {
            if (blobIDs.Add(enemy.blob.ID))
            {
                enemies.Add(enemy);
            }
        }

        public void UnregisterEnemy(Enemy enemy)
        {
            if (blobIDs.Contains(enemy.blob.ID))
            {
                enemies.Remove(enemy);
                blobIDs.Remove(enemy.blob.ID);
            }
        }

        public List<Enemy> GetEnemies()
        {
            return enemies;
        }
    }
}
