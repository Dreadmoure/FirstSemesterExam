using FirstSemesterExam.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FirstSemesterExam.PowerUps
{
    internal class PowerUpTK : GameObject
    {

        private Texture2D knifeSprite;
        private Player player;
        private Vector2 lastVelocity;
        private int tKAmount;


        protected float timeSinceLastAttack;

        public PowerUpTK(Player player)
        {
            this.player = player;
            attackSpeed = 2;
            attackDamage = 10;
            lastVelocity = new Vector2(1, 0);
            layerDepth = 0.6f;
            tKAmount = 1;
        }
        public override void LoadContent(ContentManager content)
        {
            knifeSprite = content.Load<Texture2D>("PowerUps\\knife");
        }

        public override void Update(GameTime gameTime)
        {
            Shoot(gameTime);
            if (player.GetVelocity != Vector2.Zero)
            {
                lastVelocity = player.GetVelocity; ;
            }


        }

        private void Shoot(GameTime gameTime)
        {

            timeSinceLastAttack += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastAttack > attackSpeed)
            {
                float angleOffset = (2 * MathF.PI) / tKAmount;
                float playerAngle = MathF.Atan2(lastVelocity.Y, lastVelocity.X);
                timeSinceLastAttack = 0;
                for (int i = 0; i < tKAmount; i++)
                {
                    float angle = playerAngle + (angleOffset * i);
                    ThrowingKnife throwingKnife = new ThrowingKnife(player.GetPosition, new Vector2(MathF.Cos(angle), MathF.Sin(angle)), attackDamage, knifeSprite);
                    GameState.InstantiateGameObject(throwingKnife);
                }
            }
        }

        public void UpdateTK()
        {

            switch (player.ThrowingKnifeLvl)
            {
                case 2:
                    tKAmount++;
                    break;
                case 3:
                    tKAmount++;
                    break;
                case 4:
                    tKAmount++;
                    break;
                case 5:
                    attackSpeed -= 0.5f ;
                    break;
                case 6:
                    tKAmount++;
                    break;

            }


        }
    }
}
