using FirstSemesterExam.Enemies;
using FirstSemesterExam.Menu;
using FirstSemesterExam.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;


namespace FirstSemesterExam.PowerUps
{
    /// <summary>
    /// Lightsaber class, inherits from the superclass GameObject
    /// And upgrade which spins around the player and damages enemies and reflects enemy projectiles
    /// </summary>
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
        /// <summary>
        /// Constructor for a lightsaber
        /// </summary>
        /// <param name="player">Gets the player object as it constantly needs the players position</param>
        /// <param name="attackDamage">Sets the attackDamage of the lightsaber</param>
        /// <param name="timeAlive">Time the lightsaber is alive before it is removed</param>
        /// <param name="angleOffset">The angle which the saber is offset, when you have more than 1, the 2nd it needs to be offset from the 1st </param>
        /// <param name="canReflect">Wether or not it can reflect projectiles</param>
        public LightSaber(Player player, float attackDamage, float timeAlive, float angleOffset, bool canReflect)
        {
            this.player = player;
            this.timeAlive = timeAlive;
            offset = 150;
            speed = 2;
            this.attackDamage = attackDamage;
            layerDepth = 0.6f;
            this.angleOffset = angleOffset;
            this.canReflect = canReflect;
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            //loads the sprite
            sprites = new Texture2D[1];
            sprites[0] = content.Load<Texture2D>("PowerUps\\lightSaber_Purple");

        }

        public override void Update(GameTime gameTime)
        {
            //Changes the angle based on time and speed
            angle += (float)gameTime.ElapsedGameTime.TotalSeconds * speed;
            //Direction vector based on angle and angle offset
            Vector2 dirVector = new Vector2(MathF.Cos(angle + angleOffset), MathF.Sin(angle + angleOffset));
            //position based on dirVector, playersPosition and offset. Makes the ligtsaber fly around the player
            position = offset * dirVector + player.GetPosition;
            //rotates 3 times faster around itself
            rotation = angle * 3;

            //removes the lightsaber when the timer reaches 0
            timeAlive -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeAlive <= 0)
            {
                shouldBeRemoved = true;
            }
        }

        public override void OnCollision(GameObject other)
        {
            // damages the enemy and sets the bool CanBeDamagedByLs to false
            // so it can't damage the enemy for the next x time, so it doesn't damage it every frame.
            if (other is Enemy)
            {
                Enemy enemy = (Enemy)other;
                if (enemy.CanBeDamagedByLs)
                {
                    other.TakeDamage(attackDamage);
                    enemy.CanBeDamagedByLs = false;
                }
            }
            // if it collides with a EnemyProjectile it gets that object velocity, postion and rotation
            // and spawns a PlayerProjectile that has the opposite velocity.
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
