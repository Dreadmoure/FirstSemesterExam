using FirstSemesterExam.Menu;
using FirstSemesterExam.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FirstSemesterExam
{
    internal class LaserGun : Weapon
    {
        #region Constructors
        public LaserGun(Player player, float attackDamage) : base(player)
        {
            this.attackDamage = attackDamage;
        }
        #endregion

        #region Methods
        //Shoots a protectile based on the players firerate, the bigger the number og firerate the faster the player shoots.
        // if firerate is 1 its every second, if its 2 its every 0.5 seconds
        public override void Shoot(GameTime gameTime)
        {
            if (timeSinceFire > 1/player.AttackSpeed)
            {
                timeSinceFire = 0;
                PlayerProjectile projectile = new PlayerProjectile(shootingPos, dirVector, angle, player.AttackDamage);
                GameState.InstantiateGameObject(projectile);
            }
        }
        #endregion

    }
}
