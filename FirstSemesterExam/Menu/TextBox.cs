using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace FirstSemesterExam.Menu
{
    public class TextBox : Component
    {
        #region fields 
        private Texture2D textboxTexture;
        private SpriteFont textFont;
        private string text;

        private Vector2 position;
        private float scale;
        private float layer;

        private MouseState _currentMouse;
        private MouseState _previousMouse;
        public bool isActive;

        private KeyboardState currentKeyState;
        private KeyboardState previousKeyState;
        #endregion

        #region properties 
        private Vector2 GetSpriteSize
        {
            get { return new Vector2(textboxTexture.Width * scale, textboxTexture.Height * scale); }
        }
        private Vector2 GetOrigin
        {
            get { return new Vector2(textboxTexture.Width / 2, textboxTexture.Height / 2); }
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
        public string TextEntered
        {
            get 
            {
                if (string.Equals(text, "Enter name"))
                {
                    return "";
                }
                else
                {
                    return text;
                }
            }
        }
        #endregion

        #region Constructors
        public TextBox(Vector2 position, string text)
        {
            this.position = position;
            this.text = text;
            layer = 0.2f;
            scale = 1.5f;
            isActive = true; 
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            textboxTexture = content.Load<Texture2D>("Menus\\Button");
            textFont = content.Load<SpriteFont>("Fonts\\textFont");
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            // input text 
            UpdateTextInput(); 
        }

        private void UpdateTextInput()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            if(string.Equals(text, ""))
            {
                text = "Enter name"; 
            }

            if (currentKeyState != previousKeyState)
            {
                if(string.Equals(text, "Enter name"))
                {
                    text = ""; 
                }
                
                if (text.Length > 0 && currentKeyState.IsKeyDown(Keys.Back))
                {
                    text = text.Remove(text.Length - 1); 
                }

                if (text.Length < 12)
                {
                    foreach (var key in currentKeyState.GetPressedKeys())
                    {
                        string keyValue = key.ToString();

                        if (AllowedInput(keyValue) && keyValue.Length <= 1) {
                            text += keyValue;
                        }
                    }
                } 
            }
        }
        private bool AllowedInput(string s)
        {
            Regex regex = new Regex("[A-Z]");

            Match match = regex.Match(s);

            return match.Success; 
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textboxTexture, position, null, Color.White, 0f, GetOrigin, scale, SpriteEffects.None, layer);

            if (!string.IsNullOrEmpty(text))
            {
                float x = (GetRectangle.X + GetRectangle.Width / 2) - textFont.MeasureString(text).X / 2;
                float y = (GetRectangle.Y + GetRectangle.Height / 2) - textFont.MeasureString(text).Y / 2;

                spriteBatch.DrawString(textFont, text, new Vector2(x, y), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer + 0.01f);
            }
        }
        #endregion

    }
}
