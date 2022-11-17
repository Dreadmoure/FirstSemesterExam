using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam
{
    public class Robot : Enemy
    {
        public Robot() : base()
        {
            health = 15; 
            speed = 5f;
            attackSpeed = 50f; 
        }
    }
}
