﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.Menu
{
    public class MenuState : State
    {
        #region fields 
        private Texture2D menuBackgroundTexture;
        private List<Button> buttons;
        private Button continueGameButton; 
        private Button newGameButton;
        private Button highscoreButton; 
        private Button quitGameButton;
        #endregion

        public MenuState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            float buttonLayer = 0.2f;
            float buttonScale = 6f;

            continueGameButton = new Button(new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2 - GameWorld.GetScreenSize.Y / 6), "Resume Game", buttonLayer, buttonScale); 
            newGameButton = new Button(new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2 -50), "New Game", buttonLayer, buttonScale);
            highscoreButton = new Button(new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2 +50), "Highscore", buttonLayer, buttonScale); 
            quitGameButton = new Button(new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2 + GameWorld.GetScreenSize.Y / 6), "Quit Game", buttonLayer, buttonScale);

            buttons = new List<Button>() { continueGameButton, newGameButton, highscoreButton, quitGameButton };

            LoadContent(); 
        }

        #region methods 
        public override void LoadContent()
        {
            menuBackgroundTexture = content.Load<Texture2D>("Menus\\background");

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
                }
                else
                {
                    GameState.HandlePause = false;
                    game.ChangeState(GameWorld.HandleGameState);
                }
            }
            if (newGameButton.isClicked)
            {
                newGameButton.isClicked = false;
                GameState.HandlePause = false;
                GameWorld.HandleGameState = new GameState(content, graphicsDevice, game);
                game.ChangeState(GameWorld.HandleGameState);
            }
            if (highscoreButton.isClicked)
            {
                highscoreButton.isClicked = false;
                game.ChangeState(GameWorld.HandleHighscoreState); 
            }
            if (quitGameButton.isClicked)
            {
                quitGameButton.isClicked = false;
                game.Exit();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(menuBackgroundTexture, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2), null, Color.White, 0f, new Vector2(menuBackgroundTexture.Width / 2, menuBackgroundTexture.Height / 2), 6f, SpriteEffects.None, 0.1f);

            foreach (Button button in buttons)
            {
                button.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }
        #endregion
    }
}
