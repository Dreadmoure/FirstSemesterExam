using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.Menu
{
    public class Button : Component
    {
        #region fields 
        private Texture2D buttonTexture;
        private SpriteFont textFont;
        private string text;

        private Vector2 position;
        private float scale;
        private float layer;
        private Color color;
        private bool colorShiftDown; 

        private MouseState _currentMouse;
        private MouseState _previousMouse;
        public bool isClicked;
        #endregion

        #region properties 
        private Vector2 GetSpriteSize
        {
            get { return new Vector2(buttonTexture.Width * scale, buttonTexture.Height * scale); }
        }
        private Vector2 GetOrigin
        {
            get { return new Vector2(buttonTexture.Width / 2, buttonTexture.Height / 2); }
        }
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
        #endregion

        public Button(Vector2 position, string text, Color color)
        {
            this.position = position;
            this.text = text;
            this.color = color; 
            layer = 0.96f;
            scale = 1.5f; 
        }

        public override void LoadContent(ContentManager content)
        {
            buttonTexture = content.Load<Texture2D>("Menus\\Button");
            textFont = content.Load<SpriteFont>("Fonts\\textFont"); 
        }

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
            else if(color.A < 255)
            {
                color.A += 3; 
            }
        }

        private void ColorShift()
        {
            
            if (color.A == 255)
            {
                GameWorld.soundEffects[15].Play();
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, position, null, color, 0f, GetOrigin, scale, SpriteEffects.None, layer);

            if (!string.IsNullOrEmpty(text))
            {
                float x = (GetRectangle.X + GetRectangle.Width / 2) - textFont.MeasureString(text).X / 2;
                float y = (GetRectangle.Y + GetRectangle.Height / 2) - textFont.MeasureString(text).Y / 2;

                spriteBatch.DrawString(textFont, text, new Vector2(x, y), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer + 0.01f);
            }
        }
    }
}
