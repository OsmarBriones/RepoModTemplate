using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace RepoModTemplate
{
	[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
	public class RepoModTemplatePlugin : BaseUnityPlugin
	{
		// Se reemplaza AUTHOR_ID por el parámetro AuthorId del template.json
		// y RepoModTemplate por el nombre del proyecto (sourceName).
		public const string PluginGuid = "AUTHOR_ID.RepoModTemplate";
		public const string PluginName = "RepoModTemplate";
		public const string PluginVersion = "1.0.0";

		internal Harmony? Harmony { get; set; }
		internal static new BepInEx.Logging.ManualLogSource Logger { get; private set; } = null!;

		private void Awake()
		{
			Logger = base.Logger;

			// Prevent the plugin from being deleted
			this.gameObject.transform.parent = null;
			this.gameObject.hideFlags = HideFlags.HideAndDontSave;

			ConfigurationController.Initialize(this.Config);

			Harmony = new Harmony(Info.Metadata.GUID);
			Harmony.PatchAll();

			Logger.LogInfo($"{PluginName} {PluginVersion} loaded!");
		}

		internal void Unpatch()
		{
			Harmony?.UnpatchSelf();
		}
	}
}
