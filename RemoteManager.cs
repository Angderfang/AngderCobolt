/*Thank you Randall, A lot of this code came from studying you. */

using Angder.Angdermod.Cards;
using FMOD.Studio;
using HarmonyLib;
using System;

namespace Angder.Angdermod;



public sealed class RemoteManager
{

    private static Card? Cardforpostfix;
    private static State? Thisstate;
    public RemoteManager()
    {
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Combat), nameof(Combat.TryPlayCard)),
            prefix: new HarmonyMethod(AccessTools.DeclaredMethod(typeof(RemoteManager), nameof(HarmonyPrefix_Combat_TryPlayCard))),

            finalizer: new HarmonyMethod(GetType(), nameof(TryCard_Finalizer))
        );
        //Render is a real pain
            ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Card), nameof(Card.Render)),
            prefix: new HarmonyMethod(AccessTools.DeclaredMethod(typeof(RemoteManager), nameof(HarmonyPrefix_Card_Render))),

            finalizer: new HarmonyMethod(GetType(), nameof(TryCard_Finalizer))
        //Seriously, how do I fix this when loading a save?
        );
        //do not readd debug text from this; it lags the game.
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(State), nameof(State.CharacterIsMissing)),
            postfix: new HarmonyMethod(AccessTools.DeclaredMethod(typeof(RemoteManager), nameof(HarmonyPostfix_State_Characterismissing)))
       
        );
    }

    private static ModEntry Instance => ModEntry.Instance;

    public static bool IsRemoteControlled(Card card, State state)
        => Instance.Helper.Content.Cards.IsCardTraitActive(state, card, Instance.RemoteControl);

    public static void SetRemote(Card card, State state, bool value)
        => Instance.Helper.Content.Cards.SetCardTraitOverride(state, card, Instance.RemoteControl, value, permanent: false);

    public static void SetRemoteUnending(Card card, State state, bool value)
    => Instance.Helper.Content.Cards.SetCardTraitOverride(state, card, Instance.RemoteControl, value, permanent: true);
    /*
    public static bool RemoteOverride(Card Test, State s)
    {
        if (IsRemoteControlled(Test, s))
        {
            return true;
        }
        return false;
    }
    */
    [HarmonyPatch(typeof(Card), nameof(Card.Render))]
    static void HarmonyPrefix_Card_Render(ref Card __instance, G g, Vec? posOverride = null, State? fakeState = null, bool ignoreAnim = false, bool ignoreHover = false, bool hideFace = false, bool hilight = false, bool showRarity = false, bool autoFocus = false, UIKey? keyOverride = null, OnMouseDown? onMouseDown = null, OnMouseDownRight? onMouseDownRight = null, OnInputPhase? onInputPhase = null, double? overrideWidth = null, UIKey? leftHint = null, UIKey? rightHint = null, UIKey? upHint = null, UIKey? downHint = null, int? renderAutopilot = null, bool? forceIsInteractible = null, bool reportTextBoxesForLocTest = false, bool isInCombatHand = false)
    {
        Cardforpostfix = __instance;
    }
    public static void Getfix(Card card, State s)
    {
        Cardforpostfix = card;
        Thisstate = s;
    }
    private static void TryCard_Finalizer()
    => Cardforpostfix = null;

    [HarmonyPatch(typeof(Combat), nameof(Combat.TryPlayCard))]
    static void HarmonyPrefix_Combat_TryPlayCard(State s, Card card, bool playNoMatterWhatForFree = false,  bool exhaustNoMatterWhat = false)
    {
        //Console.WriteLine("Playing card");
        Getfix(card, s);
    }
    private static bool HarmonyPostfix_State_Characterismissing(bool __result, Deck? deck)
    {
        //Console.WriteLine("Missing detected"); //LAGGY AS BALLS. DON'T TURN THIS ON UNLESS YOU NEED IT
        if (Cardforpostfix != null && Thisstate != null && IsRemoteControlled(Cardforpostfix, Thisstate))
        {
            //Console.WriteLine("Lets skip this");
            return false;
        }
        return __result;
        /*do something to disrupt it, while still letting the rest of the code take place */
    }


      /*do something to disrupt it, while still letting the rest of the code take place */
    /* I could just copy the entire block of code, and add my own return, that's dumb, and may break other mods.
     I could do a transpiler fix thingy, thats hard
    I could maybe switch the deck if it's remote controlled? Except the card used is "card2" (which makes my initial method not work, and accurately switching it back will be close to impossible
    Override Character is missing directly? Works in theory, but it's not supplied with the card. So I can't easily modify it.
     */


}
