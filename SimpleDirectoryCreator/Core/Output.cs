using System;
using Simple_Directory_Creator.Core.Data;

namespace Simple_Directory_Creator.Core
{
    public static class Output
    {
        //Modified version of https://stackoverflow.com/questions/1649027/how-do-i-print-out-a-tree-structure
        public static void PrintTree(DirInfo root, string indent = "", bool last = true)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("└─");
                indent += "  ";
            }
            else
            {
                Console.Write("├─");
                indent += "│ ";
            }
            Console.Write(root.Name + "             ");
            root.WriteStatus();

            for (int i = 0; i < root.SubDirectories.Length; i++)
                PrintTree(root.SubDirectories[i], indent, i == root.SubDirectories.Length - 1);
        }
    }
}
