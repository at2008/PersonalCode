using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelLegacyFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //string dirPath = @"F:\Dev\VisualStudio\VS2017Ent";
                string dirPath = @".";

                List<string> dirs = new List<string>(Directory.EnumerateDirectories(dirPath));

                List<string> olderFolder = new List<string>();

                //不自行排序，使用系统自己的排序方式
                //dirs.Sort();

                int count = 0;
                for (int i = 0; i < dirs.Count - 1; i++)
                {
                    string[] OlderFolderName = dirs[i].Split(',');
                    string[] NewerFolderName = dirs[i + 1].Split(',');

                    if ((OlderFolderName[0] == NewerFolderName[0]) && (OlderFolderName[1] != NewerFolderName[1]))
                    {
                        Console.WriteLine(dirs[i]);
                        Console.WriteLine(dirs[i + 1]);
                        olderFolder.Add(dirs[i]);
                        count += 1;
                        i += 2;
                        continue;
                    }
                }
                Console.WriteLine("总共有 {0} 个旧文件夹，是否删除？ y--删除 n--不删除", count);
                string yesDel = Console.ReadLine().Trim().ToUpper();
                if (yesDel == "Y")
                {
                    for (int i = 0; i < olderFolder.Count; i++)
                    {
                        Directory.Delete(olderFolder[i], true);
                    }
                }


            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }


            Console.WriteLine("执行完毕，按任意键退出。");
            Console.ReadKey();
        }
    }
}
