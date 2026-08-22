using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using FrontiersMap;

namespace FrontierMapBetterProspecting;

[HarmonyPatch]
internal static class ProspectingTooltipPatch
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(GuiDialogMap), "DrawProspEntry")
            ?? AccessTools.Method(typeof(GuiDialogMap), "OnDrawProspectingPanel")
            ?? throw new MissingMethodException(
                typeof(GuiDialogMap).FullName,
                "OnDrawProspectingPanel or DrawProspEntry"
            );
        yield return AccessTools.Method(typeof(GuiDialogMap), "DrawProspectingTooltip")
            ?? throw new MissingMethodException(
                typeof(GuiDialogMap).FullName,
                "DrawProspectingTooltip"
            );
    }

    private static IEnumerable<CodeInstruction> Transpiler(
        IEnumerable<CodeInstruction> instructions
    )
    {
        var replaced = false;

        foreach (var instruction in instructions)
        {
            if (
                instruction.opcode == OpCodes.Ldstr
                && instruction.operand is string format
                && format == "0.0"
            )
            {
                instruction.operand = "0.00";
                replaced = true;
            }

            yield return instruction;
        }

        if (!replaced)
        {
            throw new InvalidOperationException(
                "Frontier's Map prospecting tooltip format string was not found."
            );
        }
    }
}
