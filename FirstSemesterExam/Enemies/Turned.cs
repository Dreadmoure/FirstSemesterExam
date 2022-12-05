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
    /// Subclass of Enemy, Turned - a faster enemy 
    /// </summary>
    public class Turned : Enemy
    {
        #region Constructors
        /// <summary>
        /// Constructor for Turned, sets its initial variables 
        /// </summary>
        /// <param name="player"></param>
        public Turned(Player player) : base(player)
        {
            health = 10f;
            speed = 250f;
            attackSpeed = 20f;
            attackDamage = 5;
            attackRange = 25f;
            animationSpeed = 6f;
            expValue = 5;
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\Turned1");
            sprites[1] = content.Load<Texture2D>("Enemies\\Turned2");
        }
        #endregion
    }
}
