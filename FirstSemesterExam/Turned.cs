using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam
{
    public class Turned : Enemy
    {
        public Turned() : base()
        {
            health = 10; 
            speed = 25f;
            attackSpeed = 20f; 
        }
    }
}
