using FirstSemesterExam.Menu;
using FirstSemesterExam.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FirstSemesterExam
{
    /// <summary>
    /// LaserGun is the base weapon the player starts with. It can rotate and shoot
    /// </summary>
    internal class LaserGun : Weapon
    {
        #region Constructors
        /// <summary>
        /// LaserGun constructor
        /// </summary>
        /// <param name="player">Gets the player as it needs to access its position constantly, inherited from the superclass</param>
        /// <param name="attackDamage">sets the attackDamage of the gun</param>
        public LaserGun(Player player, float attackDamage) : base(player)
        {
            this.attackDamage = attackDamage;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Shoots a protectile based on the players firerate, the bigger the number og firerate the faster the player shoots.
        /// if firerate is 1 its every second, if its 2 its every 0.5 seconds
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Shoot(GameTime gameTime)
        {
            if (timeSinceFire > 1/player.AttackSpeed)
            {
                timeSinceFire = 0;
                //creates a new projectile object
                PlayerProjectile projectile = new PlayerProjectile(shootingPos, dirVector, angle, player.AttackDamage);
                //instanstiates it in the gamestate
                GameState.InstantiateGameObject(projectile);
            }
        }
        #endregion

    }
}
