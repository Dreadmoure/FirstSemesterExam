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
        public HornedGuy(Player player) : base(player)
        {
            health = 20f;
            speed = 100f;
            attackSpeed = 10f;
            attackDamage = 5;
            attackRange = 100f;
            animationSpeed = 3f;
            expValue = 5;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\HornGuy1");
            sprites[1] = content.Load<Texture2D>("Enemies\\HornGuy2");
        }
    }
}
