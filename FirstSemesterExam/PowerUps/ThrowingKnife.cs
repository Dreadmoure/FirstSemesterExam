using FirstSemesterExam.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace FirstSemesterExam.PowerUps
{
    internal class ThrowingKnife : GameObject
    {
        public ThrowingKnife(Vector2 position, Vector2 velocity, float attackDamage , Texture2D sprite)
        {
            this.position = position;
            this.velocity = velocity;
            this.attackDamage = attackDamage;
            speed = 1000;
            sprites = new Texture2D[1];
            sprites[0] = sprite;
            rotation = MathF.Atan2(velocity.Y, velocity.X);
            layerDepth = 0.6f;
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            CheckIfOutsideBounds();

        }

        private void CheckIfOutsideBounds()
        {
            Vector2 min = new Vector2(GetSpriteSize.X / 2, GetSpriteSize.Y / 2);
            Vector2 max = new Vector2(GameWorld.GetScreenSize.X - GetSpriteSize.X / 2, GameWorld.GetScreenSize.Y - GetSpriteSize.Y / 2);
            if (position.X <= min.X || position.X >= max.X || position.Y <= min.Y || position.Y >= max.Y)
            {
                ShouldBeRemoved = true;
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                other.TakeDamage(attackDamage);
            }

        }
    }
}
