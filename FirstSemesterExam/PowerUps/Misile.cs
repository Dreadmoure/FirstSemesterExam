using FirstSemesterExam.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FirstSemesterExam.PowerUps
{
    /// <summary>
    /// Seeking misile that follows enemies and deals damage on collision.
    /// </summary>
    internal class Misile : GameObject
    {
        #region Fields
        private GameObject target;
        #endregion

        #region Constructors
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="position">position of the player</param>
        /// <param name="go">The gameobject that the misile will follow. the target</param>
        /// <param name="sprite">Gets the sprite from the PowerUpTK class</param>
        public Misile(Vector2 position, GameObject go, Texture2D sprite)
        {
            this.position = position;
            target = go;
            sprites = new Texture2D[1];
            sprites[0] = sprite;
            speed = 700;
            attackDamage = 20;
            velocity = new Vector2(1, 0);
            layerDepth = 0.6f;
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            CheckIfOutsideBounds();
            Move(gameTime);
            //Changes the velocity to always be the directeion from this to the target.
            if (target.ShouldBeRemoved == false && !target.Equals(null))
            {
                velocity = target.GetPosition - position;
                velocity.Normalize();
                rotation = MathF.Atan2(velocity.Y, velocity.X);

            }
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
                shouldBeRemoved = true;
            }
        }
        #endregion

    }
}
