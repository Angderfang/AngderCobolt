using Angder.EchoesOfTheFuture;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class Flow : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Flow", new()
        {


            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.uncommon,
                
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Flow", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            description = ModEntry.Instance.Localizations.Localize(["card", "Flow", "description", upgrade.ToString()]),
            art = ModEntry.Instance.Bookscard.Sprite,
            cost = 0,
            singleUse = true,
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
                    //limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    },
                };
        break;
            case Upgrade.A:
                actions = new()
                {
                new ACardOffering
                    {
                    amount = 5,
                    //limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = true,
                    inCombat = true,
                    },
                };

                break;
            case Upgrade.B:
                actions = new()
                {
                new ACardOffering
                    {
                    amount = 1,
                    //limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    discount = -3,
                    },
                new ACardOffering
                    {
                    amount = 1,
                    //limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    discount = -3,
                    },
                new ACardOffering
                    {
                    amount = 1,
                    //limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    discount = -3,
                    },
                };
        break;
        }
        return actions;
    }
}
