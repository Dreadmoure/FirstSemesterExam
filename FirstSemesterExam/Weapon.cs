using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FirstSemesterExam
{
    internal class Weapon : GameObject
    {
        #region Fields
        protected Player player;
        protected Texture2D sprite;
        protected float angle;
        protected float offset;
        protected Vector2 dirVector;
        protected Vector2 shootingPos;
        protected float shootingPosOffset;
        protected float fireRate = 0.2f;
        protected float timeSinceFire;
        #endregion

        #region Constructors
        public Weapon(Player player)
        {
            this.player = player;

            offset = 30;
            shootingPosOffset = 10;
            layerDepth = 0.51f;

        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];
            sprite = content.Load<Texture2D>("Weapons\\testGun");
            sprites[0] = sprite;
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceFire += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //angle from player to mouse.
            angle = player.MouseAngle();
            //The position of the weapon based on the mouse angle, player pos and offset. the shootinPos ís the same but it has an extra offset.
            position = new Vector2(offset * MathF.Cos(angle) + player.GetPosition.X, offset * MathF.Sin(angle) + player.GetPosition.Y );
            shootingPos = new Vector2((offset + shootingPosOffset) * MathF.Cos(angle) + player.GetPosition.X, (offset + shootingPosOffset) * MathF.Sin(angle) + player.GetPosition.Y );
            //Calcurlates the direction vector from the player to mouse, based on the angle.
            dirVector = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
            rotation = angle;
            Flip();

        }

        public virtual void Shoot(GameTime gameTime)
        {

        }

        protected void Flip()
        {
            if (rotation > (MathF.PI / 2) && rotation < (3 * Math.PI) / 2)
            {
                spriteEffects = SpriteEffects.FlipVertically;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }
        }
        #endregion
    }
}
