using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace SkyEditor.SaveEditor
{
    public static class DataUtil
    {
        private static readonly Assembly thisAssembly = Assembly.GetExecutingAssembly();
        private static readonly string[] manifestResourceNames = thisAssembly.GetManifestResourceNames();
        private static readonly Dictionary<string, string> resourceNameMap = new Dictionary<string, string>();

        public static string GetStringResource(string name)
        {
            if (!resourceNameMap.ContainsKey(name))
            {
                bool Match(string x)
                {
                    if (x.StartsWith("SkyEditor.SaveEditor.Resources." + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
                        && x.EndsWith($"{name}.txt", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else if (x.StartsWith("SkyEditor.SaveEditor.Resources.en")
                            && x.EndsWith($"{name}.txt", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                var resname = Array.Find(manifestResourceNames, Match);
                resourceNameMap.Add(name, resname);
            }

            if (resourceNameMap[name] == null)
            {
                return null;
            }

            using (var resource = thisAssembly.GetManifestResourceStream(resourceNameMap[name]))
            using (var reader = new StreamReader(resource))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
