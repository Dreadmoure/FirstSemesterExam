using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.Menu
{
    public abstract class State
    {
        protected ContentManager content;
        protected GraphicsDevice graphicsDevice;
        protected GameWorld game; 

        public State(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game)
        {
            this.content = content;
            this.graphicsDevice = graphicsDevice;
            this.game = game; 
        }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch); 
    }
}
