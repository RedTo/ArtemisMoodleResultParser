using FileHelpers;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisMoodleResults
{
    internal class Program
    {
        private static void Main(string[] args) {
            //Check if the programm was called with two files.
            if (args.Length != 2) {
                Exit("Please call this programm with two files: Moodle-File and Artemis-File!");
                return;
            }
            //Check if both files have csv-extension.
            if (!Path.GetExtension(args[0]).Equals(".csv") || !Path.GetExtension(args[1]).Equals(".csv")) {
                Exit("This programm can only handle CSV-Files.");
                return;
            }
            //get the filenames of both
            string fileName1 = Path.GetFileName(args[0]);
            string fileName2 = Path.GetFileName(args[1]);

            ArtemisResult[] artemis;
            MoodleResult[] moodle;
            string outputFile = "";

            //Check if first or second file is the artemis-file and parse with the corresponding class.
            if (fileName1.ToLower().Contains("artemis")) {
                var engine = new FileHelperEngine<ArtemisResult>();
                artemis = engine.ReadFile(args[0]);
                var engine2 = new FileHelperEngine<MoodleResult>();
                var txt = File.ReadAllText(args[1]);
                moodle = engine2.ReadString(txt);
                outputFile = Path.GetDirectoryName(args[1]);
            } else if (fileName2.ToLower().Contains("artemis")) {
                var engine = new FileHelperEngine<MoodleResult>();
                var txt = File.ReadAllText(args[0]);
                moodle = engine.ReadString(txt);
                outputFile = Path.GetDirectoryName(args[0]);
                var engine2 = new FileHelperEngine<ArtemisResult>();
                artemis = engine2.ReadFile(args[1]);
            } else {
                Exit("There is no Artemis-File given as attribute (file which includes name 'artemis').");
                return;
            }
            //outputpath will be the same as moodle-file path
            outputFile += "\\moodleImport.csv";
            StringBuilder builder = new StringBuilder();

            //for each moodle result search in artemis file for corresponding entry
            foreach (MoodleResult m in moodle) {
                var artemisResult = artemis.FirstOrDefault(o => o.Name.Equals(m.FirstName + " " + m.LastName));
                if (artemisResult != null) {
                    //get the score for existing artemis entry and divide by 10
                    var score = double.Parse(artemisResult.Score.Trim()) / 10.0;
                    //use . instead of culture separator and set the moodle result score
                    m.Score = score.ToString("N2", CultureInfo.InvariantCulture);
                } else if (m.Score.Equals("-")) {
                    //if there was no artemis result and the moodle result score is -, set it to 0.00
                    m.Score = (0).ToString("N2", CultureInfo.InvariantCulture);
                }
                //add a new line to the import
                builder.Append(m.FirstName + "," + m.LastName + "," + m.ID + "," + m.University + "," + m.Faculty + "," + m.Score + "," + m.LastMoodleExport + "\n");
            }
            //write all the text to the specified file
            File.WriteAllText(outputFile, builder.ToString());
            //finish program
            Exit("Finished parsing... You will find the import file here: " + outputFile);
        }

        //Wait for a key input from the user and prints message.
        private static void Exit(string message) {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to exit.");
            while (!Console.KeyAvailable) {
                // Wait for key input
            }
            return;
        }
    }
}
