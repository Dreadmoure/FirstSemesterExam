using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FirstSemesterExam
{
    internal class Weapon : GameObject
    {
        protected Player player;
        protected Texture2D sprite;
        protected Texture2D spriteFlipped;
        protected float angle;
        protected float offset;
        protected Vector2 dirVector;
        protected Vector2 shootingPos;
        protected float shootingPosOffset;
        protected float fireRate = 0.2f;
        protected float timeSinceFire;
        public Weapon(Player player)
        {
            this.player = player;
            scale = 3;

            offset = 30;
            shootingPosOffset = 10;
            layerDepth = 0.51f;

        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];
            sprite = content.Load<Texture2D>("Weapons\\testGun");
            sprites[0] = sprite;
            //originOffset = new Vector2(0, 2);
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceFire += (float)gameTime.ElapsedGameTime.TotalSeconds;
            angle = player.MouseAngle();
            position = new Vector2(offset * MathF.Cos(angle) + player.Position.X, offset * MathF.Sin(angle) + player.Position.Y );
            shootingPos = new Vector2((offset + shootingPosOffset) * MathF.Cos(angle) + player.Position.X, (offset + shootingPosOffset) * MathF.Sin(angle) + player.Position.Y );

            dirVector = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
            //dirVector.Normalize();
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

    }
}
