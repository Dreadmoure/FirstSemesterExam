using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FirstSemesterExam
{
    public abstract class GameObject
    {
        //stats
        protected float health;
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

        //Animation
        protected float animationSpeed = 10;
        private float animationTime;
        private int currentIndex = 0;

        protected bool shouldBeRemoved;

        public float GetAttackDamage
        {
            get { return attackDamage; }
        }

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
                return new Vector2(GetCurrentSprite.Width * scale, GetCurrentSprite.Height * scale);
            }
        }
        public Vector2 GetPosition 
        {
            get { return position; }
        }
        public float GetHealth 
        {
            get { return health; }
        }
        public bool ShouldBeRemoved 
        { 
            get { return shouldBeRemoved; } 
            set { shouldBeRemoved = value; } 
        }

        public int CurrentIndex { get => currentIndex; set => currentIndex = value; }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        protected void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += ((velocity * speed) * deltaTime);

        }

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
                spriteBatch.Draw(GetCurrentSprite, position, null, Color.White, rotation, origin, scale, spriteEffects, layerDepth);

            }
            

        }

        public bool IsColliding(GameObject other)
        {
            if (this == other)
                return false;

            return GetCollisionBox.Intersects(other.GetCollisionBox);
        }

        public virtual void TakeDamage(int damage)
        {

        }

        public virtual void OnCollision(GameObject other)
        {

        }



    }
}
