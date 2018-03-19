using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor
{
    public class BasicIniDictionaryFile
    {
        public static Dictionary<int, string> GetDictionary(string iniFileContents)
        {
            var entries = new Dictionary<int, string>();
            foreach (var line in iniFileContents.Split('\n'))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var parts = line.Trim().Split("=".ToCharArray(), 2);
                    var key = int.Parse(parts[0]);
                    if (!entries.ContainsKey(key))
                    {
                        entries.Add(key, parts[1]);
                    }
                }
            }
            return entries;
        }

        public BasicIniDictionaryFile(string fileContents)
        {
            Entries = GetDictionary(fileContents);
        }

        public Dictionary<int, string> Entries { get; set; }
    }
}
