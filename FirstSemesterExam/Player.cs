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
using SharpDX.MediaFoundation;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch; //blev added for a fixe linje 183 spritebatch, why I dunno
using System.Drawing.Text;
using System.Dynamic;

namespace FirstSemesterExam
{
    public class Player : GameObject
    {
        private MouseState mouseState;
        private Weapon weapon;
        private int exp;
        private int levelIndicator;

        private float defense;
        private float itemAttackCoolDown;

        private Texture2D crosshair; 

        //health bar
        private Texture2D healthBarTexture;
        private Texture2D healthBarBackgroundTexture;
        private Rectangle healthBarRectangle;
        private Rectangle healthBarBackgroundRectangle;
        private Vector2 healthBarPosition;
        private float healthBarLayerDepth;
        private float healthBarBackgroundLayerDepth;

        //exp bar
        private Texture2D expBarTexture;
        private Rectangle expBarRectangle;
        private Vector2 expBarPosition;
        private float expBarLayerDepth;

        public int Exp
        {
            get { return exp; }
            set { exp = value; }
        }

        public int LevelIndicator
        {
            get { return levelIndicator; }
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

            levelIndicator = 1;
            exp = 0;

            animationSpeed = 9;
            
            layerDepth = 0.5f;

            healthBarLayerDepth = 0.95f;
            healthBarBackgroundLayerDepth = 0.94f;
            expBarLayerDepth = 0.95f;

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
            expBarTexture = content.Load<Texture2D>("Player\\Exp");
            healthBarBackgroundTexture = content.Load<Texture2D>("Player\\HealthBackground");

            position.X = GameWorld.GetScreenSize.X / 2;
            position.Y = GameWorld.GetScreenSize.Y / 2;

            crosshair = content.Load<Texture2D>("Player\\crosshair");
            Mouse.SetCursor(MouseCursor.FromTexture2D(crosshair, mouseState.X, mouseState.Y));
        }

        public override void Update(GameTime gameTime)
        {
            //KeyboardState keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            HandleInput(gameTime);
            HandleLimits();
            //dashCooldown = (dashCooldown >= 5000 && keyState.IsKeyDown(Keys.Space)) ? 0 : dashCooldown + gameTime.ElapsedGameTime.TotalMilliseconds;
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

            if(exp >= 100)
            {
                LevelUp();
            }

            healthBarPosition.X = position.X - (GetSpriteSize.X /0.70f);
            healthBarPosition.Y = position.Y - 45;
            healthBarRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, (int)health, 10);
            healthBarBackgroundRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, 100, 10);

            expBarPosition.X = position.X - (GetSpriteSize.X / 0.70f);
            expBarPosition.Y = position.Y - 35;
            expBarRectangle = new Rectangle((int)expBarPosition.X, (int)expBarPosition.Y, exp, 5);
        }

        private void LevelUp()
        {
            exp = 0;
            levelIndicator += 1;
        }
        //Prøvede at lave en metode hvor spillerens sidste input ville blive gemt og derfra dash i det sidste movement-inputs retning. Kunne ikke få det til at virke.
        //private KeyboardState oldState;
        private Keys movementKey;
        //private Vector2 lastMovedDirection;
        float dashCooldown = 1; // Vores Dash Cooldown, hver
        float nextDashCooldown = 0; //start cooldown ligger på 0, så man kan bruge dash til at starte med
        
        float currentTime = 0f;
        
        private void HandleInput(GameTime gameTime)
        {
            velocity = Vector2.Zero;
            bool isIdle = velocity.X == 0 && velocity.Y == 0;
            if (isIdle)
            {

            }
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
            //if (keyState.IsKeyDown(Keys.Space))
            //{
            //    float dashDistance = 5f;


            //}
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;//Tjekker hvor lang tid der er gået i spillet
            if (currentTime >= nextDashCooldown)
            {
                
                if (keyState.IsKeyDown(Keys.Space) && keyState.IsKeyDown(Keys.W))
                {

                    velocity.Y = -10;
                    
                    nextDashCooldown = currentTime + dashCooldown;

                }
                if (keyState.IsKeyDown(Keys.Space) && keyState.IsKeyDown(Keys.S))
                {

                    velocity.Y = 10;
                    nextDashCooldown = currentTime + dashCooldown;
                }
                if (keyState.IsKeyDown(Keys.Space) && keyState.IsKeyDown(Keys.A))
                {

                    velocity.X = -10;
                    nextDashCooldown = currentTime + dashCooldown;
                }
                if (keyState.IsKeyDown(Keys.Space) && keyState.IsKeyDown(Keys.D))
                {

                    velocity.X = 10;
                    nextDashCooldown = currentTime + dashCooldown;

                }
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

        public override void TakeDamage(float damage)
        {
            
            health -= damage;
            
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBarTexture, healthBarRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, healthBarLayerDepth);
            spriteBatch.Draw(healthBarBackgroundTexture, healthBarBackgroundRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, healthBarBackgroundLayerDepth);
            spriteBatch.Draw(expBarTexture, expBarRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, expBarLayerDepth);
            base.Draw(spriteBatch);
        }
        //Overvejde at lave Dash til en funktion der kunne blive kaldt
        //private void Dash()
        //{
        //    if (keyState.IsKeyDown(Keys.S))
        //    {

        //    }
        //}

    } 
}
