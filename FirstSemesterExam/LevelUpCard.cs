using FirstSemesterExam.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace FirstSemesterExam
{
    public class LevelUpCard : GameObject
    {
        #region Fields

        private Texture2D sprite;

        private string text;
        private SpriteFont textFont;
        private int cardIndex;
        private string cardLvlIndicator;
        
        private Random random = new Random();

        private MouseState _currentMouse;
        private MouseState _previousMouse;
        public bool isClicked;

        private bool colorShiftDown;
        #endregion

        #region Properties
        private Rectangle GetRectangle
        {
            get
            {
                return new Rectangle(
                    (int)(position.X - GetSpriteSize.X / 2),
                    (int)(position.Y - GetSpriteSize.Y / 2),
                    (int)GetSpriteSize.X,
                    (int)GetSpriteSize.Y
                );
            }
        }

        public int GetCardIndex
        {
            get { return cardIndex; }
        }

        private Vector2 Origin
        {
            get { return new Vector2(sprite.Width / 2, sprite.Height / 2); }
        }

        #endregion

        #region Constructors
        public LevelUpCard(Vector2 position)
        {
            this.position = position;
            layerDepth = 0.98f;
            scale = 1f;

        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {

            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

                if (mouseRectangle.Intersects(GetRectangle))
                {
                    ColorShift();

                    


                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        isClicked = true;
                        color.A = 255;
                    }



                }
                else if (color.A < 255)
                {
                    color.A += 3;
                }

        }

        public override void LoadContent(ContentManager content)
        {
            textFont = content.Load<SpriteFont>("Fonts\\textFont");

            sprites = new Texture2D[1];

            sprites[0] = content.Load<Texture2D>("LevelUpCards\\card");




            //assigns the sprite of the card from the index number
            RandomCard(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, color, 0f, Origin, scale, SpriteEffects.None, layerDepth);

            if (!string.IsNullOrEmpty(text))
            {
                float x = (GetRectangle.X + GetRectangle.Width / 2) - textFont.MeasureString(text).X / 2;
                float y = (GetRectangle.Y + GetRectangle.Height / 2 + 15) - textFont.MeasureString(text).Y / 2;

                spriteBatch.DrawString(textFont, text, new Vector2(x, y), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth + 0.01f);
            }

            if (!string.IsNullOrEmpty(cardLvlIndicator))
            {
                float x = (GetRectangle.X + 35) - textFont.MeasureString(cardLvlIndicator).X / 2;
                float y = (GetRectangle.Y + 25) - textFont.MeasureString(cardLvlIndicator).Y / 2;

                spriteBatch.DrawString(textFont, cardLvlIndicator, new Vector2(x, y), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, layerDepth + 0.01f);
            }

            
        }

        private void ColorShift()
        {
            if (color.A == 255)
            {
                colorShiftDown = false;
            }
            if (color.A == 0)
            {
                colorShiftDown = true;
            }
            if (colorShiftDown)
            {
                color.A += 3;
            }
            else
            {
                color.A -= 3;
            }
        }

        public void RandomCard(ContentManager content)
        {
            cardIndex = random.Next(1, 10);
            switch (cardIndex)
            {
                case 1: //lightsaber
                    sprites[0] = content.Load<Texture2D>("LevelUpCards\\LightsaberCard");
                    text = "Lightsaber";
                    cardLvlIndicator = (GameState.player.LightSaberLvl + 1).ToString();
                    break;
                case 2: //knife
                    sprites[0] = content.Load<Texture2D>("LevelUpCards\\KnifeCard");
                    text = "Knife";
                    cardLvlIndicator = (GameState.player.ThrowingKnifeLvl + 1).ToString();
                    break;
                case 3: //magic missile
                    sprites[0] = content.Load<Texture2D>("LevelUpCards\\MagicMissileCard");
                    text = "Magic Missile";
                    cardLvlIndicator = (GameState.player.MagicMissileLvl + 1).ToString();
                    break;
                case 4: //attackDamage
                    sprites[0] = content.Load<Texture2D>("LevelUpCards\\AttackDamageCard");
                    text = "Damage";
                    cardLvlIndicator = (GameState.player.AttackDamageLvl + 1).ToString();
                    break;
                case 5: //attackSpeed
                    sprites[0] = content.Load<Texture2D>("LevelUpCards\\AttackSpeedCard"); // attack speed
                    text = "Attack Speed";
                    cardLvlIndicator = (GameState.player.AttackSpeedLvl + 1).ToString();
                    break;
                case 6: //maxHealth
                    sprites[0] = content.Load<Texture2D>("LevelUpCards\\MaxHealthCard"); // max health 
                    text = "Max Health";
                    cardLvlIndicator = (GameState.player.MaxHealthLvl + 1).ToString();
                    break;
                case 7: //defense
                    sprites[0] = content.Load<Texture2D>("LevelUpCards\\DefenceCard"); // defence 
                    text = "Defense";
                    cardLvlIndicator = (GameState.player.DefenseLvl + 1).ToString();
                    break;
                case 8: //movementSpeed
                    sprites[0] = content.Load<Texture2D>("LevelUpCards\\MovementSpeedCard"); // movement speed 
                    text = "Movement Speed";
                    cardLvlIndicator = (GameState.player.MovementSpeedLvl + 1).ToString();
                    break;
                case 9: //itemCoolDown
                    sprites[0] = content.Load<Texture2D>("LevelUpCards\\ItemCooldownCard"); // item cooldown 
                    text = "Item Cooldown";
                    cardLvlIndicator = (GameState.player.ItemAttackCoolDownLvl + 1).ToString();
                    break;
                default: //make it throw an expection
                    break;
            }
            sprite = sprites[0];
        }
        #endregion
    }   
}
