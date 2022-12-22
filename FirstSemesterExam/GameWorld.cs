using FirstSemesterExam.Enemies;
using FirstSemesterExam.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FirstSemesterExam
{
    /// <summary>
    /// Class GameWorld - used as State handler 
    /// </summary>
    public class GameWorld : Game
    {
        #region Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static Vector2 screenSize;

        private State _currentState;
        private State _nextState;
        private static State menuState;
        private static State howToPlayState; 
        private static State highscoreState;
        private static State gameState;

        public static List<SoundEffect> soundEffects;
        #endregion

        #region Properties
        /// <summary>
        /// Property to get the menuState 
        /// </summary>
        public static State GetMenuState
        {
            get { return menuState; }
        }
        /// <summary>
        /// Property to get the HowToPlayState 
        /// </summary>
        public static State GetHowToState
        {
            get { return howToPlayState; }
        }
        /// <summary>
        /// Property for both getting and setting the HighscoreState 
        /// Get - returns the current local leaderboard 
        /// Set - after GameState gameOver, loads new highscore file, and hence a new HighscoreState 
        /// </summary>
        public static State HandleHighscoreState
        {
            get { return highscoreState; }
            set { highscoreState = value; }
        }
        /// <summary>
        /// Property for getting and setting the GameState 
        /// Get - the game that was previously exited through GameState pauseMenu and is now paused 
        /// Set - instantiates a new game, hence a new GameState 
        /// </summary>
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
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for the GameWorld
        /// </summary>
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            soundEffects = new List<SoundEffect>();

            IsMouseVisible = true;

            // set screensize 
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;

            // set screen fullscreen 
            _graphics.IsFullScreen = false;

            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method for initializing objects into the gameworld
        /// </summary>
        protected override void Initialize()
        {
            this.Window.Title = "Survive Us";

            // set initial states  
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

            //Our content sound files from our SoundEffect List
            //Player SFX
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Player and Weapons/LaserSound")); //Lasergun shot
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Player and Weapons/Retro Swooosh 02")); //
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Player and Weapons/SwordOn")); // Lightsaber spawn
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Player and Weapons/Retro Swooosh 16")); //
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Player and Weapons/MagicMissile")); // Magic Missile Spawn
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Player and Weapons/SaberSound")); // knife
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Player and Weapons/Retro Negative Short 23")); // player getting hit
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Player and Weapons/Retro PowerUP StereoUP 05")); // Level up
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Player and Weapons/Retro PowerUP 09")); // Power up
            //Enemy SFX
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Monster/EnemyDeath")); // Turned and Horned guy death
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Monster/PainSound"));// Blob Monster and slime death
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Monster/Retro Impact Metal 36"));// Robot Death
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Monster/Retro Impact Punch Hurt 01"));// robot Shoot
            //Menu SFX
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Menu/Retro8")); // Menu Buttons for everything but "back" and "exit" buttons
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Menu/Retro9")); // Sound for "back" and "exit" buttons
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/Menu/Retro5")); // Hoverover sound effect
            //GameOver SFX
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/GameOver/GameOver")); // Gameover
        }

        /// <summary>
        /// Method which updates the content within, by default each frame
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            // checks if a new state is available 
            if (_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }

            _currentState.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Change the state to property: state 
        /// </summary>
        /// <param name="state">The next state</param>
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

            // draws the currentstate 
            _currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
        #endregion
    }
}