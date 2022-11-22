using FirstSemesterExam.Enemies;
using FirstSemesterExam.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FirstSemesterExam
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static Vector2 screenSize;

        private State _currentState;
        private State _nextState;

        /// <summary>
        /// Property for getting the value of the screen size, used in positioning objects and handle the boundaries of the screen
        /// </summary>
        public static Vector2 GetScreenSize
        {
            get { return screenSize; }
        }

        /// <summary>
        /// Constructor for the GameWorld
        /// </summary>
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // set screensize 
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = true;
            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        /// <summary>
        /// Method for initializing objects into the gameworld
        /// </summary>
        protected override void Initialize()
        {
            this.Window.Title = "Survive Us";

            base.Initialize();
        }

        /// <summary>
        /// Method for loading content so it is ready to be used
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(Content, GraphicsDevice, this);
            _currentState.LoadContent();
            _nextState = null;
        }

        /// <summary>
        /// Method which updates the content within, by default each frame
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();

                _nextState = null;
            }

            _currentState.Update(gameTime);

            base.Update(gameTime);
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        /// <summary>
        /// Method for Drawing assets to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}