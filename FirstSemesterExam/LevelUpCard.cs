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
            layerDepth = 0.99f;
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

            sprites = new Texture2D[9];
            sprites[0] = content.Load<Texture2D>("LevelUpCards\\card"); //needs to ba changed
            sprites[1] = content.Load<Texture2D>("LevelUpCards\\card"); //needs to ba changed
            sprites[2] = content.Load<Texture2D>("LevelUpCards\\card"); //needs to ba changed
            sprites[3] = content.Load<Texture2D>("LevelUpCards\\card"); //needs to ba changed
            sprites[4] = content.Load<Texture2D>("LevelUpCards\\card"); //needs to ba changed
            sprites[5] = content.Load<Texture2D>("LevelUpCards\\card"); //needs to ba changed
            sprites[6] = content.Load<Texture2D>("LevelUpCards\\card");
            sprites[7] = content.Load<Texture2D>("LevelUpCards\\card");//needs to ba changed
            sprites[8] = content.Load<Texture2D>("LevelUpCards\\card");

            //assigns the sprite of the card from the index number
            RandomCard();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, color, 0f, Origin, scale, SpriteEffects.None, layerDepth);

            if (!string.IsNullOrEmpty(text))
            {
                float x = (GetRectangle.X + GetRectangle.Width / 2) - textFont.MeasureString(text).X / 2;
                float y = (GetRectangle.Y + GetRectangle.Height / 3) - textFont.MeasureString(text).Y / 2;

                spriteBatch.DrawString(textFont, text, new Vector2(x, y), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth + 0.01f);
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

        public void RandomCard()
        {
            cardIndex = random.Next(1, 10);
            switch (cardIndex)
            {
                case 1: //lightsaber
                    sprite = sprites[0];
                    text = "Lightsaber";
                    break;
                case 2: //knife
                    sprite = sprites[1];
                    text = "Knife";
                    break;
                case 3: //magic missile
                    sprite = sprites[2];
                    text = "Magic Missile";
                    break;
                case 4: //attackDamage
                    sprite = sprites[3];
                    text = "Damage";
                    break;
                case 5: //attackSpeed
                    sprite = sprites[4];
                    text = "Attack Speed";
                    break;
                case 6: //maxHealth
                    sprite = sprites[5];
                    text = "Max Health";
                    break;
                case 7: //defense
                    sprite = sprites[6];
                    text = "Defense";
                    break;
                case 8: //movementSpeed
                    sprite = sprites[7];
                    text = "Movement Speed";
                    break;
                case 9: //itemCoolDown
                    sprite = sprites[8];
                    text = "Item Cooldown";
                    break;
                default: //make it throw an expection
                    break;
            }
        }
        #endregion
    }   
}
