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


        protected float timeSinceLastAttack;

        public PowerUpTK(Player player)
        {
            this.player = player;
            attackSpeed = 2;
            lastVelocity = new Vector2(1, 0);
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

                timeSinceLastAttack = 0;
                ThrowingKnife throwingKnife = new ThrowingKnife(player.GetPosition, lastVelocity, knifeSprite);
                GameState.InstantiateGameObject(throwingKnife);
            }
        }

    }
}
