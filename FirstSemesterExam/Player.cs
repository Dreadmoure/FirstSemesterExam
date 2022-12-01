﻿using Microsoft.Xna.Framework;
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
using FirstSemesterExam.PowerUps;

namespace FirstSemesterExam
{
    public class Player : GameObject
    {
        private MouseState mouseState;
        private Weapon weapon;
        private int exp;
        private int maxExp;
        private int levelIndicator;

        private int attackDamageLvl;
        private int attackSpeedLvl;
        private float baseAttackSpeed;
        private int maxHealthLvl;
        private float maxHealth;
        private int defenseLvl;
        private float baseDefense;
        private float defense;
        private int movementSpeedLvl;
        private int itemAttackCoolDownLvl;
        private float itemAttackCoolDown;
        private float baseItemAttackCoolDown;
        private int lightSaberLvl;
        private int throwingKnifeLvl;
        private int magicMissileLvl;

        private PowerUpLS powerUpLS;
        private PowerUpMisile powerUpMisile;
        private PowerUpTK powerUpTK;

        private Texture2D crosshair;
        

        private bool invulnerable = false;
        private float iFrames = 0.5f;


        //health bar
        private Texture2D healthBarTexture;
        private Texture2D healthBarBackgroundTexture;
        private Rectangle healthBarRectangle;
        private Rectangle healthBarBackgroundRectangle;
        private Vector2 healthBarPosition;

        private float barLayerDepth;
        private float barBackgroundLayerDepth;

        //exp bar
        private Texture2D expBarTexture;
        private Rectangle expBarRectangle;
        private Vector2 expBarPosition;

        private static bool leveledUp = false;


        //dash indicator bar
        private Texture2D dashBarTexture;
        private Rectangle dashBarRectangle;
        private Vector2 dashBarPosition;

        public int MaxExp
        {
            get { return maxExp; }
            set { MaxExp = value; }
        }

        public int Exp
        {
            get { return exp; }
            set { exp = value; }
        }

        public static bool LeveledUp
        {
            get { return leveledUp; }
            set { leveledUp = value; }
        }

        public int LevelIndicator
        {
            get { return levelIndicator; }
        }

        public float MaxHealth
        {
            get { return maxHealth; }
        }

        public float AttackSpeed
        {
            get { return attackSpeed; }
        }

        public int DefenseLvl
        {
            get { return defenseLvl; }
        }

        public int MovementSpeedLvl
        {
            get { return movementSpeedLvl; }
        }

        public int ItemAttackCoolDownLvl
        {
            get { return itemAttackCoolDownLvl; }
        }

        public int AttackSpeedLvl
        {
            get { return attackSpeedLvl; }
        }

        public int AttackDamageLvl
        {
            get { return attackDamageLvl; }
        }

        public int MaxHealthLvl
        {
            get { return maxHealthLvl; }
        }

        public int LightSaberLvl
        {
            get { return lightSaberLvl; }
        }

        public int ThrowingKnifeLvl
        {
            get { return throwingKnifeLvl; }
        }

        public int MagicMissileLvl
        {
            get { return magicMissileLvl; }
        }
        public float GetItemAttackCoolDown { get => itemAttackCoolDown;}
        public Player()
        {
            //stats
            maxHealth = 100f;
            health = 100f;
            baseSpeed = 300f;
            speed = baseSpeed;
            attackDamage = 10;
            attackDamageLvl = 0;
            attackSpeedLvl = 0;
            baseAttackSpeed = 1f;
            attackSpeed = 1f;
            maxHealthLvl = 0;
            defenseLvl = 0;
            movementSpeedLvl = 0;
            itemAttackCoolDownLvl = 0;
            itemAttackCoolDown = 0f;
            baseItemAttackCoolDown = 1f;
            
            defense = 0f;
            baseDefense = 0f;
            

            lightSaberLvl = 0;
            throwingKnifeLvl = 0;
            magicMissileLvl = 0;

            levelIndicator = 1;
            exp = 0;
            maxExp = 100;

            animationSpeed = 9;
            
            layerDepth = 0.5f;

            barLayerDepth = 0.95f;
            barBackgroundLayerDepth = 0.94f;

            


        }

        public MouseState MouseState { get => mouseState; }
       

        public override void LoadContent(ContentManager content)
        {

            powerUpLS = new PowerUpLS(this);
            GameState.InstantiateGameObject(powerUpLS);

            weapon = new LaserGun(this, attackDamage);
            GameState.InstantiateGameObject(weapon);


            sprites = new Texture2D[2];
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>($"Player\\PlayerWalk_{i +1}");
            }

            healthBarTexture = content.Load<Texture2D>("Player\\HealthV2");
            expBarTexture = content.Load<Texture2D>("Player\\Exp");
            healthBarBackgroundTexture = content.Load<Texture2D>("Player\\HealthBackground");
            dashBarTexture = content.Load<Texture2D>("Player\\DashBar");

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
            Move(gameTime);
            Flip();

            health = Math.Clamp(health, 0, maxHealth);
            exp = Math.Clamp(exp, 0, maxExp);

            if ( velocity != Vector2.Zero)
            {
                Animate(gameTime);
            }
            else
            {
                CurrentIndex = 0;
            }
            if (invulnerable)
            {
                iFrames -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (iFrames <= 0)
                {
                    invulnerable = false;
                    iFrames = 0.5f;
                }
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

            healthBarPosition.X = position.X - (GetSpriteSize.X /0.70f);
            healthBarPosition.Y = position.Y - 45;
            healthBarRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, (int)(healthBarTexture.Width * ((double)(health / maxHealth * 100)/100)), 10);
            healthBarBackgroundRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, 100, 10);

            expBarPosition.X = position.X - (GetSpriteSize.X / 0.70f);
            expBarPosition.Y = position.Y - 35;
            expBarRectangle = new Rectangle((int)expBarPosition.X, (int)expBarPosition.Y, exp, 5);

            dashBarPosition.X = position.X - (GetSpriteSize.X / 0.70f);
            dashBarPosition.Y = position.Y - 30;
            int dashBarOffset = (int)((nextDashCooldown - currentTime) * 50f);
            dashBarRectangle = new Rectangle((int)dashBarPosition.X, (int)dashBarPosition.Y, dashBarOffset, 5);

            

            if (exp >= maxExp)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            exp = 0;
            levelIndicator += 1;
            leveledUp = true;
            
            //sets the mouse position to avoid misclick, which was a thing before
            Mouse.SetPosition((int)(GameWorld.GetScreenSize.X / 2), (int)(GameWorld.GetScreenSize.Y / 1.2f));
        }

        public void InitializeUpgrade(int cardIndex)
        {
            switch (cardIndex)
            {
                case 1: //lightsaber
                    if(lightSaberLvl == 0)
                    {
                        powerUpLS = new PowerUpLS(this);
                        GameState.InstantiateGameObject(powerUpLS);
                        lightSaberLvl += 1;
                        powerUpLS.UpdateLightSaber();
                    }
                    else
                    {
                        lightSaberLvl += 1;
                        powerUpLS.UpdateLightSaber();
                    }
                    break;
                case 2: //throwingKnife
                    if(throwingKnifeLvl == 0)
                    {
                        powerUpTK = new PowerUpTK(this);
                        GameState.InstantiateGameObject(powerUpTK);
                        throwingKnifeLvl += 1;
                        powerUpTK.UpdateTK();
                    }
                    else
                    {
                        throwingKnifeLvl += 1;
                        powerUpTK.UpdateTK();
                    }
                    break;
                case 3: //magicMissile
                    if(magicMissileLvl == 0)
                    {
                        GameState.InstantiateGameObject(new PowerUpMisile(this));
                        magicMissileLvl += 1;
                    }
                    else
                    {
                        magicMissileLvl += 1;
                    }
                    break;
                case 4: //attackDamage
                    attackDamageLvl += 1;
                    attackDamage += 5; //might need to be changed
                    break;
                case 5: //attackSpeed
                    attackSpeedLvl += 1;
                    attackSpeed = baseAttackSpeed * (1 - 1 /(1 + 0.3f * attackSpeedLvl) +1);
                    break;
                case 6: //maxHealth
                    maxHealthLvl += 1;
                    maxHealth += 10f;
                    break;
                case 7: //defense
                    if(defenseLvl == 0)
                    {
                        defenseLvl += 1;
                        defense += 0.1f;
                        baseDefense = defense;
                    }
                    else
                    {
                        defenseLvl += 1;
                        defense = baseDefense * ((1 - 1 / (1 + 0.3f * defenseLvl)) +1);
                    }
                    
                    break;
                case 8: //movementSpeed
                    movementSpeedLvl += 1;
                    speed = baseSpeed * ((1 - 1 / (1 + 0.3f * movementSpeedLvl)) + 1);
                    break;
                case 9: //itemCoolDown
                    itemAttackCoolDownLvl += 1;
                    itemAttackCoolDown = ((1 - 1 / (1 + 0.3f * itemAttackCoolDownLvl)));
                    break;
            }
        }
        //Prøvede at lave en metode hvor spillerens sidste input ville blive gemt og derfra dash i det sidste movement-inputs retning. Kunne ikke få det til at virke.
        //private KeyboardState oldState;
        private Keys movementKey;
        //private Vector2 lastMovedDirection;
        
        float dashCooldown = 2;

        float nextDashCooldown = 0; //start cooldown ligger på 0, så man kan bruge dash til at starte med
        float dashTime;
        int speedMultiplier = 10;
        float currentTime = 0f;
        
        
        private void HandleInput(GameTime gameTime)
        {
            velocity = Vector2.Zero;
            //bool isIdle = velocity.X == 0 && velocity.Y == 0;
            //if (isIdle)
            //{

            //}
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
            //Velocity skal være den samme. Den skal dashe i længere tid hvor den ganger speed med 10, når vi har fået vores dash input. Dash skal have en timer, hvor speed er forhøjet i det antal tid
            if (currentTime >= nextDashCooldown)
            {
                
                if (keyState.IsKeyDown(Keys.Space) && keyState.IsKeyDown(Keys.W))
                {

                    velocity.Y = -10;
                    //speed *= speedMultiplier;

                    
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
                else
                {
                    //speed = 600;
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
            
            health -= damage * (1-defense);
            
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
            spriteBatch.Draw(healthBarTexture, healthBarRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, barLayerDepth);
            spriteBatch.Draw(healthBarBackgroundTexture, healthBarBackgroundRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, barBackgroundLayerDepth);
            spriteBatch.Draw(expBarTexture, expBarRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, barLayerDepth);
            spriteBatch.Draw(dashBarTexture, dashBarRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, barLayerDepth);
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
