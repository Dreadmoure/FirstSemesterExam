using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam
{
    public class EnemyProjectile : Projectile
    {


        public override void OnCollision(GameObject other)
        {
            if(other is Player)
            {
                ShouldBeRemoved = true; 
            }
        }
    }
}
