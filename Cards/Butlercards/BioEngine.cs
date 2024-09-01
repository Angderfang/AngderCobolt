using Angder.EchoesOfTheFuture.Features;
using Nickel;
//using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards;


internal sealed class CardBioEngine : Card, IAngderCard
{
    
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("BioEngine", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "BioEngine", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Maid_Trashfire.Sprite,
            cost = 1,
            retain = upgrade == Upgrade.A ? true : false,
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
                    new AVariableHintExhaustFive
                    {
                        status = ModEntry.Instance.Exhaustover5.Status
                    },
                    new AEnergy()
                    {
                       changeAmount = Exhaustcount/5,
                       xHint = 1
                    },
                    new ADrawCard()
                    {
                       count = Exhaustcount/5,
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
                    new AEnergy()
                    {
                       changeAmount = Exhaustcount/5,
                       xHint = 1
                    },
                    new ADrawCard()
                    {
                       count = Exhaustcount/5,
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
                    new AEnergy()
                    {
                       changeAmount = Exhaustcount/5,
                       xHint = 1
                    },
                    new AStatus()
                    {
                        targetPlayer = true,
                       status = Status.energyNextTurn,
                       statusAmount = Exhaustcount/5,
                       xHint = 1
                    },
                    new ADrawCard()
                    {
                       count = 1
                    },
                };
                break;
        }
        return actions;
    }
}
