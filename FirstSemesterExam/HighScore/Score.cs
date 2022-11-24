using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.HighScore
{
    public class Score
    {
        public string name { get; set; }
        public int score { get; set; }

        public Score(string name, int score)
        {
            this.name = name;
            this.score = score; 
        }
    }
}
