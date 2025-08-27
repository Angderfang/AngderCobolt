using Angder.EchoesOfTheFuture.Features;
using FSPRO;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Angder.EchoesOfTheFuture.Patches;

public static class FortressPatch
{
    static ModEntry Instance => ModEntry.Instance;


       
    internal static void Apply(Harmony harmony)
    {
        harmony.Patch(
            //logger: ModEntry.Instance.Logger,
            original: AccessTools.DeclaredMethod(typeof(AMove), nameof(AMove.Begin)),
            postfix: new HarmonyMethod(typeof(FortressPatch), nameof(AMoveBegin_Postfix))
            //postfix: new HarmonyMethod(typeof(MGPatches), nameof(Ship_Set_Postfix))
        );
    }

    private static void AMoveBegin_Postfix(AMove __instance, G g, State s, Combat c)
    {
        if (s.ship.Get(ModEntry.Instance.Fortress.Status) > 0 && __instance.targetPlayer == true)
        {
            Status Fortressthing = ModEntry.Instance.Fortress.Status;
            int Fortressnumber = s.ship.Get(Fortressthing);
            s.ship._Set(ModEntry.Instance.Fortress.Status, Fortressnumber - 1);
            Audio.Play(Event.Status_PowerDown);
        }
        else if (c.otherShip.Get(ModEntry.Instance.Fortress.Status) > 0 && __instance.targetPlayer == false)
        {
            Status Fortressthing = ModEntry.Instance.Fortress.Status;
            int Fortressnumber = c.otherShip.Get(Fortressthing);
            c.otherShip._Set(ModEntry.Instance.Fortress.Status, Fortressnumber - 1);
            Audio.Play(Event.Status_PowerDown);
        }
    }
}
    