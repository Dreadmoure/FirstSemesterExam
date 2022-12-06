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
    /// Subclass of Enemy, Robot - a long range shooter 
    /// </summary>
    public class Robot : Enemy
    {
        #region Constructors
        /// <summary>
        /// Constructor for Robot, sets its initial variables 
        /// </summary>
        /// <param name="player"></param>
        public Robot(Player player) : base(player)
        {
            health = 15f;
            speed = 50f;
            attackSpeed = 5f;
            attackRange = 1000f;
            animationSpeed = 4f;
            expValue = 4;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            if (health <= 0)
            {
                
                GameWorld.soundEffects[11].CreateInstance().Play();
            }

            base.Update(gameTime);
        }
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\Robot1");
            sprites[1] = content.Load<Texture2D>("Enemies\\Robot2");
        }
        #endregion
    }
}
