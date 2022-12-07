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
    /// Subclass of Enemy, HornedGuy - nothing special about it 
    /// </summary>
    public class HornedGuy : Enemy
    {
        #region Constructors
        /// <summary>
        /// Constructor for HornedGuy that sets its initial variables 
        /// </summary>
        /// <param name="player"></param>
        public HornedGuy(Player player) : base(player)
        {
            health = 20f;
            speed = 100f;
            attackSpeed = 10f;
            attackDamage = 5;
            attackRange = 100f;
            animationSpeed = 3f;
            expValue = 5;
        }
        #endregion

        #region Method
        public override void Update(GameTime gameTime)
        {
            if (health <= 0)
            {
                GameWorld.soundEffects[9].Play(volume: 0.5f, pitch: 0.0f, pan: 0.5f);
                GameWorld.soundEffects[9].CreateInstance().Play();
            }

            base.Update(gameTime);
        }
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\HornGuy1");
            sprites[1] = content.Load<Texture2D>("Enemies\\HornGuy2");
        }
        #endregion
    }
}
