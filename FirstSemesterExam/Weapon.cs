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
        public Weapon(Player player)
        {
            this.player = player;
            scale = 3;
            offset = 50;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];
            sprite = content.Load<Texture2D>("Weapons\\testGun");
            spriteFlipped = content.Load<Texture2D>("Weapons\\testGunFlipped");
            sprites[0] = sprite;
        }

        public override void Update(GameTime gameTime)
        {
            angle = player.MouseAngle();
            position = new Vector2(offset * MathF.Cos(angle) + player.Position.X, offset * MathF.Sin(angle) + player.Position.Y);
            shootingPos = new Vector2((offset + shootingPosOffset) * MathF.Cos(angle) + player.Position.X, (offset + shootingPosOffset) * MathF.Sin(angle) + player.Position.Y);

            dirVector = position;
            dirVector.Normalize();
            rotation = angle;
            Flip();

        }

        public virtual void Shoot()
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
