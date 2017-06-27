using SkyEditor.Core;
using SkyEditor.SaveEditor.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor
{
    public class PluginDefinition : SkyEditorPlugin
    {
        public override string PluginName => Language.PluginName;

        public override string PluginAuthor => Language.PluginAuthor;

        public override string Credits => Language.PluginCredits;

        public override void Load(PluginManager manager)
        {
            base.Load(manager);

            manager.RegisterIOFilter("*.sav", Language.RawSaveFile);
            manager.RegisterIOFilter("*.skypkm", Language.SkyPkmFile);
            manager.RegisterIOFilter("*.skypkmex", Language.SkyPkmExFile);
            manager.RegisterIOFilter("*.skypkmq", Language.SkyPkmQFile);
            manager.RegisterIOFilter("*.tdpkm", Language.TDPkmFile);
            manager.RegisterIOFilter("*.tdpkmex", Language.TDPkmExFile);
            manager.RegisterIOFilter("*.rbpkm", Language.RBPkmFile);
            manager.RegisterIOFilter("game_data", Language.GameDataFile);
        }
    }
}
