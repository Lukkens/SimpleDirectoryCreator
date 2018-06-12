using System;

namespace Simple_Directory_Creator.Core.Data
{
    public class DirInfo
    {
        public DirectoryState DirState = DirectoryState.PENDING;

        public string Name;
        public DirInfo[] SubDirectories;

        public DirInfo(string name, DirInfo[] subDirs)
        {
            Name = name;
            SubDirectories = subDirs;
        }

        public void WriteStatus()
        {
            switch (DirState)
            {
                case DirectoryState.CREATED:
                    Console.Write("[");
                    Program.ColorConsole.Write("Created", ConsoleColor.Green);
                    Console.WriteLine("]");
                    break;
                case DirectoryState.PENDING:
                case DirectoryState.FAILED:
                    Console.Write("[");
                    Program.ColorConsole.Write("Failed", ConsoleColor.Red);
                    Console.WriteLine("]");
                    break;
                case DirectoryState.ALREADYEXISTS:
                    Console.Write("[");
                    Program.ColorConsole.Write("Already Exists", ConsoleColor.Yellow);
                    Console.WriteLine("]");
                    break;
            }
        }
    }
}
