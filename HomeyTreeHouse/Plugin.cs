using System;
using System.IO;
using System.Reflection;
using BepInEx;
using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine;
using Utilla;

namespace AssetBundleTemplate
{
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		bool inRoom;
		public static Plugin instance;
        public static AssetBundle bundle;
        public static GameObject assetBundleParent;
        public static GameObject Couch;
        public static GameObject Carpet;
        public static GameObject Screen;
        public static GameObject Window;
        public static GameObject ScreenOutline;        
        public static string assetBundleName = "thestuff";
        public static string parentName = "BundleParent";

        void Start()
        {
	        Utilla.Events.GameInitialized += OnGameInitialized;
        }




		void OnDisable()
		{
			assetBundleParent.SetActive(false);
		}
		
		[ModdedGamemodeJoin]
		public void OnJoin(string gamemode)
		{
			/* Activate your mod here */
			/* This code will run regardless of if the mod is enabled*/

			assetBundleParent.SetActive(true);
				

			inRoom = true;
		}
		
		[ModdedGamemodeLeave]
		public void OnLeave(string gamemode)
		{
			/* Deactivate your mod here */
			/* This code will run regardless of if the mod is enabled*/
		assetBundleParent.SetActive(false);
			inRoom = false;
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
			instance = this;

			//This loads the asset bundle put in the AssetBundles folder
			bundle = LoadAssetBundle("HomeyTreeHouse.AssetBundles." + assetBundleName);

			//Spawn in Parent
			assetBundleParent = Instantiate(bundle.LoadAsset<GameObject>(parentName));

			//Set Parent Position
			assetBundleParent.transform.position = new Vector3(-67.2225f, 11.57f, -82.611f);

			ScreenOutline = assetBundleParent.GetNamedChild("ScreenOutline");
			ScreenOutline.AddComponent<GorillaSurfaceOverride>().overrideIndex = 0;
			
			Screen = assetBundleParent.GetNamedChild("Screen");
			Screen.AddComponent<GorillaSurfaceOverride>().overrideIndex = 120;
			
			Couch = assetBundleParent.GetNamedChild("Couch");
			Couch.AddComponent<GorillaSurfaceOverride>().overrideIndex = 119;
			
			Carpet = assetBundleParent.GetNamedChild("Carpet");
			Carpet.AddComponent<GorillaSurfaceOverride>().overrideIndex = 119;
			
			Window = assetBundleParent.GetNamedChild("Window");
			Window.AddComponent<GorillaSurfaceOverride>().overrideIndex = 29;
			
			assetBundleParent.SetActive(false);
        }

        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }
    }
}
