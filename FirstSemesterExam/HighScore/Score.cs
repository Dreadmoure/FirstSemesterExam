using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.HighScore
{
    /// <summary>
    /// Made by: Ida 
    /// Class for storing a score, used for Highscore class 
    /// </summary>
    public class Score
    {
        #region Fields
        private string name;
        private int score;
        #endregion

        #region Properties
        /// <summary>
        /// Property for player name 
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Property for player score 
        /// </summary>
        public int _Score
        {
            get { return score; }
            set { score = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for storing the name and score 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="score"></param>
        public Score(string name, int score)
        {
            this.name = name;
            this.score = score; 
        }
        #endregion
    }
}
