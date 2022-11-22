using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using FirstSemesterExam.Menu;

namespace FirstSemesterExam
{
    public class Player : GameObject
    {
        private MouseState mouseState;
        private int experiencePoints;

        #region Properties
        public int ExperiencePoints
        {
            get { return experiencePoints; }
            set { experiencePoints = value; }
        }
        #endregion

        public Player()
        {
            speed = 600;
            animationSpeed = 9;
            health = 100;
            layerDepth = 0.5f;
            experiencePoints = 0;

        }

        public MouseState MouseState { get => mouseState; }

        public override void LoadContent(ContentManager content)
        {
            GameState.InstantiateGameObject(new Weapon(this));

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
            HandleLimits();
            Move(gameTime);
            Flip();
            if ( velocity != Vector2.Zero)
            {
                Animate(gameTime);
            }
            else
            {
                CurrentIndex = 0;
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

            //checks experience points
            if(experiencePoints >= 100)
            {
                LevelUp();
                experiencePoints = 0;
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

        private void HandleLimits()
        {
            position.X = Math.Clamp(position.X, GetSpriteSize.X / 2, GameWorld.GetScreenSize.X - GetSpriteSize.X / 2);
            position.Y = Math.Clamp(position.Y, GetSpriteSize.Y / 2, GameWorld.GetScreenSize.Y - GetSpriteSize.Y / 2);
        }

        public void LevelUp()
        {
            //Do something
        }

    } 
}
