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
    public class BlobMonster : Enemy
    {
        public BlobMonster(Player player) : base(player)
        {
            health = 5;
            speed = 5f;
            attackSpeed = 2f;
            attackRange = 25f;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];
            sprites[0] = content.Load<Texture2D>("Enemies\\testEnemy");
        }
    }
}
