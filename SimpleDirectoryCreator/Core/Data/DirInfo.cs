using System;

namespace Simple_Directory_Creator.Core.Data
{
    public class DirInfo
    {
        public DirectoryState DirState;
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
                    Program.ColorConsole.WriteLine("[Created]", ConsoleColor.Green);
                    break;
            }
        }
    }
}
