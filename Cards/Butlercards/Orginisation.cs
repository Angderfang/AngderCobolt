using Angder.EchoesOfTheFuture;
using Angder.EchoesOfTheFuture.Features;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardOrginization : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Orginization", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Orginization", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Maid_Trashfire.Sprite,
            cost = 1,
            retain = false,

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
                    new AVariableHintExhaustFive
                    {
                        status = ModEntry.Instance.Exhaustover5.Status
                    },
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = Exhaustcount/5,
                        targetPlayer = true,
                        xHint = 1
                    },

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AVariableHintExhaustFive
                    {
                        status = ModEntry.Instance.Exhaustover5.Status
                    },

                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 1,
                        targetPlayer = true,
                    },

                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = Exhaustcount/5,
                        targetPlayer = true,
                        xHint = 1
                    },



                };
                break;
            case Upgrade.B:
                actions = new()
                {

                    new AVariableHintExhaustFive
                    {
                        status = ModEntry.Instance.Exhaustover5.Status
                    },
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = Exhaustcount/5,
                        targetPlayer = true,
                        xHint = 1
                    },
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = Exhaustcount/5,
                        targetPlayer = true,
                        xHint = 1
                    },
                };
                break;
        }
        return actions;
    }
}
