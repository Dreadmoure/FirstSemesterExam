using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Input;
using FirstSemesterExam.Menu;
using FirstSemesterExam.Enemies;
using FirstSemesterExam.Projectiles;
using SharpDX.Direct3D9;

namespace FirstSemesterExam
{
    public class Player : GameObject
    {
        private MouseState mouseState;
        private Weapon weapon;
        private int exp;

        private float defense;
        private float itemAttackCoolDown;

        private Texture2D crosshair; 


        //health bar
        private Texture2D healthBarTexture;
        private Rectangle healthBarRectangle;
        private Vector2 healthBarPosition;
        private float healthBarLayerDepth;

        public int Exp
        {
            get { return exp; }
            set { exp = value; }
        }

        public Player()
        {
            //stats
            health = 100f;
            speed = 600;
            //attackDamage = 10;
            attackSpeed = 10f;
            defense = 0.5f;
            itemAttackCoolDown = 5f;

            exp = 0;

            animationSpeed = 9;
            
            layerDepth = 0.5f;

            healthBarLayerDepth = 0.95f;

        }

        public MouseState MouseState { get => mouseState; }

        public override void LoadContent(ContentManager content)
        {
            weapon = new LaserGun(this);
            GameState.InstantiateGameObject(weapon);
            GameState.InstantiateGameObject(new LightSaber(this));

            sprites = new Texture2D[2];
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>($"Player\\PlayerWalk_{i +1}");
            }

            healthBarTexture = content.Load<Texture2D>("Player\\Health");

            position.X = GameWorld.GetScreenSize.X / 2;
            position.Y = GameWorld.GetScreenSize.Y / 2;

            crosshair = content.Load<Texture2D>("Player\\crosshair");
            Mouse.SetCursor(MouseCursor.FromTexture2D(crosshair, mouseState.X, mouseState.Y));
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

            healthBarPosition.X = position.X - (GetSpriteSize.X /0.70f);
            healthBarPosition.Y = position.Y - 45;
            healthBarRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, (int)health, 10);
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

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                weapon.Shoot(gameTime);
            }
        }

        public float MouseAngle()
        {
            //Regner vinkel ud mellem spiller og musen.
            float f = MathF.Atan2((mouseState.Y ) - position.Y, mouseState.X - position.X);
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

        public override void OnCollision(GameObject other)
        {
            if(other is Enemy)
            {
                health -= (int)other.GetAttackDamage; 
            }
            if(other is EnemyProjectile)
            {
                health -= (int)other.GetAttackDamage;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBarTexture, healthBarRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, healthBarLayerDepth);
            base.Draw(spriteBatch);
        }

    } 
}
