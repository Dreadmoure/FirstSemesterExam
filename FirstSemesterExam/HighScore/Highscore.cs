using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.HighScore
{
    /// <summary>
    /// Class for sorting a file of scores from highest to lowest 
    /// </summary>
    public class Highscore
    {
        #region Fields
        // List used for sorting scores 
        private List<Score> scores = new List<Score>();
        private string filePath = "./scores.txt";
        private string[] fileDataLines;
        private FileStream file;
        #endregion

        #region Properties
        /// <summary>
        /// Property to get the list of scores 
        /// </summary>
        public List<Score> GetScores
        {
            get { return scores; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that calls the methods of this class 
        /// </summary>
        public Highscore()
        {
            CreateFile();

            ReadFile();

            Sort();

            WriteToFile(); 
        }
        #endregion

        #region Methods
        /// <summary>
        /// Checks if file exists, and creates the file if it does not exist
        /// </summary>
        private void CreateFile()
        {
            // check if file exists 
            if (!File.Exists(filePath))
            {
                // create file with filePath 
                file = File.Create(filePath);
                file.Close(); 
            }
        }

        /// <summary>
        /// Reads the scores from the file and parses them into the list 
        /// </summary>
        private void ReadFile()
        {
            // Score text format: "name score\n" 

            // read each line of the file 
            fileDataLines = File.ReadAllLines(filePath);

            // split the string of fileData, and put it into the list 
            foreach (string line in fileDataLines)
            {
                // split at space 
                string[] sub = line.Split(" "); 
                
                string name = sub[0];
                int score = Int32.Parse(sub[1]);

                // add the score to the list of scores 
                scores.Add(new Score(name, score)); 
            }
        }

        /// <summary>
        /// Sorts the list according to the scores - from highest to lowest 
        /// </summary>
        private void Sort()
        {
            // sort by the Score types second parameter: _Score 
            scores = scores.OrderBy(x => -x._Score).ToList(); 
        }

        /// <summary>
        /// Rewrite the file, so the scores are in sorted order 
        /// </summary>
        private void WriteToFile()
        {
            // delete file 
            File.Delete(filePath); 

            // create file again 
            if (!File.Exists(filePath))
            {
                file = File.Create(filePath);
                file.Close();
            }

            // write the scores from the now sorted list to the file 
            for (int i = 0; i < scores.Count; i++)
            {
                File.AppendAllText(filePath, scores[i].Name + " " + scores[i]._Score + "\n");
            }
        }
        #endregion
    }
}
