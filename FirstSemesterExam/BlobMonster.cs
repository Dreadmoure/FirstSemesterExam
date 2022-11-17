using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam
{

    public class BlobMonster : Enemy
    {
        public BlobMonster() : base()
        {
            health = 5; 
            speed = 5f;
            attackSpeed = 2f; 
        }
    }
}
