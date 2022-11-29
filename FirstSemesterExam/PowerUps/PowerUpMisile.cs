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
                go = GameState.enemies;
                Misile misile = new Misile(player.GetPosition ,go[0], sprite);
                GameState.InstantiateGameObject(misile);
            }
        }

    }
}
