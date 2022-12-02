using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.Menu
{
    public class MenuState : State
    {
        #region Fields 
        private Texture2D menuBackgroundTexture;
        private List<Button> buttons;
        private Button continueGameButton; 
        private Button newGameButton;
        private Button highscoreButton;
        private Button howToPlayButton; 
        private Button quitGameButton;
        private static Song menuMusic;
        #endregion

        #region Constructors
        public MenuState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            Vector2 buttonPosition = new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2 - GameWorld.GetScreenSize.Y / 6);

            Color buttonColor = Color.Purple;
            float spacer = 75; 

            continueGameButton = new Button(buttonPosition, "Resume Game", buttonColor);
            newGameButton = new Button(buttonPosition + new Vector2(0, spacer), "New Game", buttonColor);
            highscoreButton = new Button(buttonPosition + new Vector2(0, 2*spacer), "Highscore", buttonColor);
            howToPlayButton = new Button(buttonPosition + new Vector2(0, 3*spacer), "How to play", buttonColor); 
            quitGameButton = new Button(buttonPosition + new Vector2(0, 4*spacer), "Quit Game", buttonColor);

            buttons = new List<Button>() { continueGameButton, newGameButton, highscoreButton, howToPlayButton, quitGameButton };

            LoadContent(); 
        }
        #endregion

        #region Methods 
        public override void LoadContent()
        {
            menuBackgroundTexture = content.Load<Texture2D>("Menus\\MainScreen");

            // load background music 
            menuMusic = content.Load<Song>("Music\\Fatality Racer_demo");
            MediaPlayer.Play(menuMusic);
            MediaPlayer.IsRepeating = true;

            foreach (Button button in buttons)
            {
                button.LoadContent(content);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button button in buttons)
            {
                button.Update(gameTime);
            }

            if (continueGameButton.isClicked)
            {
                continueGameButton.isClicked = false;
                if (GameState.GetGameOver)
                {
                    GameWorld.HandleGameState = new GameState(content, graphicsDevice, game);
                    game.ChangeState(GameWorld.HandleGameState);
                    MediaPlayer.Stop();
                    GameState.RestartGameMusic(); 
                }
                else
                {
                    GameState.HandlePause = false;
                    game.ChangeState(GameWorld.HandleGameState);
                    MediaPlayer.Stop();
                    GameState.UnpauseGameMusic();

                    Debug.WriteLine("new: " + GameWorld.HandleGameState.GetHashCode());
                }
            }
            if (newGameButton.isClicked)
            {
                newGameButton.isClicked = false;
                GameState.HandlePause = false;
                GameWorld.HandleGameState = new GameState(content, graphicsDevice, game);
                game.ChangeState(GameWorld.HandleGameState);
                MediaPlayer.Stop();
                GameState.RestartGameMusic();

                Debug.WriteLine("old: " + GameWorld.HandleGameState.GetHashCode());
            }
            if (highscoreButton.isClicked)
            {
                highscoreButton.isClicked = false;
                game.ChangeState(GameWorld.HandleHighscoreState); 
            }
            if (howToPlayButton.isClicked)
            {
                howToPlayButton.isClicked = false;
                game.ChangeState(GameWorld.GetHowToState); 
            }
            if (quitGameButton.isClicked)
            {
                quitGameButton.isClicked = false;
                MediaPlayer.Stop();
                game.Exit();
            }
        }

        public static void RestartMenuMusic()
        {
            MediaPlayer.Play(menuMusic);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(menuBackgroundTexture, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2), null, Color.White, 0f, new Vector2(menuBackgroundTexture.Width / 2, menuBackgroundTexture.Height / 2), 1f, SpriteEffects.None, 0.1f);

            foreach (Button button in buttons)
            {
                button.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }
        #endregion
    }
}
