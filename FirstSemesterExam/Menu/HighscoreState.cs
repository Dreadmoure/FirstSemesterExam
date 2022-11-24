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
    public class HighscoreState : State
    {
        #region fields 
        private Texture2D menuBackgroundTexture;
        private SpriteFont textFont; 
        private List<Button> buttons;
        private Button backButton;
        private Highscore highscore = new Highscore(); 
        #endregion

        public HighscoreState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            float buttonLayer = 0.2f;
            float buttonScale = 6f;

            backButton = new Button(new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2 - GameWorld.GetScreenSize.Y / 6), "Back", buttonLayer, buttonScale);
            
            buttons = new List<Button>() { backButton };

            LoadContent();

            Highscore highscore = new Highscore(); 
        }

        #region methods 
        public override void LoadContent()
        {
            menuBackgroundTexture = content.Load<Texture2D>("Menus\\background");
            textFont = content.Load<SpriteFont>("Fonts\\textFont"); 

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
                game.ChangeState(GameWorld.GetMenuState);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(menuBackgroundTexture, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2), null, Color.White, 0f, new Vector2(menuBackgroundTexture.Width / 2, menuBackgroundTexture.Height / 2), 6f, SpriteEffects.None, 0.1f);

            //foreach (Score score in highscore.Scores)
            //{
            //    spriteBatch.DrawString(textFont, score.name + score.score, Vector2.Zero, Color.White);
            //}

            List<Score> scores = highscore.Scores; 
            Vector2 offsetScoreText = new Vector2(GameWorld.GetScreenSize.X/2, GameWorld.GetScreenSize.Y/2);
            //Vector2 offsetScoreText = new Vector2(0, 0);
            for (int i = 0; i < 10; i++)
            {
                spriteBatch.DrawString(textFont, scores[i].name, offsetScoreText + new Vector2(0, i * 15), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f); 
                spriteBatch.DrawString(textFont, scores[i].score.ToString(), offsetScoreText + new Vector2(100, i * 15), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
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
