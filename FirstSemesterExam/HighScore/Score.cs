using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.HighScore
{
    public class Score
    {
        #region Fields
        private string name;
        private int score;
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int _Score
        {
            get { return score; }
            set { score = value; }
        }
        #endregion

        #region Constructors
        public Score(string name, int score)
        {
            this.name = name;
            this.score = score; 
        }
        #endregion
    }
}
