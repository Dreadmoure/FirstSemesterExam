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
        #region Fields
        private Player player;

        private float timeSinceLastAttack;
        private float timeAlive;
        private int lSAmount;
        private bool canReflect;
        #endregion

        #region Constructors
        public PowerUpLS (Player player)
        {
            this.player = player;
            attackDamage = 5;
            attackSpeed = 3;
            timeAlive = 3;
            lSAmount = 1;
            timeSinceLastAttack = timeAlive;
            canReflect = false;
        }
        #endregion

        #region Methods
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
            if (timeSinceLastAttack > (attackSpeed - (attackSpeed * player.GetItemAttackCoolDown)) + timeAlive)
            {

                float angleOffset = (2 * MathF.PI) / lSAmount;
                timeSinceLastAttack = 0;
                for (int i = 0; i < lSAmount; i++)
                {
                    LightSaber lightSaber = new LightSaber(player, attackDamage, timeAlive, angleOffset * i, canReflect );
                    GameState.InstantiateGameObject(lightSaber);
                }
            }
        }
        public void UpdateLightSaber()
        {

            switch (player.LightSaberLvl)
            {
                case 2:
                    attackDamage += 5; 
                    break;
                case 3:
                    canReflect = true;
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
        #endregion
    }
}
