using System;
using System.IO;

namespace FileCount
{
    class Program
    {
        static void Main(string[] args)
        {
            int allFileCount = 0;
            string directory;
            if (args.Length > 0)
            {
                directory = args[0];
            }
            else
            {
                directory = Directory.GetCurrentDirectory();
            }

            allFileCount = DirectoryCountFiles(directory);

            Console.WriteLine(directory);
            Console.WriteLine(allFileCount);
            Console.ReadKey();
        }

        static int DirectoryCountFiles(string directory)
        {
            int fileCount = 0;

            foreach (string file in Directory.GetFiles(directory))
            {
                fileCount++;
            }

            foreach (string subdirectory in Directory.GetDirectories(directory))
            {
                fileCount += DirectoryCountFiles(subdirectory);
            }

            return fileCount;
        }


    }
}
