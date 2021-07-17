using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using HarmonyLib;

namespace O21CarryCapacity
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            Harmony harmony = new Harmony("neronix17.utility.carrycapcontrol");
            harmony.PatchAll();
        }

    }

    [HarmonyPatch(typeof(MassUtility), "Capacity")]
    [HarmonyPriority(999)]
    public static class MassUtility_Capacity_Prefix
    {
        [HarmonyPrefix]
        public static bool Prefix(ref float __result, ref Pawn p, ref StringBuilder explanation)
        {
            if (!MassUtility.CanEverCarryAnything(p))
            {
                __result = 0f;
            }
            else
            {
                float num = StatDefOf.CarryingCapacity.defaultBaseValue * p.BodySize * CarryCapMod.settings.capacityMultiplier  * Current.Game.Scenario.GetStatFactor(StatDefOf.CarryingCapacity);
                if (explanation != null)
                {
                    if (explanation.Length > 0)
                    {
                        explanation.AppendLine();
                    }
                    explanation.Append("  - " + p.LabelShortCap + ": " + num.ToStringMassOffset());
                }
                __result = num;
            }
            return false;
        }
    }
}
