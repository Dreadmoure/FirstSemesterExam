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
        // make list for scores 
        private List<Score> scores = new List<Score>();
        private List<Score> sortedScores = new List<Score>(); 
        private string fileName = "./scores.txt";
        private string[] fileDataLines;
        private FileStream file; 

        public List<Score> Scores
        {
            get { return scores; }
        }

        public Highscore()
        {
            CreateFile();

            ReadFile(); 

        }

        // check if file exist, if not then create it 
        private void CreateFile()
        {
            if (!File.Exists(fileName))
            {
                file = File.Create(fileName);
                file.Close(); 
            }
        }


        // read scores from file, and parse them into the list 
        private void ReadFile()
        {
            // Score: name, score \n 

            // read file 
            fileDataLines = File.ReadAllLines(fileName);

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


        // rewrite the file in the order of the scores 

    }
}
