﻿using Microsoft.Xna.Framework;
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
    /// <summary>
    /// Subclass of Component, Button - for clicking and switching states in menus 
    /// </summary>
    public class Button : Component
    {
        #region Fields 
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

        #region Properties 
        /// <summary>
        /// Property to get the size of the button sprite texture 
        /// </summary>
        private Vector2 GetSpriteSize
        {
            get { return new Vector2(buttonTexture.Width * scale, buttonTexture.Height * scale); }
        }
        /// <summary>
        /// Property to get the origin/the center of the button 
        /// </summary>
        private Vector2 GetOrigin
        {
            get { return new Vector2(buttonTexture.Width / 2, buttonTexture.Height / 2); }
        }
        /// <summary>
        /// Property to get the rectangle - used when mouse collides with button 
        /// </summary>
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

        #region Constructors
        /// <summary>
        /// Constructor for Button - sets its initial variables 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="text"></param>
        /// <param name="color">Color overlay</param>
        public Button(Vector2 position, string text, Color color)
        {
            this.position = position;
            this.text = text;
            this.color = color; 
            layer = 0.96f;
            scale = 1.5f; 
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            buttonTexture = content.Load<Texture2D>("Menus\\Button");
            textFont = content.Load<SpriteFont>("Fonts\\textFont"); 
        }

        public override void Update(GameTime gameTime)
        {
            // update mouse states 
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            // set rectangle for mouse position 
            Rectangle mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            // when mouse hovers over button 
            if (mouseRectangle.Intersects(GetRectangle))
            {
                ColorShift(); 

                // when button gets clicked 
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

        /// <summary>
        /// Makes the button shift its color opacity 
        /// </summary>
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, position, null, color, 0f, GetOrigin, scale, SpriteEffects.None, layer);

            if (!string.IsNullOrEmpty(text))
            {
                // calculate text position according to textFont and text length 
                float x = (GetRectangle.X + GetRectangle.Width / 2) - textFont.MeasureString(text).X / 2;
                float y = (GetRectangle.Y + GetRectangle.Height / 2) - textFont.MeasureString(text).Y / 2;
                // write the lext on top of the button tecture 
                spriteBatch.DrawString(textFont, text, new Vector2(x, y), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer + 0.01f);
            }
        }
        #endregion
    }
}
