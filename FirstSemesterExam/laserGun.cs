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

        public LaserGun(Player player) : base(player)
        {
            attackDamage = 10f;
        }

        public override void Shoot(GameTime gameTime)
        {
            if (timeSinceFire > player.AttackSpeed)
            {
                timeSinceFire = 0;
                PlayerProjectile projectile = new PlayerProjectile(shootingPos, dirVector, angle, attackDamage);
                GameState.InstantiateGameObject(projectile);
            }
        }

    }
}
