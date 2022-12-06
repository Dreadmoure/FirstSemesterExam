﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.Projectiles
{
    public class EnemyProjectile : Projectile
    {
        #region Constructors
        public EnemyProjectile(Vector2 enemyPosition, Vector2 playerPosition, float enemyAttackRange) : base(enemyPosition)
        {
            position = enemyPosition;
            speed = 1000f;
            attackDamage = 5f;
            attackRange = enemyAttackRange;
            layerDepth = 0.6f;
            

            // angle of projectile 
            float angle = MathF.Atan2(playerPosition.Y - enemyPosition.Y, playerPosition.X - enemyPosition.X);
            velocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
            rotation = angle; 
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];
            sprites[0] = content.Load<Texture2D>("Projectiles\\Projectile1");
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                GameWorld.soundEffects[6].CreateInstance().Play();
                other.TakeDamage(attackDamage);
                
                ShouldBeRemoved = true;
            }
        }
        #endregion
    }
}
