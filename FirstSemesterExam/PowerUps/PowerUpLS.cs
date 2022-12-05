using FirstSemesterExam.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.PowerUps
{
    internal class PowerUpLS : GameObject
    {
        private Player player;

        private float timeSinceLastAttack;
        private float timeAlive;
        private int lSAmount;
        public PowerUpLS (Player player)
        {
            this.player = player;
            attackDamage = 5;
            attackSpeed = 3;
            timeAlive = 3;
            lSAmount = 1;
            
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            SpawnLightsaber(gameTime);
        }

        private void SpawnLightsaber(GameTime gameTime)
        {

            timeSinceLastAttack += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastAttack > attackSpeed + timeAlive)
            {
                GameWorld.soundEffects[2].CreateInstance().Play();
                float angleOffset = (2 * MathF.PI) / lSAmount;
                timeSinceLastAttack = 0;
                for (int i = 0; i < lSAmount; i++)
                {
                    LightSaber lightSaber = new LightSaber(player, attackDamage, timeAlive, angleOffset * i );
                    GameState.InstantiateGameObject(lightSaber);
                }
            }
        }
        public void UpdateLightSaber()
        {

            switch (player.LightSaberLvl)
            {
                case 2:
                    timeAlive = 4f;
                    break;
                case 3:
                    attackDamage++; 
                    break;
                case 4:
                    timeAlive = 6;
                    break;
                case 5:
                    lSAmount = 2;
                    break;
                case 6:
                    attackSpeed = 2;
                    break;

            }
        }
    }
}
