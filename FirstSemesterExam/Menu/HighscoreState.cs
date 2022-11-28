﻿using Microsoft.Xna.Framework.Content;
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
    public class HighscoreState : State
    {
        #region fields 
        private Texture2D menuBackgroundTexture;
        private Texture2D highscoreTable; 
        private SpriteFont textFont; 
        private List<Button> buttons;
        private Button backButton;
        private Button nextScoresButton;
        private Button prevScoresButton; 
        private Highscore highscore = new Highscore();
        private int indexMin;
        private int indexMax;
        private int indexStart;
        private int indexEnd;
        private int numberOfViewedScores; 
        #endregion

        public HighscoreState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            float buttonLayer = 0.2f;
            float buttonScale = 1f;

            backButton = new Button(new Vector2(100, 50), "Back", buttonLayer, buttonScale);
            prevScoresButton = new Button(new Vector2(GameWorld.GetScreenSize.X / 2 - GameWorld.GetScreenSize.X / 5, GameWorld.GetScreenSize.Y / 2), "Prev", buttonLayer, buttonScale);
            nextScoresButton = new Button(new Vector2(GameWorld.GetScreenSize.X / 2 + GameWorld.GetScreenSize.X / 5, GameWorld.GetScreenSize.Y / 2), "Next", buttonLayer, buttonScale); 
            
            buttons = new List<Button>() { backButton, nextScoresButton, prevScoresButton };

            LoadContent();

            Highscore highscore = new Highscore(); 
        }

        #region methods 
        public override void LoadContent()
        {
            menuBackgroundTexture = content.Load<Texture2D>("Menus\\HighscoreScreen");
            highscoreTable = content.Load<Texture2D>("Menus\\HighscoreTableBG"); 
            textFont = content.Load<SpriteFont>("Fonts\\textFont");

            numberOfViewedScores = 10; 
            indexMin = 0;
            indexMax = highscore.Scores.Count;
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

            //Debug.WriteLine("start: " + indexStart + " end: " + indexEnd);

            if (backButton.isClicked)
            {
                backButton.isClicked = false;
                game.ChangeState(GameWorld.GetMenuState);
            }
            if (nextScoresButton.isClicked)
            {
                nextScoresButton.isClicked = false;

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

            List<Score> scores = highscore.Scores; 
            Vector2 nameTextPosition = new Vector2(GameWorld.GetScreenSize.X / 2 - highscoreTable.Width + 15, GameWorld.GetScreenSize.Y / 2 - highscoreTable.Height / 2);
            Vector2 scoreTextPosition = new Vector2(GameWorld.GetScreenSize.X / 2 + highscoreTable.Width - 15, GameWorld.GetScreenSize.Y / 2 - highscoreTable.Height / 2); 

            spriteBatch.DrawString(textFont, "Name", new Vector2(GameWorld.GetScreenSize.X / 2 - highscoreTable.Width/2, GameWorld.GetScreenSize.Y / 2 - highscoreTable.Height / 1.5f), Color.White, 0f, new Vector2(textFont.MeasureString("Name").X/2, textFont.MeasureString("Name").Y/2), 1f, SpriteEffects.None, 0.9f);
            spriteBatch.DrawString(textFont, "Score", new Vector2(GameWorld.GetScreenSize.X / 2 + highscoreTable.Width/2, GameWorld.GetScreenSize.Y / 2 - highscoreTable.Height / 1.5f), Color.White, 0f, new Vector2(textFont.MeasureString("Score").X/2, textFont.MeasureString("Score").Y/2), 1f, SpriteEffects.None, 0.9f);
            for (int i = indexStart; i < indexEnd; i++)
            {
                float offsetScorePositionX = textFont.MeasureString(scores[i].score.ToString()).X;
                float textHeight = i % 10 * textFont.MeasureString("Text").Y; 
                spriteBatch.DrawString(textFont, scores[i].name, nameTextPosition + new Vector2(0, textHeight), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f); 
                spriteBatch.DrawString(textFont, scores[i].score.ToString(), scoreTextPosition + new Vector2(0, textHeight), Color.White, 0f, new Vector2(offsetScorePositionX, 0), 1f, SpriteEffects.None, 0.9f);
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
