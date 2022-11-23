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
            health = 5f;
            speed = 5f;
            attackSpeed = 2f;
            attackRange = 25f;
            animationSpeed = 2f;
            expValue = 5;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\BlobMonster1");
            sprites[1] = content.Load<Texture2D>("Enemies\\BlobMonster2");
        }
    }
}
