using FirstSemesterExam.Enemies;
using FirstSemesterExam.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FirstSemesterExam.PowerUps
{
    internal class PowerUpMisile : GameObject
    {
        private Texture2D sprite;
        private float timeSinceLastAttack;
        private List<GameObject> go = new List<GameObject>();
        private Player player;
        public PowerUpMisile(Player player)
        {
            this.player = player;
            attackSpeed = 1;
            layerDepth = 0.6f;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("PowerUps\\magicMissile");
        }

        public override void Update(GameTime gameTime)
        {
            if (go != null)
            {
                Shoot(gameTime);
            }
        }

        private void Shoot(GameTime gameTime)
        {
            timeSinceLastAttack += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastAttack > attackSpeed)
            {
                timeSinceLastAttack = 0;
                GameObject gameObject = FindClosestEnemy();
                Misile misile = new Misile(player.GetPosition, gameObject, sprite);
                GameState.InstantiateGameObject(misile);
            }
        }

        private GameObject FindClosestEnemy()
        {
            go = GameState.enemies;
            GameObject enemy = null;
            float minDist = 999999996999;
            foreach (GameObject gameObject in go)
            {
                float distance = Vector2.Distance(gameObject.GetPosition, player.GetPosition);
                if(distance < minDist)
                {
                    enemy = gameObject;
                    minDist = distance;
                }
            }
            go.Clear();
            return enemy;
        }

        public void UpdateMisile()
        {

            switch (player.ThrowingKnifeLvl)
            {
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                case 4:
                    
                    break;
                case 5:
                    attackSpeed -= 0.5f;
                    break;
                case 6:
                    
                    break;
                case > 6:
                    
                    break;

            }
        }

    }
}
