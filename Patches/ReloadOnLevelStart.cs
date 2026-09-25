using HarmonyLib;
using System;

namespace RepoModTemplate.Patches
{
	[HarmonyPatch(typeof(EnemyDirector), "Start")]
	internal class ReloadOnLevelStart
	{
		static void Postfix()
		{
			if (!SemiFunc.RunIsLevel()) return;
			ConfigurationController.Reload();
		}
	}
}
