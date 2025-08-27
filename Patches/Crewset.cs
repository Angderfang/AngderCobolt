using Angder.EchoesOfTheFuture.Features;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Angder.EchoesOfTheFuture.Patches;

public static class Capcrew
{
    static ModEntry Instance => ModEntry.Instance;

    internal static void Apply(Harmony harmony)
    {
        harmony.Patch(
            //logger: ModEntry.Instance.Logger,
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.Set)),
            postfix: new HarmonyMethod(typeof(Capcrew), nameof(Ship_Set_Postfix))
            //postfix: new HarmonyMethod(typeof(MGPatches), nameof(Ship_Set_Postfix))
        );
    }

    private static void Ship_Set_Postfix(Ship __instance, Status status, int n = 1)
    {
        Status crew = Instance.StatusConstructioncrew.Status;
        int num5 = __instance.Get(crew);
        int maxCrew = 2;
        if (num5 > 2)
        {
            __instance._Set(crew, maxCrew);
        }
    }
}
    