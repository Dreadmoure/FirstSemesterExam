using FirstSemesterExam.Enemies;
using FirstSemesterExam.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam
{
    /// <summary>
    /// PlayerProjectile class, inherits from the superclass, Projectile
    /// this is the projectile the player shoots which its main gun
    /// </summary>
    public class PlayerProjectile : Projectile
    {
        #region Constructors
        /// <summary>
        /// Constructor for the Playerprojectile
        /// </summary>
        /// <param name="position">inherited from superclass, sets the initial position</param>
        /// <param name="velocity">sets the velocity</param>
        /// <param name="rotation">sets the rotation</param>
        /// <param name="attackDamage">sets the damage</param>
        public PlayerProjectile(Vector2 position, Vector2 velocity, float rotation, float attackDamage) : base(position)
        {
            this.velocity = velocity;
            this.position = position;
            this.rotation = rotation;
            this.attackDamage = attackDamage;
            speed = 1000;
            layerDepth = 0.6f;
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
            //if the projectile collides with an enemy, it passes on the attackDamage and removes the projectile
            if (other is Enemy)
            {
                
                other.TakeDamage(attackDamage);
                ShouldBeRemoved = true;
            }
        }
        #endregion
    }
}
