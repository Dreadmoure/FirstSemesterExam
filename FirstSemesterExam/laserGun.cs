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

        public LaserGun(Player player, float attackDamage) : base(player)
        {
            this.attackDamage = attackDamage;
        }

        public override void Shoot(GameTime gameTime)
        {
            if (timeSinceFire > 1/player.AttackSpeed)
            {
                GameWorld.soundEffects[0].CreateInstance().Play();
                timeSinceFire = 0;
                PlayerProjectile projectile = new PlayerProjectile(shootingPos, dirVector, angle, attackDamage);
                GameState.InstantiateGameObject(projectile);
            }
        }

    }
}
