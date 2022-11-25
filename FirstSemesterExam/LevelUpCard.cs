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
    public class LevelUpCard
    {
        #region Fields
        private Vector2 position;
        private float scale;
        private Texture2D sprite;
        private Texture2D[] sprites;
        private float layerDepth;
        private string text;
        private SpriteFont textFont;
        private int index;
        
        private Random random = new Random();

        private MouseState _currentMouse;
        private MouseState _previousMouse;
        public bool isClicked;
        private Color color = new Color(255, 255, 255, 255);
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

        private Vector2 GetSpriteSize
        {
            get
            {
                return new Vector2(sprite.Width * scale, sprite.Height * scale);
            }
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
            index = random.Next(0,7);
            layerDepth = 0.99f;
            scale = 3f;
            

            


            
        }
        #endregion

        #region Methods
        public void Update(GameTime gameTime)
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

        public void LoadContent(ContentManager content)
        {
            textFont = content.Load<SpriteFont>("Fonts\\textFont");

            sprites = new Texture2D[7];
            sprites[0] = content.Load<Texture2D>("Enemies\\testEnemy"); //needs to ba changed
            sprites[1] = content.Load<Texture2D>("Enemies\\testEnemy"); //needs to ba changed
            sprites[2] = content.Load<Texture2D>("Enemies\\testEnemy"); //needs to ba changed
            sprites[3] = content.Load<Texture2D>("Enemies\\testEnemy"); //needs to ba changed
            sprites[5] = content.Load<Texture2D>("Enemies\\testEnemy"); //needs to ba changed
            sprites[6] = content.Load<Texture2D>("Enemies\\testEnemy"); //needs to ba changed

            //assigns the sprite of the card from the index number
            switch (index)
            {
                case 0:
                    sprite = sprites[0];
                    text = "1";
                    break;
                case 1:
                    sprite = sprites[1];
                    text = "2";
                    break;
                case 2:
                    sprite = sprites[2];
                    text = "3";
                    break;
                case 3:
                    sprite = sprites[3];
                    text = "4";
                    break;
                case 4:
                    sprite = sprites[4];
                    text = "5";
                    break;
                case 5:
                    sprite = sprites[5];
                    text = "6";
                    break;
                case 6:
                    sprite = sprites[6];
                    text = "7";
                    break;
                default: //make it throw an expection
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
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
        #endregion
    }
}
