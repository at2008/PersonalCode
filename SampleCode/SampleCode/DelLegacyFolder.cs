﻿using System;
using System.Collections.Generic;
using System.IO;

namespace SampleCode
{
    class DelLegacyFolder
    {
        static void Main(string[] args)
        {
            try
            {
                //string dirPath = @"F:\Dev\VisualStudio\VS2017Ent";
                string dirPath = @".";

                List<string> dirs = new List<string>(Directory.EnumerateDirectories(dirPath));

                List<string> olderFolder = new List<string>();

                //测试修改 hosts 后联网速度
                //不自行排序，使用系统自己的排序方式
                //dirs.Sort();

                int count = 0;
                for (int i = 0; i < dirs.Count - 1; i++)
                {
                    string[] OlderFolderName = dirs[i].Split(',');
                    string[] NewerFolderName = dirs[i + 1].Split(',');

                    if ((OlderFolderName[0] == NewerFolderName[0])
                        && (OlderFolderName[1] != NewerFolderName[1])
                        && (OlderFolderName.Length == NewerFolderName.Length))
                    {
                        bool ifNeedOperation = false;
                        switch (OlderFolderName.Length)
                        {
                            case 2:
                                ifNeedOperation = true;
                                break;
                            case 3:
                                if ((OlderFolderName[2] == NewerFolderName[2]))
                                    ifNeedOperation = true;
                                break;
                            case 4:
                                if ((OlderFolderName[2] == NewerFolderName[2])
                                    && (OlderFolderName[3] == NewerFolderName[3]))
                                    ifNeedOperation = true;
                                break;
                            default:
                                break;
                        }

                        if (ifNeedOperation)
                        {
                            string[] olderVersion = OlderFolderName[1].Substring(OlderFolderName[1].IndexOf("=") + 1).Split('.');
                            string[] newerVersion = NewerFolderName[1].Substring(NewerFolderName[1].IndexOf("=") + 1).Split('.');
                            for (int x = 0; x < olderVersion.Length; x++)
                            {
                                if (Int32.Parse(olderVersion[x]) < Int32.Parse(newerVersion[x]))
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(dirs[i]);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine(dirs[i + 1]);
                                    olderFolder.Add(dirs[i]);
                                    count += 1;
                                    i += 1;
                                    break;
                                }
                                else if (Int32.Parse(olderVersion[x]) > Int32.Parse(newerVersion[x]))
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine(dirs[i]);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(dirs[i + 1]);
                                    olderFolder.Add(dirs[i + 1]);
                                    count += 1;
                                    i += 1;
                                    break;
                                }
                            }
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;
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