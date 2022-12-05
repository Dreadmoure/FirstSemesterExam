﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using FirstSemesterExam.Projectiles;
using FirstSemesterExam.Menu;
using Color = Microsoft.Xna.Framework.Color;

namespace FirstSemesterExam.Enemies
{
    public abstract class Enemy : GameObject
    {
        #region Fields
        protected Random random = new Random();
        private float attackTime;
        protected float attackRange;
        private Player player;
        protected int expValue;
        protected float iFrames1;
        protected float iFrames2;
        private bool canBeDamagedByLs;
        private bool canBeDamagedByTK;
        enum Edge { Upper, Lower, Left, Right }
        #endregion

        #region Properties
        public bool CanBeDamagedByLs
        {
            get { return canBeDamagedByLs; }
            set { canBeDamagedByLs = value; }
        }

        public bool CanBeDamagedByTK
        {
            get { return canBeDamagedByTK; }
            set { canBeDamagedByTK = value; }
        }
        #endregion

        #region Constructors
        public Enemy(Player player)
        {
            this.player = player;
            animationSpeed = 1f;
            spriteEffects = SpriteEffects.None;
            layerDepth = 0.5f;
            //rotation = 0.01f;
            // set initial position randomly 
            switch (random.Next(0, 4)) // 0 to 3 
            {
                case int n when n == (int)Edge.Upper: // 0
                    position.X = random.NextFloat(0, GameWorld.GetScreenSize.X);
                    position.Y = 0;
                    break;
                case int n when n == (int)Edge.Lower: // 1
                    position.X = random.NextFloat(0, GameWorld.GetScreenSize.X);
                    position.Y = GameWorld.GetScreenSize.Y;
                    break;
                case int n when n == (int)Edge.Left: // 2
                    position.X = 0;
                    position.Y = random.NextFloat(0, GameWorld.GetScreenSize.Y);
                    break;
                case int n when n == (int)Edge.Right: // 3
                    position.X = GameWorld.GetScreenSize.X;
                    position.Y = random.NextFloat(0, GameWorld.GetScreenSize.Y);
                    break;
                default:
                    // throw exception 
                    break;
            }
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            // sprites are handled in the classes that inherits from Enemy 
        }

        public override void Update(GameTime gameTime)
        {
            HandlePosition();
            Move(gameTime);
            Animate(gameTime);

            if (IsInRange())
            {
                // attack when ready 
                attackTime += (float)gameTime.ElapsedGameTime.TotalSeconds * attackSpeed;
                if (attackTime > 10f) // TODO: check if it should be lower 
                {
                    Attack();
                    attackTime = 0f;
                }
            }

            if (HasJustBeenHit)
            {
                hitTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (hitTimer <= 0.1f)
                {
                    color = Color.Red;
                }
                else
                {
                    hitTimer = 0;
                    color = Color.White;
                    HasJustBeenHit = false; 
                }
            }

            if (health <= 0)
            {
                Die();
            }

            if (!canBeDamagedByLs)
            {
                iFrames1 -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (iFrames1 <= 0)
                {
                    canBeDamagedByLs = true;
                    iFrames1 = 0.3f;
                }
            }
            if (!canBeDamagedByTK)
            {
                iFrames2 -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (iFrames2 <= 0)
                {
                    canBeDamagedByTK = true;
                    iFrames2 = 0.3f;
                }
            }
        }

        private bool IsInRange()
        {
            float distance = (float)Math.Sqrt((player.GetPosition.X - position.X) * (player.GetPosition.X - position.X) + (player.GetPosition.Y - position.Y) * (player.GetPosition.Y - position.Y));

            return distance <= attackRange; 
        }

        private void Die()
        {
            GameState.CountEnemyKill(); 

            float rnd = random.Next(100);
            if (rnd < 10)
            {
                GameState.InstantiateGameObject(new PickUp(position));
            }
            player.Exp += expValue;
            ShouldBeRemoved = true;
        }

        private void HandlePosition()
        {
            velocity = (player.GetPosition - position);
            velocity.Normalize();
            if (velocity.X > 0)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }

        }

        public override void TakeDamage(float damage)
        {
            hasJustBeenHit = true;
            health -= damage;
            GameState.InstantiateGameObject(new FloatingCombatText(position, damage));
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                other.TakeDamage(attackDamage);
            }
        }

        public virtual void Attack()
        {
            GameState.InstantiateGameObject(new EnemyProjectile(position, player.GetPosition, attackRange));
        }
        #endregion
    }
}
