using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam
{
    public abstract class Projectile : GameObject
    {
        protected float attackRange; 

        public override void LoadContent(ContentManager content)
        {
            // projectile sprites are handled in the classes that inherits from Projectile 
        }

        public override void Update(GameTime gameTime)
        {
            CheckIfOutsideBounds();

            if(attackRange != 0)
            {
                CheckIfOutsideRange();
            }

            Move(gameTime); 
        }

        private void CheckIfOutsideBounds()
        {
            Vector2 min = new Vector2(SpriteSize.X / 2, SpriteSize.Y / 2);
            Vector2 max = new Vector2(GameWorld.GetScreenSize.X - SpriteSize.X / 2, GameWorld.GetScreenSize.Y - SpriteSize.Y / 2); 
            if (position.X <= min.X || position.X >= max.X || position.Y <= min.Y || position.Y >= max.Y)
            {
                ShouldBeRemoved = true; 
            }
        }

        private void CheckIfOutsideRange()
        {
            Vector2 min = new Vector2(position.X - attackRange, position.Y - attackRange);
            Vector2 max = new Vector2(position.X + attackRange, position.Y + attackRange);
            if (position.X <= min.X || position.X >= max.X || position.Y <= min.Y || position.Y >= max.Y)
            {
                ShouldBeRemoved = true;
            }
        }
    }
}
