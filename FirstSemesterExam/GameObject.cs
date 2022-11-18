using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FirstSemesterExam
{
    public abstract class GameObject
    {
        //stats
        protected int health;
        protected float speed;
        protected float attackSpeed; 
        protected Vector2 position;
        protected Vector2 velocity;
        protected float rotation;
        protected float attackDamage;

        //Sprite
        protected Texture2D[] sprites;
        protected float scale = 2f;
        protected float layerDeph;

        //Animation
        protected float animationSpeed = 10;
        private float animationTime;
        private int currentIndex = 0;

        protected bool shouldBeRemoved;

        public float GetAttackDamage
        {
            get { return attackDamage; }
        }

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)(position.X - SpriteSize.X / 2),
                    (int)(position.Y - SpriteSize.Y / 2),
                    (int)SpriteSize.X,
                    (int)SpriteSize.Y
                );
            }
        }

        protected Texture2D CurrentSprite
        {
            get
            {
                return sprites[currentIndex];
            }
        }
        protected Vector2 SpriteSize
        {
            get
            {
                return new Vector2(CurrentSprite.Width * scale, CurrentSprite.Height * scale);
            }
        }
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public Vector2 Position { get => position; set => position = value; }
        public int Health { get => health; }
        public bool ShouldBeRemoved { get { return shouldBeRemoved; } set { shouldBeRemoved = value; } }

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

            currentIndex = (int)animationTime;

            if (animationTime > sprites.Length - 1)
            {
                animationTime = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (sprites != null)
            {
                Vector2 origin = new Vector2(CurrentSprite.Width / 2, CurrentSprite.Height / 2);
                spriteBatch.Draw(CurrentSprite, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, layerDeph);
            }
            

        }

        public bool IsColliding(GameObject other)
        {
            if (this == other)
                return false;

            return CollisionBox.Intersects(other.CollisionBox);
        }

        public virtual void TakeDamage(int damage)
        {

        }

        public virtual void OnCollision(GameObject other)
        {

        }


    }
}
