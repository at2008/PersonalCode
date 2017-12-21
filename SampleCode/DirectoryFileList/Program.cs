using System;
using System.IO;

namespace DirectoryFileList
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory;
            if (args.Length > 0)
            {
                directory = args[0];
            }
            else
            {
                directory = Directory.GetCurrentDirectory();
            }

            //directory = @"d:\YoMail";

            Console.WriteLine(directory);

            DirectoryFileList(directory, directory);

            Console.ReadKey();
        }

        static void DirectoryFileList(string beginDirectory, string currentDirectory)
        {
            int level = currentDirectory.Split('\\').Length - beginDirectory.Split('\\').Length;

            string formatPrefix = "|--";

            for (int currentLevel = 0; currentLevel < level; currentLevel++)
            {
                formatPrefix += "|--";
            }

            foreach (string subdirectory in Directory.GetDirectories(currentDirectory))
            {
                //string[] currentPathName = subdirectory.Split('\\');
                //Console.WriteLine(formatPrefix + currentPathName[currentPathName.Length-1]);
                Console.WriteLine(formatPrefix + subdirectory.Substring(subdirectory.LastIndexOf('\\')));

                DirectoryFileList(beginDirectory, subdirectory);
            }

            foreach (string file in Directory.GetFiles(currentDirectory))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(formatPrefix + Path.GetFileName(file));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
