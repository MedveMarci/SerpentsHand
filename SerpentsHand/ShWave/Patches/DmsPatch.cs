using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Respawning;
using Respawning.Waves.Generic;

namespace SerpentsHand.ShWave.Patches;

[HarmonyPatch(typeof(DeadmanSwitch), nameof(DeadmanSwitch.OnUpdate))]
public static class DmsPatch
{
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new(instructions);
        MethodInfo getRespawnTokensMethod = AccessTools.PropertyGetter(typeof(ILimitedWave), nameof(ILimitedWave.RespawnTokens));
        MethodInfo helperMethod = AccessTools.Method(typeof(DmsPatch), nameof(HasShRespawnTokens));

        for (int i = 0; i < codes.Count - 5; i++)
        {
            if (codes[i].opcode != OpCodes.Ldloc_3 || codes[i + 1].opcode != OpCodes.Brtrue_S || codes[i + 2].opcode != OpCodes.Ret || codes[i + 3].opcode != OpCodes.Ldloc_2 || codes[i + 4].opcode != OpCodes.Callvirt || codes[i + 4].operand is not MethodInfo mi || mi != getRespawnTokensMethod)
                continue;

            Label exitLabel = codes[i + 2].labels.Count > 0 ? codes[i + 2].labels[0] : default;

            for (int j = i + 4; j < codes.Count - 3; j++)
            {
                if (codes[j].opcode != OpCodes.Ldloc_3 || codes[j + 1].opcode != OpCodes.Callvirt || codes[j + 1].operand is not MethodInfo mi2 || mi2 != getRespawnTokensMethod || codes[j + 2].opcode != OpCodes.Ldc_I4_0 || codes[j + 3].opcode != OpCodes.Ble_S)
                    continue;

                Label countdownLabel = (Label)codes[j + 3].operand;
                Label shCheckLabel = new();
                codes[j + 3].operand = shCheckLabel;

                Label retLabel = codes[j - 1].labels.Count > 0 ? codes[j - 1].labels[0] : exitLabel;

                codes.InsertRange(j + 4, [
                    new CodeInstruction(OpCodes.Call, helperMethod).WithLabels(shCheckLabel),
                    new CodeInstruction(OpCodes.Brtrue_S, retLabel),
                    new CodeInstruction(OpCodes.Br_S, countdownLabel)
                ]);
                return codes;
            }

            break;
        }

        return codes;
    }

    public static bool HasShRespawnTokens()
    {
        return WaveManager.TryGet(out SerpentsHandWave shWave) && shWave.RespawnTokens > 0;
    }
}