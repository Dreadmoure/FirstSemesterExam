using FirstSemesterExam.Menu;
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
    /// <summary>
    /// Subclass of Enemy, BlobMonster - can split into multiple Slime 
    /// </summary>
    public class BlobMonster : Enemy
    {
        #region Fields
        private Player player;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for spawning enemy at edge 
        /// </summary>
        /// <param name="player"></param>
        public BlobMonster(Player player) : base(player)
        {
            health = 20f;
            speed = 50f;
            attackSpeed = 10f;
            attackRange = 25f;
            attackDamage = 5;
            animationSpeed = 2f;
            expValue = 5;
            this.player = player; 
        }
        /// <summary>
        /// Constructor overload for spawning new BlobMonster at position 
        /// Used when two Slime collide and merge into BlobMonster 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="child1Position"></param>
        /// <param name="child2Position"></param>
        public BlobMonster(Player player, Vector2 child1Position, Vector2 child2Position) : base(player)
        {
            health = 20f;
            speed = 50f;
            attackSpeed = 2f;
            attackRange = 25f;
            animationSpeed = 2f;
            expValue = 5;
            this.player = player;

            // calculate position between the two Slime
            Vector2 midPointPosition;
            midPointPosition.X = (child1Position.X + child2Position.X) / 2; 
            midPointPosition.Y = (child1Position.Y + child2Position.Y) / 2; 
            position = midPointPosition;
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\BlobMonster1");
            sprites[1] = content.Load<Texture2D>("Enemies\\BlobMonster2");
        }

        public override void Update(GameTime gameTime)
        {
            if(health <= 0)
            {
                GameWorld.soundEffects[10].CreateInstance().Play();
                // when a BlobMonster dies it spawns 1-3 new Slime 
                int numberOfChildren = random.Next(1, 4);
                for (int i = 0; i < numberOfChildren; i++)
                {
                    GameState.InstantiateGameObject(new Slime(player, position));
                }
            }

            base.Update(gameTime); 
        }
        #endregion
    }
}
