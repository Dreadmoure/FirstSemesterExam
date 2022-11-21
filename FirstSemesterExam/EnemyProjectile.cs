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
    public class EnemyProjectile : Projectile
    {
        public EnemyProjectile(Vector2 enemyPosition, Vector2 enemyVelocity, float enemyAttackRange) : base(enemyPosition)
        {
            position = enemyPosition;
            speed = 500f;
            velocity = enemyVelocity; // same direction as the enemy 
            attackDamage = 5f;
            attackRange = enemyAttackRange; 
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];
            sprites[0] = content.Load<Texture2D>("Projectiles\\Projectile1"); 
        }

        public override void OnCollision(GameObject other)
        {
            //if(other is Player)
            //{
            //    ShouldBeRemoved = true; 
            //}
        }
    }
}
