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
            if (args.Length != 2) {
                Exit("Please call this programm with two files: Moodle-File and Artemis-File!");
                return;
            }
            if (!Path.GetExtension(args[0]).Equals(".csv") || !Path.GetExtension(args[1]).Equals(".csv")) {
                Exit("This programm can only handle CSV-Files.");
                return;
            }
            string fileName1 = Path.GetFileName(args[0]);
            string fileName2 = Path.GetFileName(args[1]);

            ArtemisResult[] artemis;
            MoodleResult[] moodle;
            string outputFile = "";
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

            outputFile += "\\moodleImport.csv";
            StringBuilder builder = new StringBuilder();

            foreach (MoodleResult m in moodle) {
                var artemisResult = artemis.FirstOrDefault(o => o.Name.Equals(m.FirstName + " " + m.LastName));
                if (artemisResult != null) {
                    var score = double.Parse(artemisResult.Score.Trim()) / 10.0;
                    m.Score = score.ToString("N2", CultureInfo.InvariantCulture);
                } else if (m.Score.Equals("-")) {
                    m.Score = (0).ToString("N2", CultureInfo.InvariantCulture);
                }
                builder.Append(m.FirstName + "," + m.LastName + "," + m.ID + "," + m.University + "," + m.Faculty + ","
                    // + m.Unused + ","
                    + m.Score + "," + m.Unused2 + "\n");
            }
            File.WriteAllText(outputFile, builder.ToString());
            Exit("Finished parsing... You will find the import file here: " + outputFile);
        }

        private static void Exit(string message) {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to exit.");
            while (!Console.KeyAvailable) {
                // Do something
            }
            return;
        }
    }
}
