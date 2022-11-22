using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace FirstSemesterExam
{
    internal class Player : GameObject
    {
        private MouseState mouseState;
        
        public Player()
        {
            scale = 3;
            speed = 600;
            animationSpeed = 3;
            health = 100;

        }

        public MouseState MouseState { get => mouseState; }

        public override void LoadContent(ContentManager content)
        {
            GameWorld.InstantiateGameObject(new Weapon(this));

            sprites = new Texture2D[2];
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>($"Player\\PlayerWalk_{i +1}");
            }

            position.X = GameWorld.GetScreenSize.X / 2;
            position.Y = GameWorld.GetScreenSize.Y / 2;
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            HandleInput(gameTime);
            Move(gameTime);
            Flip();
            if ( velocity != Vector2.Zero)
            {
                Animate(gameTime);
            }
            else
            {
                CurrentIndex = 1;
            }
        }

        private void HandleInput(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W))
            {
                velocity.Y -= 1;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                velocity.Y += 1;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                velocity.X -= 1;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                velocity.X += 1;
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                //Shoot(gameTime);
            }
        }

        public float MouseAngle()
        {
            float f = MathF.Atan2(mouseState.Y - position.Y, mouseState.X - position.X);
            if (f < 0)
            {
                return f + 2 * MathF.PI;
            }
            else
            {
                return f;
            }
        }

        protected void Flip()
        {
            if (MouseAngle() > (MathF.PI / 2) && MouseAngle() < (3 * Math.PI) / 2)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }
        }

    } 
}
