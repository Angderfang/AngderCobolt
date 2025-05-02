using Angder.EchoesOfTheFuture;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class Trails : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Trails", new()
        {
            

            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.common,
               
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Trails", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            //description = ColorlessLoc.GetDesc(state, upgrade == Upgrade.B ? 3 : 2, (Deck)ModEntry.Instance.AngderDeck.Deck),
            cost = upgrade == Upgrade.B ? 1 : 0,
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
                        status = Status.evade,
                        statusAmount = 1,
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
                        status = Status.evade,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                };

                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 3,
                        targetPlayer = true
                    },
                };
                break;
        }
        return actions;
    }
}
