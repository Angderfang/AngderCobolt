using Angder.EchoesOfTheFuture;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class AngderEXE : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Angder.EXE", new()
        {


            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = Deck.colorless,

                rarity = Rarity.common,
                
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "AngderEXE", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            description = ModEntry.Instance.Localizations.Localize(["card", "AngderEXE", "description", upgrade.ToString()]),
            //description = ColorlessLoc.GetDesc(state, upgrade == Upgrade.B ? 3 : 2, (Deck)ModEntry.Instance.AngderDeck.Deck),
            cost = upgrade == Upgrade.A ? 0 : 1,
            exhaust = true
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
                    amount = 2,
                    limitDeck = ModEntry.Instance.AngderDeck.Deck,
                    makeAllCardsTemporary = true,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    discount = -1,
                    //dialogueSelector = ".summonAngder"
                    },
                new AAddCard()
                    {
                    card = new CardAngderBot(){
                    //upgrade = Upgrade.B
                    },
                        destination = CardDestination.Hand,
                        amount = 1,
                    },
                };
        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                new ACardOffering
                    {
                    amount = 2,
                    limitDeck = ModEntry.Instance.AngderDeck.Deck,
                    makeAllCardsTemporary = true,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    discount = -1,
                    //dialogueSelector = ".summonAngder"
                    },
                new AAddCard()
                {
                    card = new CardAngderBot(),
                    destination = CardDestination.Hand,
                    amount = 1,
                }
                };

                break;
            case Upgrade.B:
                actions = new()
                {
                new ACardOffering
                    {
                    amount = 3,
                    limitDeck = ModEntry.Instance.AngderDeck.Deck,
                    makeAllCardsTemporary = true,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    discount = -1,
                    //dialogueSelector = ".summonAngder"
                    },
                new AAddCard()
                {
                    card = new CardAngderBot()
                    {
                    },
                    destination = CardDestination.Hand,
                    amount = 1,
                }
                };
        break;
        }
        return actions;
    }
}
