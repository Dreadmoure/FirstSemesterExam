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
        #region Fields
        private Texture2D sprite;
        private float timeSinceLastAttack;
        private Player player;
        #endregion

        #region Constructors
        public PowerUpMisile(Player player)
        {
            this.player = player;
            attackSpeed = 1;
            layerDepth = 0.6f;
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("PowerUps\\magicMissile");
        }

        public override void Update(GameTime gameTime)
        {
            if (GameState.enemies.Count > 0)
            {
                Shoot(gameTime);
            }
        }

        private void Shoot(GameTime gameTime)
        {
            timeSinceLastAttack += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastAttack > attackSpeed - (attackSpeed * player.GetItemAttackCoolDown))
            {
                GameWorld.soundEffects[4].CreateInstance().Play();
                timeSinceLastAttack = 0;
                GameObject gameObject = FindClosestEnemy();
                Misile misile = new Misile(player.GetPosition, gameObject, sprite);
                GameState.InstantiateGameObject(misile);
            }
        }

        //Finds the closest enemy by looping through all enenies alive, and comparing the distance from the player to the enemy.
        private GameObject FindClosestEnemy()
        {

            GameObject enemy = null;
            float minDist = 999999996999; // this just needed to be a very high number
            foreach (GameObject gameObject in GameState.enemies)
            {
                float distance = Vector2.Distance(gameObject.GetPosition, player.GetPosition);
                if(distance < minDist)
                {
                    enemy = gameObject;
                    minDist = distance;
                }
            }

            return enemy;
        }

        public void UpdateMisile()
        {

            switch (player.MagicMissileLvl)
            {
                case 2:
                    attackSpeed -= 0.1f;
                    break;
                case 3:
                    attackSpeed -= 0.1f;
                    break;
                case 4:
                    attackSpeed -= 0.1f;
                    break;
                case 5:
                    attackSpeed -= 0.1f;
                    break;
                case 6:
                    attackSpeed -= 0.1f;
                    break;
                case > 6:
                    attackSpeed -= 0.1f;
                    break;

            }
        }
        #endregion
    }
}
