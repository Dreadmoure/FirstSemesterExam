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

namespace FirstSemesterExam
{
    public abstract class Enemy : GameObject
    {
        private Random random = new Random(); 
        enum Edge { Upper, Lower, Left, Right }
        private float attackTime;
        protected float attackRange; 

        public Enemy()
        {
            animationSpeed = 1f;
            rotation = 0.01f;

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

            // attack when ready 
            attackTime += (float)gameTime.ElapsedGameTime.TotalSeconds * attackSpeed; 
            if(attackTime > 10f) // TODO: check if it should be lower 
            {
                Attack();
                attackTime = 0f; 
            }
        }

        private void HandlePosition()
        {
            // reset velocity 
            velocity = Vector2.Zero;

            // set velocity towards center of screen (TODO: change to player position later) 
            if(position.X > GameWorld.GetScreenSize.X / 2)
            {
                velocity.X += -GameWorld.GetScreenSize.X / 2; 
            }
            else if(position.X < GameWorld.GetScreenSize.X / 2)
            {
                velocity.X += GameWorld.GetScreenSize.X / 2;
            }
            if(position.Y > GameWorld.GetScreenSize.Y / 2)
            {
                velocity.Y += -GameWorld.GetScreenSize.Y / 2; 
            }
            else if(position.Y < GameWorld.GetScreenSize.Y / 2)
            {
                velocity.Y += GameWorld.GetScreenSize.Y / 2;
            }

            // set the length of the velocity vector to 1 no matter direction. 
            if(velocity != Vector2.Zero)
            {
                velocity.Normalize(); 
            }
        }

        public override void OnCollision(GameObject other)
        {
            if(other is PlayerProjectile)
            {
                health -= (int)other.GetAttackDamage; 
                if(health <= 0)
                {
                    ShouldBeRemoved = true; 
                }
            }
        }

        public virtual void Attack()
        {
            GameWorld.InstantiateGameObject(new EnemyProjectile(position, velocity, attackRange)); 
        }
    }
}
