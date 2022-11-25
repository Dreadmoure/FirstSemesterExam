using FirstSemesterExam.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FirstSemesterExam.PowerUps
{
    internal class Misile : GameObject
    {
        GameObject target;
        public Misile(Vector2 position, GameObject go, Texture2D sprite)
        {
            this.position = position;
            target = go;
            sprites = new Texture2D[1];
            sprites[0] = sprite;
            speed = 700;
            attackDamage = 20;
            velocity = new Vector2(1, 0);
        }
        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            if (target.ShouldBeRemoved == false)
            {
                velocity = target.GetPosition - position;
                velocity.Normalize();
                rotation = MathF.Atan2(velocity.Y, velocity.X);

            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                other.TakeDamage(attackDamage);
                shouldBeRemoved = true;
            }
        }

    }
}
