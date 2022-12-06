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
using FirstSemesterExam.PowerUps;

namespace FirstSemesterExam
{
    /// <summary>
    /// The player gameobject which the user controls
    /// </summary>
    public class Player : GameObject
    {
        #region Fields
        //mouse related
        private Texture2D crosshair;
        private MouseState mouseState;

        //weapon
        private Weapon weapon;

        //exp related
        private float exp;
        private float maxExp;
        private float baseMaxExp;
        private int levelIndicator;
        private static bool leveledUp = false;

        //stat related
        private int attackDamageLvl;
        private int attackSpeedLvl;
        private float baseAttackSpeed;
        private int maxHealthLvl;
        private float maxHealth;
        private int defenseLvl;
        private float defense;
        private int movementSpeedLvl;
        private int itemAttackCoolDownLvl;
        private float itemAttackCoolDown;
        private int lightSaberLvl;
        private int throwingKnifeLvl;
        private int magicMissileLvl;
        
        //Powerup related
        private PowerUpLS powerUpLS;
        private PowerUpMisile powerUpMisile;
        private PowerUpTK powerUpTK;

        //iFrames related
        private bool invulnerable = false;
        private float iFrames = 0.5f;

        //health bar
        private Texture2D healthBarTexture;
        private Texture2D healthBarBackgroundTexture;
        private Rectangle healthBarRectangle;
        private Rectangle healthBarBackgroundRectangle;
        private Vector2 healthBarPosition;

        //bar layerdepths
        private float barLayerDepth;
        private float barBackgroundLayerDepth;

        //exp bar
        private Texture2D expBarTexture;
        private Rectangle expBarRectangle;
        private Vector2 expBarPosition;

        //dash indicator bar
        private Texture2D dashBarTexture;
        private Rectangle dashBarRectangle;
        private Vector2 dashBarPosition;
        #endregion

        #region Properties
        /// <summary>
        /// used to check if the player has enough exp to level up
        /// if exp is bigger or equal to maxExp
        /// </summary>
        public float MaxExp
        {
            get { return maxExp; }
        }

        /// <summary>
        /// used to check if the player has enough exp to level up
        /// if exp is bigger or equal to maxExp
        /// </summary>
        public float Exp
        {
            get { return exp; }
            set { exp = value; } //used to add exp to the players value
        }

        /// <summary>
        /// returns a bool if the player levels up, which is used to call the levelUp method
        /// </summary>
        public static bool LeveledUp
        {
            get { return leveledUp; }
            set { leveledUp = value; }
        }

        /// <summary>
        /// used to check if the players current health is lower than its Max value
        /// </summary>
        public float MaxHealth
        {
            get { return maxHealth; }
        }

        /// <summary>
        /// used to determine the rate at which the player can shoot
        /// </summary>
        public float AttackSpeed
        {
            get { return attackSpeed; }
        }

        /// <summary>
        /// Used to draw the current level + 1 on the level up card
        /// </summary>
        public int DefenseLvl
        {
            get { return defenseLvl; }
        }
        /// <summary>
        /// Used to draw the current level + 1 on the level up card
        /// </summary>
        public int MovementSpeedLvl
        {
            get { return movementSpeedLvl; }
        }
        /// <summary>
        /// Used to draw the current level + 1 on the level up card 
        /// </summary>
        public int ItemAttackCoolDownLvl
        {
            get { return itemAttackCoolDownLvl; }
        }
        /// <summary>
        /// Used to draw the current level + 1 on the level up card
        /// </summary>
        public int AttackSpeedLvl
        {
            get { return attackSpeedLvl; }
        }
        /// <summary>
        /// used to get the players attackDamage to the weapon, which shoots
        /// </summary>
        public float AttackDamage
        {
            get { return attackDamage; }
        }

        /// <summary>
        /// Used to draw the current level + 1 on the level up card
        /// </summary>
        public int AttackDamageLvl
        {
            get { return attackDamageLvl; }
        }

        /// <summary>
        /// Used to draw the current level + 1 on the level up card
        /// </summary>
        public int MaxHealthLvl
        {
            get { return maxHealthLvl; }
        }

        /// <summary>
        /// Used to draw the current level + 1 on the level up card
        /// Also used to determine how many lightsabers the player and its power
        /// </summary>
        public int LightSaberLvl
        {
            get { return lightSaberLvl; }
        }
        /// <summary>
        /// Used to draw the current level + 1 on the level up card
        /// Also used to determine how many knives the player and its power
        /// </summary>
        public int ThrowingKnifeLvl
        {
            get { return throwingKnifeLvl; }
        }
        /// <summary>
        /// Used to draw the current level + 1 on the level up card
        /// Also used to determine the missiles power
        /// </summary>
        public int MagicMissileLvl
        {
            get { return magicMissileLvl; }
        }
        /// <summary>
        /// used in determining the rate at which the powerup weapons activates
        /// </summary>
        public float GetItemAttackCoolDown { get => itemAttackCoolDown;}
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for the Player with all its stats
        /// </summary>
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
            
            defense = 0f;
            
            lightSaberLvl = 0;
            throwingKnifeLvl = 0;
            magicMissileLvl = 0;

            levelIndicator = 1;
            exp = 0f;
            baseMaxExp = 50f;
            maxExp = baseMaxExp;

            animationSpeed = 9;
            
            layerDepth = 0.5f;

            barLayerDepth = 0.95f;
            barBackgroundLayerDepth = 0.94f;
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            //loads the weapon for the player and instantiates it
            weapon = new LaserGun(this, attackDamage);
            GameState.InstantiateGameObject(weapon);

            //loads the sprites for the player character
            sprites = new Texture2D[2];
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>($"Player\\PlayerWalk_{i +1}");
            }

            //loads sprites for the varios bars
            healthBarTexture = content.Load<Texture2D>("Player\\HealthV2");
            expBarTexture = content.Load<Texture2D>("Player\\ExpV2");
            healthBarBackgroundTexture = content.Load<Texture2D>("Player\\HealthBackground");
            dashBarTexture = content.Load<Texture2D>("Player\\DashBar");

            //sets the player position to the middle of the screen
            position.X = GameWorld.GetScreenSize.X / 2;
            position.Y = GameWorld.GetScreenSize.Y / 2;

            //changes the mouse cursor to a custom sprite
            crosshair = content.Load<Texture2D>("Player\\crosshair");
            Mouse.SetCursor(MouseCursor.FromTexture2D(crosshair, mouseState.X, mouseState.Y));
        }

        public override void Update(GameTime gameTime)
        {
            //updates the mouse position and "click" state
            mouseState = Mouse.GetState();

            //calls methods
            HandleInput(gameTime);
            HandleLimits();
            Move(gameTime);
            Flip();

            //sets the min and max value of health and exp, so it cannot exceed those limits
            health = Math.Clamp(health, 0, maxHealth);
            exp = Math.Clamp(exp, 0, maxExp);

            //looks at the velocity and animates the player character from the value
            if ( velocity != Vector2.Zero)
            {
                Animate(gameTime);
            }
            else
            {
                CurrentIndex = 0;
            }

            //if the player is invulnerable it sets a timer to determine when the player stops being invulnerable
            if (invulnerable)
            {
                iFrames -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (iFrames <= 0)
                {
                    invulnerable = false;
                    iFrames = 0.5f;
                }
            }

            //if the player has been hit, sets a red color overlay over the player. sets a timer which determines
            //when the overlay stops appearing
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

            //sets the position, calculates the rectangle for the healthbar and its baackground
            healthBarPosition.X = position.X - (GetSpriteSize.X /0.70f);
            healthBarPosition.Y = position.Y - 45;
            healthBarRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, (int)(healthBarTexture.Width * ((double)(health / maxHealth * 100)/100)), 10);
            healthBarBackgroundRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, 100, 10);

            //sets the position, calculates the rectangle for the expBar
            expBarPosition.X = position.X - (GetSpriteSize.X / 0.70f);
            expBarPosition.Y = position.Y - 35;
            expBarRectangle = new Rectangle((int)expBarPosition.X, (int)expBarPosition.Y, (int)(expBarTexture.Width * ((double)(exp / maxExp * 100) / 100)), 5);

            //sets the position, calculates the rectangle for the dasshBar
            dashBarPosition.X = position.X - (GetSpriteSize.X / 0.70f);
            dashBarPosition.Y = position.Y - 30;
            int dashBarOffset = (int)((nextDashCooldown - currentTime) * 50f);
            dashBarRectangle = new Rectangle((int)dashBarPosition.X, (int)dashBarPosition.Y, dashBarOffset, 5);

            //checks the current exp against the maxExp
            if (exp >= maxExp)
            {
                LevelUp();
            }
        }
        /// <summary>
        /// called when the player has enough exp, used to scale and reset the expBar and importantly
        /// sets the levelUp bool so the levelup cards will be activated
        /// </summary>
        private void LevelUp()
        {
            exp = 0;
            levelIndicator += 1;
            leveledUp = true;
            maxExp += baseMaxExp * (int)(0.1f * levelIndicator); 
            //sets the mouse position to avoid misclick, which was a thing before
            Mouse.SetPosition((int)(GameWorld.GetScreenSize.X / 2), (int)(GameWorld.GetScreenSize.Y / 1.2f));
        }
        /// <summary>
        /// called when the user chooses a card and then initializes the upgrade based on the cardIndex
        /// </summary>
        /// <param name="cardIndex"></param>
        public void InitializeUpgrade(int cardIndex)
        {
            switch (cardIndex)
            {
                case 1: //lightsaber
                    if(lightSaberLvl == 0) //if players current lvl is 0
                    {
                        //activates the Lightsaber spawner
                        powerUpLS = new PowerUpLS(this);
                        GameState.InstantiateGameObject(powerUpLS);
                        lightSaberLvl += 1;
                        powerUpLS.UpdateLightSaber();
                    }
                    else
                    {
                        //upgrades the lightsaber
                        lightSaberLvl += 1;
                        powerUpLS.UpdateLightSaber();
                    }
                    break;
                case 2: //throwingKnife
                    if(throwingKnifeLvl == 0)
                    {
                        //activates the knife spawner
                        powerUpTK = new PowerUpTK(this);
                        GameState.InstantiateGameObject(powerUpTK);
                        throwingKnifeLvl += 1;
                        powerUpTK.UpdateTK();
                    }
                    else
                    {
                        //upgrades the knife
                        throwingKnifeLvl += 1;
                        powerUpTK.UpdateTK();
                    }
                    break;
                case 3: //magicMissile
                    if(magicMissileLvl == 0)
                    {
                        //activates the magicMissile spawner
                        powerUpMisile = new PowerUpMisile(this);
                        GameState.InstantiateGameObject(powerUpMisile);
                        magicMissileLvl += 1;
                        powerUpMisile.UpdateMisile();
                    }
                    else
                    {
                        //upgrades the magicMissile
                        magicMissileLvl += 1;
                        powerUpMisile.UpdateMisile();
                    }
                    break;
                case 4: //attackDamage upgrade
                    attackDamageLvl += 1;
                    attackDamage += 5;
                    break;
                case 5: //attackSpeed upgrade
                    attackSpeedLvl += 1;
                    attackSpeed = baseAttackSpeed * ((0.3f * attackSpeedLvl) +1); //adds 30 percent for each lvl
                    break;
                case 6: //maxHealth upgrade
                    maxHealthLvl += 1;
                    maxHealth += 10f;
                    break;
                case 7: //defense upgrade
                    if (defenseLvl == 0)
                    {
                        defenseLvl += 1;
                        defense += 0.1f;
                    }
                    else
                    {
                        defenseLvl += 1;
                        defense = ((1 - 1 / (1 + 0.1f * defenseLvl))); //adds 10 percent for each lvl
                    }
                    break;
                case 8: //movementSpeed upgrade
                    movementSpeedLvl += 1;
                    speed = baseSpeed * ((1 - 1 / (1 + 0.3f * movementSpeedLvl)) + 1); //adds 30 percent for each lvl
                    break;
                case 9: //itemCoolDown upgrade
                    itemAttackCoolDownLvl += 1;
                    itemAttackCoolDown = ((1 - 1 / (1 + 0.3f * itemAttackCoolDownLvl))); //adds 30 percent for each lvl
                    break;
            }
        }


        //Jeppe kommentar -


        //Prøvede at lave en metode hvor spillerens sidste input ville blive gemt og derfra dash i det sidste movement-inputs retning. Kunne ikke få det til at virke.
        //private KeyboardState oldState;
        private Keys movementKey;
        //private Vector2 lastMovedDirection;
        
        float dashCooldown = 2;

        float nextDashCooldown = 0; //start cooldown ligger på 0, så man kan bruge dash til at starte med
        float dashTime;
        int speedMultiplier = 10;
        float currentTime = 0f;

        //-Jeppe kommentar
        
        /// <summary>
        /// checks for input both mouse and keyboard, which is used to aiming, shooting and movement
        /// </summary>
        /// <param name="gameTime"></param>
        private void HandleInput(GameTime gameTime)
        {
            //resets the velocity
            velocity = Vector2.Zero;
            //gets the keyboardstate
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W)) //up
            {
                velocity.Y -= 1;
                
            }
            if (keyState.IsKeyDown(Keys.S)) //down
            {
                velocity.Y += 1;
                
            }
            if (keyState.IsKeyDown(Keys.A)) //left
            {
                velocity.X -= 1;
                
            }
            if (keyState.IsKeyDown(Keys.D)) //right
            {
                velocity.X += 1;
                
            }

            //if we are moving we normalize the vector so we dont add 2 vectors and end up moving faster
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            //press the left mouse button and we shoot
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                weapon.Shoot(gameTime);
            }


            //Jeppe kommentar -


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

            //-Jeppe kommentar

        }

        public float MouseAngle()
        {
            //Calculates the angle between the player and mouse. Atan2 gives an angle measured in radians, between -Pi abd Pi
            float f = MathF.Atan2((mouseState.Y) - position.Y, mouseState.X - position.X);
            // plusser med 2 pi hvis vinklen er negativ så det er nemmere at regne med.
            if (f < 0)
            {
                return f + 2 * MathF.PI;
            }
            else
            {
                return f;
            }
        }
        /// <summary>
        /// The player takes damage based on the parameter, but only if the player is not invulnerable.
        /// </summary>
        /// <param name="damage"></param>
        public override void TakeDamage(float damage)
        {
            if (!invulnerable)
            {
                hasJustBeenHit = true;
                health -= damage * (1-defense);
                invulnerable = true;
                GameState.InstantiateGameObject(new FloatingCombatText(position, damage * (1 - defense)));
            }
            
        }
        /// <summary>
        /// Flips the sprite based on the angle to the mouse
        /// </summary>
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
        /// <summary>
        /// clamps the playerspostion based on the screen size
        /// </summary>
        private void HandleLimits()
        {
            position.X = Math.Clamp(position.X, GetSpriteSize.X / 2, GameWorld.GetScreenSize.X - GetSpriteSize.X / 2);
            position.Y = Math.Clamp(position.Y, GetSpriteSize.Y / 2, GameWorld.GetScreenSize.Y - GetSpriteSize.Y / 2);
        }
        /// <summary>
        /// draws the players health, exp and dash bars
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBarTexture, healthBarRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, barLayerDepth);
            spriteBatch.Draw(healthBarBackgroundTexture, healthBarBackgroundRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, barBackgroundLayerDepth);
            spriteBatch.Draw(expBarTexture, expBarRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, barLayerDepth);
            spriteBatch.Draw(dashBarTexture, dashBarRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, barLayerDepth);
            base.Draw(spriteBatch);
        }

        //Jeppe kommentar -
        //Overvejde at lave Dash til en funktion der kunne blive kaldt
        //private void Dash()
        //{
        //    if (keyState.IsKeyDown(Keys.S))
        //    {

        //    }
        //}
        //-Jeppe kommentar
        #endregion
    }
}
