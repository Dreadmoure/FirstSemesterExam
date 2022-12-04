using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam
{
    public class FloatingCombatText : GameObject
    {
        private SpriteFont textFont;
        private string text;
        private float damage;
        private float timer;
        private float elapsedTime;

        #region Constructors
        public FloatingCombatText(Vector2 position, float damage)
        {
            this.position = position;
            this.damage = damage;
            scale = 2f;
            text = $"-{damage}";
            speed = 100f;
            timer = 1f;
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            position.Y -= 55;
            textFont = content.Load<SpriteFont>("Fonts\\textFont");
        }

        public override void Update(GameTime gameTime)
        {
            position.Y--;
            
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(elapsedTime > timer)
            {
                ShouldBeRemoved = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float originX = textFont.MeasureString(text).X / 2;
            float originY = textFont.MeasureString(text).Y / 2;

            spriteBatch.DrawString(textFont, text, position, Color.Red, 0f, new Vector2(originX, originY), scale, SpriteEffects.None, 0.90f);
        }
        #endregion
    }
}
