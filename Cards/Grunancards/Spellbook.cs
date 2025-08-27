using Angder.EchoesOfTheFuture;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class SpellBook : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SpellBooks", new()
        {


            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.uncommon,
               
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Tome", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            description = ModEntry.Instance.Localizations.Localize(["card", "Tome", "description", upgrade.ToString()]),
            art = ModEntry.Instance.Bookscard.Sprite,
            cost = upgrade == Upgrade.B ? 0 : 1,
            singleUse = true
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                new ACardOffering
                    {
                    amount = 3,
                    limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    //discount = -1,
                    //dialogueSelector = ".summonAngder"
                    },
                    new ACardOffering
                    {
                    amount = 3,
                    limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    //discount = -1,
                    //dialogueSelector = ".summonAngder"
                    },
                };

        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                new ACardOffering
                    {
                    amount = 4,
                    limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    //discount = -1,
                    //dialogueSelector = ".summonAngder"
                    },
                new ACardOffering
                    {
                    amount = 4,
                    limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    //discount = -1,
                    //dialogueSelector = ".summonAngder"
                    },
                };

                break;
            case Upgrade.B:
                actions = new()
                {
                new ACardOffering
                {
                    amount = 2,
                    limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    //dialogueSelector = ".summonAngder"
                },
                new ACardOffering
                {
                    amount = 2,
                    limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    //dialogueSelector = ".summonAngder"
                },
                new ACardOffering
                {
                    amount = 2,
                    limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    //dialogueSelector = ".summonAngder"
                },
                new ACardOffering
                {
                    amount = 2,
                    limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    //dialogueSelector = ".summonAngder"
                },
                //Keep going until you reject a card.
                };
        break;
        }
        return actions;
    }
}
