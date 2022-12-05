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
        private Player player;

        private float offset; // distance from player to ligtsaber
        private float angle;
        private float timeAlive; //The object removes itself after this time has expired.
        private float angleOffset; //offset to the angle if theres more than one ligtsaber
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
            //position based on angle, playersPosition and offset. Makes the ligtsaber fly around the player
            position = new Vector2(offset * MathF.Cos(angle + angleOffset) + player.GetPosition.X, offset * MathF.Sin(angle + angleOffset) + player.GetPosition.Y);

            if (timeAlive <= 0)
            {
                shouldBeRemoved = true;
            }
        }

        public override void OnCollision(GameObject other)
        {
            // damages the enemy and sets the bool CanBeDamagedByLs to false, so it can't damage the enemy for the next x time, so it doesn't damage it every frame.
            if (other is Enemy)
            {
                Enemy enemy = (Enemy)other;
                if (enemy.CanBeDamagedByLs)
                {
                    other.TakeDamage(attackDamage);
                    enemy.CanBeDamagedByLs = false;
                }
            }
            // if it collides with a EnemyProjectile it gets that object velocity, postion and rotation, and spawns a PlayerProjectile that has the opposite velocity.
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
