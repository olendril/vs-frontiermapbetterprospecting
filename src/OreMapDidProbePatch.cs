using HarmonyLib;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace FrontierMapBetterProspecting;

[HarmonyPatch(typeof(ModSystemOreMap), nameof(ModSystemOreMap.DidProbe))]
internal static class OreMapDidProbePatch
{
    [HarmonyPostfix]
    [HarmonyPriority(Priority.Last)]
    private static void Postfix(PropickReading results, IServerPlayer splr)
    {
        ProspectingResultBridge.Send(results, splr);
    }
}
