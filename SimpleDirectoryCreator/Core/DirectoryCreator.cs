using System.Diagnostics;
using System.IO;
using Simple_Directory_Creator.Core.Data;

namespace Simple_Directory_Creator.Core
{
    public static class DirectoryCreator
    {
        private static string CurrentRoot = Program.RootDirectory;

        public static void CreateDirectories(DirInfo[] directories, DirInfo parent = null, string Root = "")
        {
            if (string.IsNullOrWhiteSpace(Root))
            {
                Root = CurrentRoot;
            }

            if (parent != null)
            {
                Root = FormatDir(Root, parent.Name);
            }
            else
            {
                Program.RootDirectory = FormatDir(Program.RootDirectory, directories[0].Name);
                CurrentRoot = Program.RootDirectory;
            }

            foreach (DirInfo dir in directories)
            {
                if (true)
                {
                    Directory.CreateDirectory(FormatDir(Root, dir.Name));
                    Debug.WriteLine(FormatDir(Root, dir.Name));
                }
                CreateDirectories(dir.SubDirectories, dir, Root);
            }
        }

        
        private static string FormatDir(string dir1, string dir2)
        {
            return string.Format("{0}\\{1}", dir1, dir2);
        }
    }
}
