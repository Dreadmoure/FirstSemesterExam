using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using FirstSemesterExam.Projectiles;
using FirstSemesterExam.Menu;
using Color = Microsoft.Xna.Framework.Color;

namespace FirstSemesterExam.Enemies
{
    public abstract class Enemy : GameObject
    {
        protected Random random = new Random();
        enum Edge { Upper, Lower, Left, Right }
        private float attackTime;
        protected float attackRange;
        private Player player;
        protected int expValue;

        public Enemy(Player player)
        {
            this.player = player; 
            animationSpeed = 1f;
            spriteEffects = SpriteEffects.None;
            //rotation = 0.01f;

            // set initial position randomly 
            switch (random.Next(0, 4)) // 0 to 3 
            {
                case int n when n == (int)Edge.Upper: // 0
                    position.X = random.NextFloat(0, GameWorld.GetScreenSize.X);
                    position.Y = 0;
                    break;
                case int n when n == (int)Edge.Lower: // 1
                    position.X = random.NextFloat(0, GameWorld.GetScreenSize.X);
                    position.Y = GameWorld.GetScreenSize.Y;
                    break;
                case int n when n == (int)Edge.Left: // 2
                    position.X = 0;
                    position.Y = random.NextFloat(0, GameWorld.GetScreenSize.Y);
                    break;
                case int n when n == (int)Edge.Right: // 3
                    position.X = GameWorld.GetScreenSize.X;
                    position.Y = random.NextFloat(0, GameWorld.GetScreenSize.Y);
                    break;
                default:
                    // throw exception 
                    break;
            }
        }

        public override void LoadContent(ContentManager content)
        {
            // sprites are handled in the classes that inherits from Enemy 
        }

        public override void Update(GameTime gameTime)
        {
            HandlePosition();
            Move(gameTime);
            Animate(gameTime);

            // attack when ready 
            attackTime += (float)gameTime.ElapsedGameTime.TotalSeconds * attackSpeed;
            if (attackTime > 10f) // TODO: check if it should be lower 
            {
                Attack();
                attackTime = 0f;
            }

            if (HasJustBeenHit)
            {
                hitTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (hitTimer <= 0.1f)
                {
                    color = Color.Red;
                }
                else
                {
                    hitTimer = 0;
                    color = Color.White;
                    HasJustBeenHit = false; 
                }
            }

            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            GameState.CountEnemyKill(); 

            float rnd = random.Next(100);
            if (rnd < 10)
            {
                GameState.InstantiateGameObject(new PickUp(position));
            }
            player.Exp += expValue;
            ShouldBeRemoved = true;
        }

        private void HandlePosition()
        {
            // reset velocity 
            velocity = Vector2.Zero;

            // set velocity towards player position 
            if (position.X > player.GetPosition.X)
            {
                velocity.X += -player.GetPosition.X;
                spriteEffects = SpriteEffects.None;
            }
            else if (position.X < player.GetPosition.X)
            {
                velocity.X += player.GetPosition.X;
                spriteEffects = SpriteEffects.FlipHorizontally;
                
            }
            if (position.Y > player.GetPosition.Y)
            {
                velocity.Y += -player.GetPosition.Y;
            }
            else if (position.Y < player.GetPosition.Y)
            {
                velocity.Y += player.GetPosition.Y;
            }

            // set the length of the velocity vector to 1 no matter direction. 
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
        }

        public override void OnCollision(GameObject other)
        {
            if ( other is Player)
            {
                other.TakeDamage(attackDamage);
            }
        }

        public virtual void Attack()
        {
            GameState.InstantiateGameObject(new EnemyProjectile(position, player.GetPosition, attackRange));
        }
    }
}
