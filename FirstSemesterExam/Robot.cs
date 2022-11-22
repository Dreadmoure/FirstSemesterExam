using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam
{
    public class Robot : Enemy
    {
        public Robot() : base()
        {
            health = 15; 
            speed = 5f;
            attackSpeed = 50f;
            attackRange = 1000f; 
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\Robot1");
            sprites[1] = content.Load<Texture2D>("Enemies\\Robot2");
        }
    }
}
