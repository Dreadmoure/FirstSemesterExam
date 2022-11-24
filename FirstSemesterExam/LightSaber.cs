using FirstSemesterExam.Enemies;
using FirstSemesterExam.Menu;
using FirstSemesterExam.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace FirstSemesterExam
{
    internal class LightSaber : GameObject
    {
        private float offset;
        private Player player;
        private float angle;
        public LightSaber(Player player)
        {
            this.player = player;
            offset = 150;
            speed = 2;
            attackDamage = 10;
            //originOffset = new Vector2(-15, 0);
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];
            sprites[0] = content.Load<Texture2D>("PowerUps\\lightSaber_Purple");
            
        }

        public override void Update(GameTime gameTime)
        {
            angle += (float)gameTime.ElapsedGameTime.TotalSeconds * speed;
            rotation = angle* 3;
            position = new Vector2(offset * MathF.Cos(angle) + player.GetPosition.X, offset * MathF.Sin(angle) + player.GetPosition.Y);
        }

        public override void OnCollisionEnter(GameObject other)
        {
            if (other is Enemy)
            {
                other.TakeDamage(attackDamage);
            }

            if (other is EnemyProjectile)
            {
                Vector2 _velocity = other.GetVelocity * -1;
                Vector2 _position = other.GetPosition;
                float _rotation = other.GetRotation;
                GameState.InstantiateGameObject(new PlayerProjectile(_position, _velocity, _rotation, 10));
                other.ShouldBeRemoved = true;


            }
        }

    }
}
