using Angder.EchoesOfTheFuture;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class Greed : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Greed", new()
        {
            

            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.common,
               
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Greed", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            //description = ColorlessLoc.GetDesc(state, upgrade == Upgrade.B ? 3 : 2, (Deck)ModEntry.Instance.AngderDeck.Deck),
            cost = 0,
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
                    new ADrawCard()
                    {
                       count = 1,
                    },
                    new AStatus()
                    {
                       status = Status.drawNextTurn,
                       statusAmount = 2,
                       targetPlayer = true
                    },
                };

        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                    new ADrawCard()
                    {
                       count = 2,
                    },
                    new AStatus()
                    {
                       status = Status.drawNextTurn,
                       statusAmount = 2,
                       targetPlayer = true
                    },
                };

                break;
            case Upgrade.B:
                actions = new()
                {
                    new ADrawCard()
                    {
                       count = 10,
                    },
                    new AHurt()
                    {
                        targetPlayer = true,
                       hurtAmount = 2,
                    },
                };
        break;
        }
        return actions;
    }
}
