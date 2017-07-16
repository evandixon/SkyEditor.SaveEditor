using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.MysteryDungeon
{
    public interface IMDAttack
    {
        bool IsValid { get; set; }
        bool IsLinked { get; set; }
        bool IsSwitched { get; set; }
        bool IsSet { get; set; }
        int ID { get; set; }
        int PowerBoost { get; set; }
    }
}
