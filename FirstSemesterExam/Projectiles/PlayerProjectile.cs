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
        public PlayerProjectile(Vector2 position, Vector2 velocity, float rotation, float attackDamage) : base(position)
        {
            this.velocity = velocity;
            this.position = position;
            this.rotation = rotation;
            this.attackDamage = attackDamage;
            speed = 1000;
            layerDepth = 0.6f;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];
            sprites[0] = content.Load<Texture2D>("Projectiles\\Projectile1");
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                
                other.TakeDamage(attackDamage);
                ShouldBeRemoved = true;
            }
        }
    }
}
