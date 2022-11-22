using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam
{
    public class PlayerProjectile : Projectile
    {
        public PlayerProjectile(Vector2 playerPosition) : base(playerPosition)
        {
            position = playerPosition;
            attackDamage = 10f; 
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];
            sprites[0] = content.Load<Texture2D>("");
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                ShouldBeRemoved = true;
            }
        }
    }
}
