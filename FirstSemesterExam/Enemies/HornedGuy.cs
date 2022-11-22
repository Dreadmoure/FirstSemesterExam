using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.Enemies
{
    public class HornedGuy : Enemy
    {
        public HornedGuy() : base()
        {
            health = 7;
            speed = 10f;
            attackSpeed = 10f;
            attackRange = 50f;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\HornGuy1");
            sprites[1] = content.Load<Texture2D>("Enemies\\HornGuy2");
        }
    }
}
