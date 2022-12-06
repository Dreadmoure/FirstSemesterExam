using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FirstSemesterExam
{
    /// <summary>
    /// GameObject superclass.
    /// holds all the stats for the objects that inherits from gameobjects, which is almost all the objects in the game.
    /// the gameobject, can draw a sprite, animate the sprite, move the sprite, detect collision, take damage, and be removed.
    /// </summary>
    public abstract class GameObject
    {
        #region Fields
        //stats
        protected float health;
        protected float baseSpeed;
        protected float speed;
        protected float attackSpeed;
        protected float attackDamage;

        protected Vector2 position;
        protected Vector2 velocity;
        protected float rotation;
        
        //Sprite
        protected Texture2D[] sprites;
        protected float scale = 3f;
        protected float layerDepth;
        protected Vector2 originOffset = Vector2.Zero;
        protected SpriteEffects spriteEffects = SpriteEffects.None;
        protected Color color = Color.White;

        protected bool hasJustBeenHit;
        protected float hitTimer;

        //Animation
        protected float animationSpeed = 10;
        private float animationTime;
        private int currentIndex = 0;

        protected bool shouldBeRemoved;
        #endregion

        #region Properties
        public bool HasJustBeenHit { get => hasJustBeenHit; set => hasJustBeenHit = value; }


        public Rectangle GetCollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)(position.X - GetSpriteSize.X / 2),
                    (int)(position.Y - GetSpriteSize.Y / 2),
                    (int)GetSpriteSize.X,
                    (int)GetSpriteSize.Y
                );
            }
        }

        protected Texture2D GetCurrentSprite
        {
            get
            {
                return sprites[currentIndex];
            }
        }
        protected Vector2 GetSpriteSize
        {
            
            get
            {
                if (sprites != null)
                {
                    return new Vector2(GetCurrentSprite.Width * scale, GetCurrentSprite.Height * scale);
                }
                else
                {
                    return new Vector2(0,0);
                }
            }
        }
        public Vector2 GetPosition 
        {
            get { return position; }
        }
        public float Health 
        {
            get { return health; }
            set { health = value; }
        }
        public float GetRotation
        {
            get { return rotation; }
        }
        public Vector2 GetVelocity { get => velocity;}
        public bool ShouldBeRemoved 
        { 
            get { return shouldBeRemoved; } 
            set { shouldBeRemoved = value; } 
        }

        public int CurrentIndex { get => currentIndex; set => currentIndex = value; }
        #endregion

        #region Methods
        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        //Moves the gameobject based on the objects velocity and speed .Move gets called in update. 
        protected void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += ((velocity * speed) * deltaTime);

        }

        /// <summary>
        /// Choses the currentIndex, based on the animationTime. Animationtimes counts up to the amount of spirtes the sprites list and then resets.
        /// Current Index os used to chose the currentSprite
        /// </summary>
        protected void Animate(GameTime gameTime)
        {
            animationTime += (float)gameTime.ElapsedGameTime.TotalSeconds * animationSpeed;


            if (animationTime > sprites.Length )
            {
                animationTime = 0;
            }
            currentIndex = (int)animationTime;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (sprites != null)
            {

                Vector2 origin = new Vector2((GetCurrentSprite.Width / 2 ), GetCurrentSprite.Height / 2) + originOffset;
                spriteBatch.Draw(GetCurrentSprite, position, null, color, rotation, origin, scale, spriteEffects, layerDepth);

            }
            

        }
        /// <summary>
        /// Checks if this gameobjects collisionBox is intersecting with another collisionbox, and returns the other gameobject.
        /// </summary>
        /// <param name="other">the gameobject that this gameobject is currently collidning with</param>
        /// <returns></returns>
        public bool IsColliding(GameObject other)
        {
            if (this == other)
                return false;

            return GetCollisionBox.Intersects(other.GetCollisionBox);
        }
        /// <summary>
        /// subtracts the amount of damage dealth from health.
        /// </summary>
        /// <param name="damage">The amount of damage</param>
        public virtual void TakeDamage(float damage)
        {
            health -= damage;
        }
        /// <summary>
        /// Gets called when two gameobjects are colliding. gets called in Update in gamestate where it checks for all gameObjects.
        /// </summary>
        /// <param name="other">the gameobject that this gameobject is currently collidning with</param>
        public virtual void OnCollision(GameObject other)
        {

        }
        #endregion
    }
}
