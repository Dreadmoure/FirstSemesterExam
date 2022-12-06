using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.Projectiles
{
    /// <summary>
    /// EnemyProjectile class, inherits from the superclass Projectile.
    /// this is the projectile the enemies shoots
    /// </summary>
    public class EnemyProjectile : Projectile
    {
        #region Constructors
        /// <summary>
        /// Constructor for the EnemyProjectile
        /// </summary>
        /// <param name="enemyPosition">inherited from the superclass, sets the position from where the projectile spawns/param>
        /// <param name="playerPosition">sets the target position, where the shot needs to go towards</param>
        /// <param name="enemyAttackRange">sets the enemies attack range</param>
        public EnemyProjectile(Vector2 enemyPosition, Vector2 playerPosition, float enemyAttackRange) : base(enemyPosition)
        {
            position = enemyPosition;
            speed = 1000f;
            attackDamage = 5f;
            attackRange = enemyAttackRange;
            layerDepth = 0.6f;
            

            // sets the angle of the projectile 
            float angle = MathF.Atan2(playerPosition.Y - enemyPosition.Y, playerPosition.X - enemyPosition.X);
            // converts it to a vector
            velocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
            rotation = angle; 
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            //loads the sprite
            sprites = new Texture2D[1];
            sprites[0] = content.Load<Texture2D>("Projectiles\\Projectile1");
        }

        public override void OnCollision(GameObject other)
        {
            //if the projectile collides with the player
            //calls the takedamage method and passes on the attackdamage as a parameter
            //then removes the projectile
            if (other is Player)
            {
                other.TakeDamage(attackDamage);
                
                ShouldBeRemoved = true;
            }
        }
        #endregion
    }
}
