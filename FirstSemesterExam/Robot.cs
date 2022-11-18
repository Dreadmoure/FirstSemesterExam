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
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];
            sprites[0] = content.Load<Texture2D>("Enemies\\testEnemy");
        }

        public override void Attack()
        {
            
        }
    }
}
