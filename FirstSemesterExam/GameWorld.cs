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
        private static State menuState;
        private static State howToPlayState; 
        private static State highscoreState;
        private static State gameState; 

        public static State GetMenuState
        {
            get { return menuState; }
        }
        public static State GetHowToState
        {
            get { return howToPlayState; }
        }
        public static State HandleHighscoreState
        {
            get { return highscoreState; }
            set { highscoreState = value; }
        }
        public static State HandleGameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

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
            //_graphics.PreferredBackBufferWidth = 900;
            //_graphics.PreferredBackBufferHeight = 800;

            _graphics.IsFullScreen = false;

            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        /// <summary>
        /// Method for initializing objects into the gameworld
        /// </summary>
        protected override void Initialize()
        {
            this.Window.Title = "Survive Us";

            menuState = new MenuState(Content, GraphicsDevice, this);
            howToPlayState = new HowToPlayState(Content, GraphicsDevice, this); 
            highscoreState = new HighscoreState(Content, GraphicsDevice, this);

            base.Initialize();
        }

        /// <summary>
        /// Method for loading content so it is ready to be used
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = menuState;
            _currentState.LoadContent();
            _nextState = null;
        }

        /// <summary>
        /// Method which updates the content within, by default each frame
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_nextState != null)
            {
                _currentState = _nextState;

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