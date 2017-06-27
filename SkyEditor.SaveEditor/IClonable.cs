using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor
{
    /// <summary>
    /// Represents an object that can create a new but equal instance of itself
    /// </summary>
    public interface IClonable
    {
        object Clone();
    }
}
