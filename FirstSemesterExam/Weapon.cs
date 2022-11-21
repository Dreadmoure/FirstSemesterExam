using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FirstSemesterExam
{
    internal class Weapon : GameObject
    {
        Player player;
        Texture2D sprite;
        Texture2D spriteFlipped;
        float angle;
        public Weapon(Player player)
        {
            this.player = player;
            scale = 5;
            //originOffset = new Vector2(-11, 0);
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
            position = new Vector2(100 * MathF.Cos(angle) + player.Position.X, 100 * MathF.Sin(angle) + player.Position.Y );
            rotation = angle;
            Flip();
            
        }

        protected void Flip()
        {
            if (rotation > (MathF.PI / 2) && rotation < (3 * Math.PI) / 2)
            {
                sprites[0] = spriteFlipped;
            }
            else
            {
                sprites[0] = sprite;
            }
        }

    }
}
