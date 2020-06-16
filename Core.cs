using Kingmaker.PubSubSystem;
using ModMaker;
using System;
using System.Reflection;
using static KingmakerHarmony2Template.Main;
using static KingmakerHarmony2Template.Utilities.SettingsWrapper;

namespace KingmakerHarmony2Template
{
    class Core :
        IModEventHandler
    {
        public int Priority => 200;

        public void ResetSettings()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());
            Mod.ResetSettings();
            Mod.Settings.lastModVersion = Mod.Version.ToString();
            LocalizationFileName = Local.FileName;
        }

        public void HandleModEnable()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());
            if (!string.IsNullOrEmpty(LocalizationFileName))
            {
                Local.Import(LocalizationFileName, e => Mod.Error(e));
                LocalizationFileName = Local.FileName;
            }
            if (!Version.TryParse(Mod.Settings.lastModVersion, out Version version) || version < new Version(0, 0, 0))
                ResetSettings();
            else
            {
                Mod.Settings.lastModVersion = Mod.Version.ToString();
            }

            EventBus.Subscribe(this);
        }

        public void HandleModDisable()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());
            EventBus.Unsubscribe(this);
        }
    }
}