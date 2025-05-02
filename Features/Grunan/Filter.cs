using System;
using System.Collections.Generic;
using System.Linq;
using Nanoray.Shrike;
using Nanoray.Shrike.Harmony;
using Nickel;
using HarmonyLib;
using System.Reflection.Emit;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Features.Grunan;
#nullable enable

public class CardBrowseFilterManager
{
    static ModEntry Instance => ModEntry.Instance;
    static Harmony Harmony => Instance.Harmony;
    static IModData ModData => Instance.Helper.ModData;

    internal const string FilterOnlySingleUseKey = "FilterOnlySingleUse";

    public CardBrowseFilterManager()
    {
        Harmony.Patch(
            //logger: Instance.Logger,
            original: AccessTools.DeclaredMethod(typeof(ACardSelect), nameof(ACardSelect.BeginWithRoute)),
            transpiler: new HarmonyMethod(GetType(), nameof(ACardSelect_BeginWithRoute_Transpiler))
        );
        Harmony.Patch(
            //logger: Instance.Logger,
            original: AccessTools.DeclaredMethod(typeof(CardBrowse), nameof(CardBrowse.GetCardList)),
            postfix: new HarmonyMethod(GetType(), nameof(CardBrowse_GetCardList_Postfix))
        );
    }

    private static IEnumerable<CodeInstruction> ACardSelect_BeginWithRoute_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il, MethodBase originalMethod)
    {
        return new SequenceBlockMatcher<CodeInstruction>(instructions)
            .Find(
                ILMatches.Newobj(typeof(CardBrowse).GetConstructor([])!),
                ILMatches.Instruction(OpCodes.Dup)
            )
            .Insert(SequenceMatcherPastBoundsDirection.After, SequenceMatcherInsertionResultingBounds.IncludingInsertion, new List<CodeInstruction> {
                new(OpCodes.Dup),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Call, AccessTools.DeclaredMethod(typeof(CardBrowseFilterManager), nameof(CopyDataToCardBrowse))),
            })
            .AllElements();
    }

    private static void CopyDataToCardBrowse(CardBrowse cardBrowse, ACardSelect cardSelect)
    {
        //Console.WriteLine("Checkingcopy");
        if (ModData.TryGetModData<bool>(cardSelect, FilterOnlySingleUseKey, out var FilterOnlySingleUse))
            ModData.SetModData(cardBrowse, FilterOnlySingleUseKey, FilterOnlySingleUse);
    }


    private static void CardBrowse_GetCardList_Postfix(CardBrowse __instance, ref List<Card> __result, G g)
    {
        bool doesFilterSingleuse = ModData.TryGetModData<bool>(__instance, FilterOnlySingleUseKey, out var FilterOnlySingleUse);
        Combat combat = g.state.route as Combat ?? DB.fakeCombat;
        if ((doesFilterSingleuse) && __instance.browseSource != CardBrowse.Source.Codex)
        {
            __result.RemoveAll(delegate (Card c)
            {
                //CardData data = c.GetDataWithOverrides(g.state);
                //Console.WriteLine("Checking");
                if (doesFilterSingleuse)
                {
                    //Console.WriteLine("HELLO2");
                    //Console.WriteLine(FilterOnlySingleUse);
                    //Console.WriteLine(c.GetDataWithOverrides(g.state).singleUse);
                    if (c.GetDataWithOverrides(g.state).singleUse != FilterOnlySingleUse || GrunanTraitManager.IsUntrashable(c, g.state))
                    {
                        //Console.WriteLine("FILTERED");
                        return true;
                    }
                }

                return false;
            });
        }
    }
}