using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using FirstSemesterExam.HighScore;
using System.Diagnostics;

namespace FirstSemesterExam.Menu
{
    /// <summary>
    /// Subclass of State, HighscoreState - reached through MenuState, shows the local highscore leaderboard 
    /// </summary>
    public class HighscoreState : State
    {
        #region fields 
        private Texture2D menuBackgroundTexture;
        private Texture2D highscoreTable; 
        private SpriteFont textFont; 

        // list of buttons 
        private List<Button> buttons;
        private Button backButton;
        private Button nextScoresButton;
        private Button prevScoresButton; 

        // instance of highscore 
        private Highscore highscore = new Highscore();

        // local variables for changing the viewed scores 
        private int indexMin;
        private int indexMax;
        private int indexStart;
        private int indexEnd;
        private int numberOfViewedScores;
        #endregion

        #region Constructors 
        /// <summary>
        /// Constructor for HighscoreState - sets the buttons 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="game"></param>
        public HighscoreState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            Color buttonColor = Color.Green;

            backButton = new Button(new Vector2(150, 60), "Back", buttonColor);
            prevScoresButton = new Button(new Vector2(GameWorld.GetScreenSize.X / 2 - GameWorld.GetScreenSize.X / 5, GameWorld.GetScreenSize.Y / 2), "Prev", buttonColor);
            nextScoresButton = new Button(new Vector2(GameWorld.GetScreenSize.X / 2 + GameWorld.GetScreenSize.X / 5, GameWorld.GetScreenSize.Y / 2), "Next", buttonColor); 
            
            buttons = new List<Button>() { backButton, nextScoresButton, prevScoresButton };

            LoadContent();

            Highscore highscore = new Highscore(); 
        }
        #endregion

        #region methods 
        public override void LoadContent()
        {
            menuBackgroundTexture = content.Load<Texture2D>("Menus\\HighscoreScreen");
            highscoreTable = content.Load<Texture2D>("Menus\\HighscoreTableBG"); 
            textFont = content.Load<SpriteFont>("Fonts\\textFont");

            // set initial min and max index, and the max numberOfViewedScores on screen 
            numberOfViewedScores = 10; 
            indexMin = 0;
            indexMax = highscore.GetScores.Count;
            indexStart = indexMin;
            if (indexMax < numberOfViewedScores)
            {
                indexEnd = indexMax;
            }
            else
            {
                indexEnd = numberOfViewedScores;
            }

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

            if (backButton.isClicked)
            {
                backButton.isClicked = false;

                // go back to MenuState 
                game.ChangeState(GameWorld.GetMenuState);
            }
            if (nextScoresButton.isClicked)
            {
                nextScoresButton.isClicked = false;

                // move to the next scores on the highscore list if possible 
                if (indexMax <= numberOfViewedScores-1)
                {
                    indexStart = indexMin;
                    indexEnd = indexMax;
                }
                else
                {
                    if (indexMax - indexEnd >= numberOfViewedScores-1)
                    {
                        indexStart += numberOfViewedScores;
                        indexEnd = indexStart + numberOfViewedScores;
                    }
                    else if (indexMax - indexEnd < numberOfViewedScores - 1 && indexMax - indexStart < numberOfViewedScores - 1)
                    {
                        indexEnd = indexMax;
                    }
                    else if(indexMax - indexEnd < numberOfViewedScores-1)
                    {
                        indexStart += numberOfViewedScores;
                        indexEnd = indexMax;
                    }
                    
                }
            }
            if (prevScoresButton.isClicked)
            {
                prevScoresButton.isClicked = false;

                // move to the previous scores on the highscore list, if possible 
                if (indexMax <= numberOfViewedScores-1)
                {
                    indexStart = indexMin;
                    indexEnd = indexMax;
                }
                else
                {
                    if (indexMin + indexStart >= numberOfViewedScores-1)
                    {
                        indexStart -= numberOfViewedScores;
                        indexEnd = indexStart + numberOfViewedScores;
                    }
                    else if (indexMin + indexStart < numberOfViewedScores - 1 && indexMax <= numberOfViewedScores - 1)
                    {
                        indexStart = indexMin;
                        indexEnd = indexMax;
                    }
                    else if(indexMin + indexStart < numberOfViewedScores - 1)
                    {
                        indexStart = indexMin;
                        indexEnd = indexStart + numberOfViewedScores;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(menuBackgroundTexture, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2), null, Color.White, 0f, new Vector2(menuBackgroundTexture.Width / 2, menuBackgroundTexture.Height / 2), 1f, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(highscoreTable, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2 +50), null, Color.White, 0f, new Vector2(highscoreTable.Width/2, highscoreTable.Height/2), 2f, SpriteEffects.None, 0.2f); 

            List<Score> scores = highscore.GetScores; 
            Vector2 nameTextPosition = new Vector2(GameWorld.GetScreenSize.X / 2 - highscoreTable.Width + 15, GameWorld.GetScreenSize.Y / 2 - highscoreTable.Height / 2);
            Vector2 scoreTextPosition = new Vector2(GameWorld.GetScreenSize.X / 2 + highscoreTable.Width - 15, GameWorld.GetScreenSize.Y / 2 - highscoreTable.Height / 2); 

            spriteBatch.DrawString(textFont, "Name", new Vector2(GameWorld.GetScreenSize.X / 2 - highscoreTable.Width/2, GameWorld.GetScreenSize.Y / 2 - highscoreTable.Height / 1.5f), Color.White, 0f, new Vector2(textFont.MeasureString("Name").X/2, textFont.MeasureString("Name").Y/2), 1f, SpriteEffects.None, 0.9f);
            spriteBatch.DrawString(textFont, "Score", new Vector2(GameWorld.GetScreenSize.X / 2 + highscoreTable.Width/2, GameWorld.GetScreenSize.Y / 2 - highscoreTable.Height / 1.5f), Color.White, 0f, new Vector2(textFont.MeasureString("Score").X/2, textFont.MeasureString("Score").Y/2), 1f, SpriteEffects.None, 0.9f);
            for (int i = indexStart; i < indexEnd; i++)
            {
                float offsetScorePositionX = textFont.MeasureString(scores[i]._Score.ToString()).X;
                float textHeight = i % 10 * textFont.MeasureString("Text").Y * 2; 
                spriteBatch.DrawString(textFont, scores[i].Name, nameTextPosition + new Vector2(0, textHeight), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f); 
                spriteBatch.DrawString(textFont, scores[i]._Score.ToString(), scoreTextPosition + new Vector2(0, textHeight), Color.White, 0f, new Vector2(offsetScorePositionX, 0), 1f, SpriteEffects.None, 0.9f);
            }

            foreach (Button button in buttons)
            {
                button.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }
        #endregion
    }
}
