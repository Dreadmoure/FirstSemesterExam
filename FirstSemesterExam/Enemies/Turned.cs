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
            health = 10;
            speed = 25f;
            attackSpeed = 20f;
            attackRange = 10f;
            animationSpeed = 6f;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\Turned1");
            sprites[1] = content.Load<Texture2D>("Enemies\\Turned2");
        }
    }
}
