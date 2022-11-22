using FirstSemesterExam.Enemies;
using FirstSemesterExam.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam
{
    public class PlayerProjectile : Projectile
    {
        public PlayerProjectile(Vector2 position, Vector2 velocity) : base(position)
        {
            this.velocity = velocity;
            this.position = position;
            attackDamage = 10f;
            speed = 1000;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];
            sprites[0] = content.Load<Texture2D>("Projectile1");
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
