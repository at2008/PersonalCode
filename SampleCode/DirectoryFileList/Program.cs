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

            //directory = @"e:\";

            Console.WriteLine(directory);
            DirectoryFileList(directory, directory);
            Console.WriteLine("遍历结束。");
            Console.ReadKey();
        }

        static void DirectoryFileList(string beginDirectory, string currentDirectory)
        {
            int level = currentDirectory.Split('\\').Length - beginDirectory.Split('\\').Length;
            level = ((beginDirectory.Split('\\')[1] == "") && (currentDirectory.Split('\\')[1] != "")) ? level + 1 : level;
            string formatPrefix = "|--";

            for (int currentLevel = 0; currentLevel < level; currentLevel++)
            {
                formatPrefix += "|--";
            }
            try
            {
                foreach (string subdirectory in Directory.GetDirectories(currentDirectory))
                {
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
            catch (UnauthorizedAccessException)
            {
                System.Console.WriteLine("发现受保护文件目录，禁止访问。");
            }
            catch (Exception exception)
            {
                Console.WriteLine("程序运行异常:" + exception.Message);
            }
        }
    }
}