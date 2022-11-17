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

namespace FirstSemesterExam
{
    public abstract class Enemy : GameObject
    {
        private Random random = new Random(); 
        enum Edge { Upper, Lower, Left, Right }

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
                    break;
            }
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];
            sprites[0] = content.Load<Texture2D>(""); 
        }

        public override void Update(GameTime gameTime)
        {
            HandlePosition(); 
            Move(gameTime); 
        }

        private void HandlePosition()
        {
            // reset velocity 
            velocity = Vector2.Zero;

            // set velocity towards center of screen (TODO: change to player position) 
            velocity += GameWorld.GetScreenSize/2; 

            // set the length of the velocity vector to 1 no matter direction. 
            if(velocity != Vector2.Zero)
            {
                velocity.Normalize(); 
            }
        }

        public override void OnCollision(GameObject other)
        {

        }

        public void Attack()
        {

        }
    }
}
