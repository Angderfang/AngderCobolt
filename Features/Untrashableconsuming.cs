/*Thank you Randall, A lot of this code came from studying you. */

using Angder.EchoesOfTheFuture.Cards;
using FMOD.Studio;
using HarmonyLib;
using System;

namespace Angder.EchoesOfTheFuture;



public sealed class GrunanTraitManager
{
    private static ModEntry Instance => ModEntry.Instance;
    public static bool IsUntrashable(Card card, State state)
        => Instance.Helper.Content.Cards.IsCardTraitActive(state, card, Instance.Untrashable);

    public static bool IsVoid(Card card, State state)
    => Instance.Helper.Content.Cards.IsCardTraitActive(state, card, Instance.Consuming);

    public static void SetVoid(Card card, State state, bool value)
    => Instance.Helper.Content.Cards.SetCardTraitOverride(state, card, Instance.Consuming, value, permanent: false);

    public static void SetUntrashable(Card card, State state, bool value)
        => Instance.Helper.Content.Cards.SetCardTraitOverride(state, card, Instance.Untrashable, value, permanent: false);

    public static void SetUntrashableUnending(Card card, State state, bool value)
    => Instance.Helper.Content.Cards.SetCardTraitOverride(state, card, Instance.Untrashable, value, permanent: true);
}
