﻿using FirstSemesterExam.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace FirstSemesterExam.Enemies
{
    /// <summary>
    /// Subclass of Enemy, Slime - can merge into a BlobMonster 
    /// </summary>
    internal class Slime : Enemy
    {
        #region Fields
        private Player player;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for spawning enemy at edge 
        /// </summary>
        /// <param name="player"></param>
        public Slime(Player player) : base(player)
        {
            health = 20;
            speed = 100f;
            attackSpeed = 15f;
            attackDamage = 5;
            attackRange = 25f;
            animationSpeed = 3f;
            expValue = 2;
            this.player = player;
        }
        /// <summary>
        /// Constructor overload for spawning new Slime at position 
        /// Used when a BlobMonster dies and splits into slimes 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="parentPosition"></param>
        public Slime(Player player, Vector2 parentPosition) : base(player)
        {
            health = 20f;
            speed = 100f;
            attackSpeed = 10f;
            attackRange = 10f;
            animationSpeed = 3f;
            expValue = 2;
            this.player = player;

            // set random position around the dead BlobMonster position 
            Vector2 offsetPosition = new Vector2(random.Next(-100, 100), random.Next(-100, 100));
            position = parentPosition + offsetPosition; 
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            if (health <= 0)
            {
                GameWorld.soundEffects[10].Play(volume: 0.5f, pitch: 0.0f, pan: 0.5f);
                
            }

            base.Update(gameTime);
        }
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\Slime1");
            sprites[1] = content.Load<Texture2D>("Enemies\\Slime2");
        }

        public override void OnCollision(GameObject other)
        {
            // 0.1% chance of slimes merging to BlobMonster 
            if (random.Next(1000) < 1)
            {
                if (other is Slime)
                {
                    // merge together to 1 BlobMonster 
                    GameState.InstantiateGameObject(new BlobMonster(player, position, other.GetPosition));

                    // remove both slimes 
                    ShouldBeRemoved = true;
                    other.ShouldBeRemoved = true;
                }
            } 

            base.OnCollision(other); 
        }
        #endregion
    }
}
