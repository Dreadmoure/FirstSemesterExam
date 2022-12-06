using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.Projectiles
{
    /// <summary>
    /// abstract superclass for EnemyProjectile and PlayerProjectile
    /// </summary>
    public abstract class Projectile : GameObject
    {
        #region Fields
        protected float attackRange;
        private Vector2 initialAttackPosition;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for Projectile which takes a attackPosition as parameter
        /// attackPosition indicates where it shoots from
        /// </summary>
        /// <param name="attackPosition"></param>
        public Projectile(Vector2 attackPosition)
        {
            initialAttackPosition = attackPosition;
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            // projectile sprites are handled in the classes that inherits from Projectile 
        }

        public override void Update(GameTime gameTime)
        {
            CheckIfOutsideBounds();

            if (attackRange != 0)
            {
                CheckIfOutsideRange();
            }

            Move(gameTime);
        }

        /// <summary>
        /// Checks if the projectile is outside of bounds, if it is, then it will be removed
        /// </summary>
        private void CheckIfOutsideBounds()
        {
            //min and max values for the boundingbox
            Vector2 min = new Vector2(GetSpriteSize.X / 2, GetSpriteSize.Y / 2);
            Vector2 max = new Vector2(GameWorld.GetScreenSize.X - GetSpriteSize.X / 2, GameWorld.GetScreenSize.Y - GetSpriteSize.Y / 2);
            if (position.X <= min.X || position.X >= max.X || position.Y <= min.Y || position.Y >= max.Y)
            {
                ShouldBeRemoved = true;
            }
        }

        /// <summary>
        /// Checks to see if projectiles range is out of range, if it is, the projectile will be removed
        /// </summary>
        private void CheckIfOutsideRange()
        {
            Vector2 min = new Vector2(initialAttackPosition.X - GetSpriteSize.X / 2 - attackRange, initialAttackPosition.Y - GetSpriteSize.Y / 2 - attackRange);
            Vector2 max = new Vector2(initialAttackPosition.X + GetSpriteSize.X / 2 + attackRange, initialAttackPosition.Y + GetSpriteSize.Y / 2 + attackRange);
            if (position.X <= min.X || position.X >= max.X || position.Y <= min.Y || position.Y >= max.Y)
            {
                ShouldBeRemoved = true;
            }
        }
        #endregion
    }
}
