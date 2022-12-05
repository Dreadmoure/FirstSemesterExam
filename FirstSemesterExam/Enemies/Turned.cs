using FirstSemesterExam.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.Enemies
{
    public class Turned : Enemy
    {
        public Turned(Player player) : base(player)
        {
            health = 10f;
            speed = 250f;
            attackSpeed = 20f;
            attackRange = 25f;
            animationSpeed = 6f;
            expValue = 5;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\Turned1");
            sprites[1] = content.Load<Texture2D>("Enemies\\Turned2");
        }
        public override void Update(GameTime gameTime)
        {
            if (health <= 0)
            {
                GameWorld.soundEffects[9].CreateInstance().Play();
            }

            base.Update(gameTime);
        }
    }
}
