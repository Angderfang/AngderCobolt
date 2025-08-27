using Angder.EchoesOfTheFuture;
using Angder.EchoesOfTheFuture.Features;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardSystemCleanout : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SystemCleanout", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "SystemCleanout", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Powerdiversion.Sprite,
            cost = 2,
            exhaust = true

        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        int Exhaustcount = c.exhausted.Count;
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {

                    new AVariableHintExhaustThree
                    {
                        status = ModEntry.Instance.Exhaustover3.Status
                    },

                    new AStatus()
                    {
                       status = Status.maxShield,
                       statusAmount = Exhaustcount/3,
                       xHint = 1,
                       targetPlayer = true
                    },
                    new AStatus()
                    {
                       status = Status.tempShield,
                       statusAmount = Exhaustcount/3,
                       xHint = 1,
                       targetPlayer = true
                    },
                    /* */

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AVariableHintExhaustThree
                    {
                        status = ModEntry.Instance.Exhaustover3.Status
                    },
                    new AStatus()
                    {
                       status = Status.maxShield,
                       statusAmount = Exhaustcount/3,
                       xHint = 1,
                       targetPlayer = true
                    },
                    new AStatus()
                    {
                       status = Status.shield,
                       statusAmount = Exhaustcount/3,
                       xHint = 1,
                       targetPlayer = true
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AVariableHintExhaustThree
                    {
                        status = ModEntry.Instance.Exhaustover3.Status
                    },
                    new AStatus()
                    {
                       status = Status.maxShield,
                       statusAmount = Exhaustcount/3,
                       xHint = 1,
                       targetPlayer = true
                    },
                    new AStatus()
                    {
                       status = Status.perfectShield,
                       statusAmount = 1,
                       targetPlayer = true
                    },

                };
                break;
        }
        return actions;
    }
}
