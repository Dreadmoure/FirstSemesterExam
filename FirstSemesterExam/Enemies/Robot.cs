using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.Enemies
{
    public class Robot : Enemy
    {
        public Robot(Player player) : base(player)
        {
            health = 15f;
            speed = 5f;
            attackSpeed = 50f;
            attackRange = 1000f;
            animationSpeed = 4f;
            expValue = 4;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\Robot1");
            sprites[1] = content.Load<Texture2D>("Enemies\\Robot2");
        }
    }
}
