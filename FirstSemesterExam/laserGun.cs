using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FirstSemesterExam
{
    internal class laserGun : Weapon
    {
        
        public laserGun(Player player) : base(player)
        {

        }

        public override void Shoot()
        {
            PlayerProjectile projectile = new PlayerProjectile(shootingPos, dirVector);
            GameWorld.InstantiateGameObject(projectile);
        }

    }
}
