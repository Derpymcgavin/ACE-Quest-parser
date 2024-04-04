using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
        string inputFilePath = Path.Combine(directoryPath, "quests.sql");

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine("The quests.sql file was not found in the program's directory.");
            return;
        }

        using (StreamReader reader = new StreamReader(inputFilePath))
        {
            string line;
            StreamWriter writer = null;
            Regex questNamePattern = new Regex(@"DELETE FROM `quest` WHERE `name` = '(.*?)';");

            while ((line = reader.ReadLine()) != null)
            {
                Match match = questNamePattern.Match(line);

                if (match.Success)
                {
                    writer?.Dispose();
                    string questName = match.Groups[1].Value;
                    string outputFilePath = Path.Combine(directoryPath, $"{questName}.sql");
                    writer = new StreamWriter(outputFilePath, false);
                }

                writer?.WriteLine(line);
            }

            writer?.Dispose();
        }

        Console.WriteLine("Quest files have been generated.");
    }
}
