using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SkyEditor.SaveEditor.Tests
{
    public static class DataUtil
    {
        public static byte[] GetBinaryResource(string name)
        {
            using (var resource = typeof(DataUtil).GetTypeInfo().Assembly.GetManifestResourceStream("SkyEditor.SaveEditor.Tests.Resources." + name))
            {
                var buffer = new byte[resource.Length];
                resource.Read(buffer, 0, (int)resource.Length);
                return buffer;
            }
        }
    }
}
