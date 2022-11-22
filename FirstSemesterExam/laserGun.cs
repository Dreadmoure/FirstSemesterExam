using FirstSemesterExam.Menu;
using FirstSemesterExam.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FirstSemesterExam
{
    internal class laserGun : Weapon
    {

        public laserGun(Player player) : base(player)
        {

        }

        public override void Shoot(GameTime gameTime)
        {
            if (timeSinceFire > fireRate)
            {
                timeSinceFire = 0;
                PlayerProjectile projectile = new PlayerProjectile(shootingPos, dirVector, angle);
                GameState.InstantiateGameObject(projectile);
            }
        }

    }
}
