using ModLoader;
using ModLoader.Helpers;
using SFS.IO;
using UnityEngine;
using System.Linq;
using System.Reflection;
using UITools;


namespace ColorUi
{
    // Token: 0x02000002 RID: 2
    public class Main : Mod
    {
        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000001 RID: 1
        public override string ModNameID
        {
            get
            {
                return "ColorUi";
            }
        }

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000002 RID: 2
        public override string DisplayName
        {
            get
            {
                return "Color Ui";
            }
        }

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x06000003 RID: 3
        public override string Author
        {
            get
            {
                return "Cratior";
            }
        }

        // Token: 0x17000004 RID: 4
        // (get) Token: 0x06000004 RID: 4
        public override string MinimumGameVersionNecessary
        {
            get
            {
                return "1.5.10";
            }
        }

        // Token: 0x17000005 RID: 5
        // (get) Token: 0x06000005 RID: 5
        public override string ModVersion
        {
            get
            {
                return "2.2.3";
            }
        }

        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000006 RID: 6
        public override string Description
        {
            get
            {
                return "makes the ui more imformative with color";
            }
        }
        public override void Early_Load()
        {
            Main.main = this;
        }
        // Token: 0x0600003F RID: 63
        public override void Load()
        {
            Settings.Init(new FolderPath(Main.main.ModFolder).ExtendToFile("settings.txt"));
            base.Load();
            SceneHelper.OnSceneLoaded += delegate ()
            {
                new GameObject().AddComponent<ColorUi>();
            };
        }
        //// Token: 0x0600003F RID: 63
        //public override void Load()
        //{
        //    base.Load();
        //    SceneHelper.OnSceneLoaded += OnSceneLoadedHandler;
        //}

        //private void OnSceneLoadedHandler(Scene scene)
        //{
        //    GameObject configObject = new GameObject();
        //    configObject.AddComponent<ThrottleSliderColorUpdater>();
        //    Config.SettingsData configData = Config.LoadConfig();
        //    ThrottleSliderColorUpdater configComponent = configObject.GetComponent<ThrottleSliderColorUpdater>();
        //    //configComponent.ApplyConfig(configData);
        //}

        public static Main main;
    }

}
