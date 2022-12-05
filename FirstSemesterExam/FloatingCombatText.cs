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
    /// <summary>
    /// A text that appears when something takes damage, displays the amount of damage done
    /// </summary>
    public class FloatingCombatText : GameObject
    {
        #region Fields
        private SpriteFont textFont;
        private string text;
        private float damage;
        private float timer;
        private float elapsedTime;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for FloatingCombatText which takes a position and the amount of damage done
        /// </summary>
        /// <param name="position">position is passed down from the object taking damage</param>
        /// <param name="damage">damage is passed down from the object taking damage</param>
        public FloatingCombatText(Vector2 position, float damage)
        {
            this.position = position;
            this.damage = damage;
            scale = 2f;
            //saves the damage as a string to be drawn later
            text = $"-{damage}";
            timer = 1f;
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            //sets the Y position relative to the position passed into the constructor
            //so it appears slightly above the object
            position.Y -= 55;
            textFont = content.Load<SpriteFont>("Fonts\\textFont");
        }

        public override void Update(GameTime gameTime)
        {
            //updates the position so it moves up on the screen
            position.Y--;
            
            //sets a timer which removes the object after a given time
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(elapsedTime > timer)
            {
                ShouldBeRemoved = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //sets the origin of the text so it appears centered
            float originX = textFont.MeasureString(text).X / 2;
            float originY = textFont.MeasureString(text).Y / 2;

            spriteBatch.DrawString(textFont, text, position, Color.Red, 0f, new Vector2(originX, originY), scale, SpriteEffects.None, 0.90f);
        }
        #endregion
    }
}
