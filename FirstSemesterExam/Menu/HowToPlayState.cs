using FirstSemesterExam.HighScore;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.Menu
{
    public class HowToPlayState : State 
    {
        #region fields 
        private Texture2D menuBackgroundTexture;
        private Texture2D howToImage; 
        private List<Button> buttons;
        private Button backButton;
        #endregion

        public HowToPlayState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            backButton = new Button(new Vector2(100, 50), "Back", Color.Yellow);
            buttons = new List<Button>() { backButton };

            LoadContent();
        }

        #region methods 
        public override void LoadContent()
        {
            menuBackgroundTexture = content.Load<Texture2D>("Menus\\HowToPlayScreen");
            howToImage = content.Load<Texture2D>("Menus\\HowTo"); 

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

            spriteBatch.Draw(menuBackgroundTexture, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2), null, Color.White, 0f, new Vector2(menuBackgroundTexture.Width / 2, menuBackgroundTexture.Height / 2), 1f, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(howToImage, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 1.7f), null, Color.White, 0f, new Vector2(howToImage.Width/2, howToImage.Height/2), 1f, SpriteEffects.None, 0.2f); 

            foreach (Button button in buttons)
            {
                button.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }
        #endregion
    }
}
