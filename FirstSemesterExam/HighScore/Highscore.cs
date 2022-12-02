using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.HighScore
{
    public class Highscore
    {
        #region Fields
        // make list for scores 
        private List<Score> scores = new List<Score>();
        private string filePath = "./scores.txt";
        private string[] fileDataLines;
        private FileStream file;
        #endregion

        #region Properties
        public List<Score> GetScores
        {
            get { return scores; }
        }
        #endregion

        #region Constructors
        public Highscore()
        {
            CreateFile();

            ReadFile();

            Sort();

            WriteToFile(); 
        }
        #endregion

        #region Methods
        // check if file exist, if not then create it 
        private void CreateFile()
        {
            if (!File.Exists(filePath))
            {
                file = File.Create(filePath);
                file.Close(); 
            }
        }


        // read scores from file, and parse them into the list 
        private void ReadFile()
        {
            // Score: name, score \n 

            // read file 
            fileDataLines = File.ReadAllLines(filePath);

            // split the string of fileData, and put it into the list 
            foreach (string line in fileDataLines)
            {
                string[] sub = line.Split(" "); 
                
                string name = sub[0];
                int score = Int32.Parse(sub[1]);

                scores.Add(new Score(name, score)); 
            }
        }


        // sort the list according to scores - highest to lowest 
        private void Sort()
        {
            scores = scores.OrderBy(x => -x._Score).ToList(); 
        }


        // rewrite the file in the order of the scores 
        private void WriteToFile()
        {
            File.Delete(filePath); 

            if (!File.Exists(filePath))
            {
                file = File.Create(filePath);
                file.Close();
            }

            for (int i = 0; i < scores.Count; i++)
            {
                File.AppendAllText(filePath, scores[i].Name + " " + scores[i]._Score + "\n");
            }
        }
        #endregion
    }
}
