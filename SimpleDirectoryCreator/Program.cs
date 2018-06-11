using System;
using System.Collections.Generic;
using System.IO;
using ColorConsole;
using Simple_Directory_Creator.Core;
using Simple_Directory_Creator.Core.Data;

namespace Simple_Directory_Creator
{
    class Program
    {
        public static DirectoryState CurrentState { get; set; }
        public static float CurrentProgress { get; set; }

        public static string RootDirectory { get; set; }

        public static ConsoleWriter ColorConsole { get; } = new ConsoleWriter();

        static void Main(string[] args)
        {
            args = new string[] { "TestList.yaml" };
            if (args.Length > 0 && args.Length <= 2 && File.Exists(args[0]))
            {
                if (args.Length > 1 && Directory.Exists(args[1]))
                {
                    RootDirectory = args[1];
                }
                else
                {
                    RootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                //Get the directory creation 'instructions'
                KeyValuePair<DirInfo[], int> instructions = Interpreter.ParseDirectoryInstructions(args[0]);

                //Create the directories based on the 'instructions'
                DirectoryCreator.CreateDirectories(instructions.Key);

                //Print a pretty tree of what directories were made in which order.
                Output.PrintTree(instructions.Key[0]);

                Console.WriteLine();
                Console.WriteLine("Press Any Key to Exit...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Usage: {0} [InputFile] {rootDirectory}", (System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe"));
                System.Threading.Thread.Sleep(5000);
                return;
            }
        }
    }
}
