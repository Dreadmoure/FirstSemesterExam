using FirstSemesterExam.Enemies;
using FirstSemesterExam.Menu;
using FirstSemesterExam.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace FirstSemesterExam.PowerUps
{
    internal class LightSaber : GameObject
    {
        #region Fields
        private float offset;
        private Player player;
        private float angle;
        private float timeAlive;
        private float angleOffset;
        private bool canReflect;
        #endregion

        #region Constructors
        public LightSaber(Player player, float attackDamage, float timeAlive, float angleOffset, bool canReflect)
        {
            this.player = player;
            this.timeAlive = timeAlive;
            offset = 150;
            speed = 2;
            canReflect = false;
            this.attackDamage = attackDamage;

            layerDepth = 0.6f;
            this.angleOffset = angleOffset;
            this.canReflect = canReflect;
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];
            sprites[0] = content.Load<Texture2D>("PowerUps\\lightSaber_Purple");

        }

        public override void Update(GameTime gameTime)
        {
            angle += (float)gameTime.ElapsedGameTime.TotalSeconds * speed;
            timeAlive -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotation = angle * 3;
            position = new Vector2(offset * MathF.Cos(angle + angleOffset) + player.GetPosition.X, offset * MathF.Sin(angle + angleOffset) + player.GetPosition.Y);

            if (timeAlive <= 0)
            {
                shouldBeRemoved = true;
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                Enemy enemy = (Enemy)other;
                if (enemy.CanBeDamagedByLs)
                {
                    other.TakeDamage(attackDamage);
                    enemy.CanBeDamagedByLs = false;
                }
            }

            if (other is EnemyProjectile && canReflect)
            {
                Vector2 _velocity = other.GetVelocity * -1;
                Vector2 _position = other.GetPosition;
                float _rotation = other.GetRotation;
                GameState.InstantiateGameObject(new PlayerProjectile(_position, _velocity, _rotation, 10));
                other.ShouldBeRemoved = true;
            }
        }
        #endregion
    }
}
