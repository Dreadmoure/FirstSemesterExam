using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam
{
    public class HornedGuy : Enemy
    {
        public HornedGuy() : base()
        {
            health = 7; 
            speed = 10f;
            attackSpeed = 10f; 
        }
    }
}
