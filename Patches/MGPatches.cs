﻿using HarmonyLib;

namespace Angder.EchoesOfTheFuture;


//I HAVE NO IDEA WHAT THIS DOES. I JUST COPIED IT FROM SHOCKAH TO MAKE DIALOG WORK. I AM A FRAUD, A THIEF. AN IMPOSTER.
internal static class MGPatches
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(MG), "DrawLoadingScreen"),
            prefix: new HarmonyMethod(typeof(MGPatches), nameof(DrawLoadingScreen_Prefix)),
            postfix: new HarmonyMethod(typeof(MGPatches), nameof(DrawLoadingScreen_Postfix))
        );
    }

    private static void DrawLoadingScreen_Prefix(MG __instance, ref int __state)
        => __state = __instance.loadingQueue?.Count ?? 0;

    private static void DrawLoadingScreen_Postfix(MG __instance, ref int __state)
    {
        if (__state <= 0)
            return;
        if ((__instance.loadingQueue?.Count ?? 0) > 0)
            return;
        Dialogue.Inject();
    }
}