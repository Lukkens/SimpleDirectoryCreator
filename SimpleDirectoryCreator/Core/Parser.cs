using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Simple_Directory_Creator.Core.Data;
using YamlDotNet.RepresentationModel;

namespace Simple_Directory_Creator.Core
{
    public static class Parser
    {
        private static YamlStream _YamlStream;

        public static KeyValuePair<DirInfo[], int> ParseDirectoryInstructions(string path)
        {
            string input = File.ReadAllText(path);
            KeyValuePair<DirInfo[], int> output = new KeyValuePair<DirInfo[], int>();
            try
            {
                StringReader reader = new StringReader(input);
                _YamlStream = new YamlStream();
                _YamlStream.Load(reader);

                YamlMappingNode mapping = (YamlMappingNode)_YamlStream.Documents[0].RootNode;
                var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("DirectoryDef")];
                foreach (YamlMappingNode item in items)
                {
                    output = IterateEntries(items);
                    Debug.WriteLine("Items: {0}", output.Value);
                }
            }
            catch
            {
                return new KeyValuePair<DirInfo[], int>();
            }
            return output;
        }
        private static KeyValuePair<DirInfo[], int> IterateEntries(YamlSequenceNode items, int iterCount = 0)
        {
            List<DirInfo> Entries = new List<DirInfo>();
            int subDirectoryCount = 0;
            string entryName = "";
            List<DirInfo> entrySubDirectories;
            foreach (YamlMappingNode item in items)
            {
                Debug.WriteLine("{0}\t", item.Children[new YamlScalarNode("Name")]);
                entryName = item.Children[new YamlScalarNode("Name")].ToString();
                entrySubDirectories = new List<DirInfo>();

                try
                {
                    iterCount++;
                    KeyValuePair<DirInfo[], int> kvp = IterateEntries((YamlSequenceNode)item.Children[new YamlScalarNode("SubDirectories")], iterCount);
                    entrySubDirectories = kvp.Key.ToList();
                    subDirectoryCount += kvp.Value;
                }
                catch
                {
                    //entrySubDirectories = new List<DirInfo>();
                }

                subDirectoryCount += entrySubDirectories.Count;
                Entries.Add(new DirInfo(entryName, entrySubDirectories.ToArray()));
            }
            return new KeyValuePair<DirInfo[], int>(Entries.ToArray(), subDirectoryCount);
        }
    }
}
