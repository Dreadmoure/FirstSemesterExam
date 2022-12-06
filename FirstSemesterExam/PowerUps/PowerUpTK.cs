using FirstSemesterExam.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FirstSemesterExam.PowerUps
{
    //TK = ThrowingKnife
    internal class PowerUpTK : GameObject
    {
        #region Fields
        private Texture2D knifeSprite;
        private Player player;
        private Vector2 lastVelocity;
        private int tKAmount;

        protected float timeSinceLastAttack;
        #endregion

        #region Constructors
        public PowerUpTK(Player player)
        {
            this.player = player;
            attackSpeed = 2;
            attackDamage = 5;
            lastVelocity = new Vector2(1, 0);
            layerDepth = 0.6f;
            tKAmount = 1;
        }
        #endregion

        #region Methods
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
            if (timeSinceLastAttack > attackSpeed - (attackSpeed * player.GetItemAttackCoolDown))
            {
                GameWorld.soundEffects[5].CreateInstance().Play();
                float angleOffset = (2 * MathF.PI) / tKAmount; // angleoffset based on the amount of TK's fired.
                float playerAngle = MathF.Atan2(lastVelocity.Y, lastVelocity.X); // gets the last direction the player moved in radians. It is the direction the TK will travel in
                timeSinceLastAttack = 0;
                for (int i = 0; i < tKAmount; i++)
                {
                    //The angle the TK wil travel in
                    float angle = playerAngle + (angleOffset * i);
                    ThrowingKnife throwingKnife = new ThrowingKnife(player.GetPosition, new Vector2(MathF.Cos(angle), MathF.Sin(angle)), attackDamage, knifeSprite);
                    GameState.InstantiateGameObject(throwingKnife);
                }
            }
        }
        //upgrades the throwing knife stats based on the level. Gets called on player each time the player picks the throwing knife power up.
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
                    attackDamage += 5;
                    break;
                case 5:
                    attackSpeed -= 0.5f ;
                    break;
                case 6:
                    tKAmount++;
                    break;
                case > 6:
                    tKAmount++;
                    break;

            }
        }
        #endregion
    }
}
