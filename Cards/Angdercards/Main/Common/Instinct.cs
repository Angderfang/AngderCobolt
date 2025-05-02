using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.EchoesOfTheFuture.Cards.Angdercards;

internal sealed class CardInstinct : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Instinct", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Instinct", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Instinct.Sprite,
            cost = upgrade == Upgrade.B ? 0 : 1,
            //exhaust = true
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
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 4,
                        mode = AStatusMode.Add,
                        targetPlayer = true
                    },

                    new AStatus()
                    {
                        status = Status.drawLessNextTurn,
                        statusAmount = 1,
                        mode = AStatusMode.Add,
                        targetPlayer = true
                    },
                    /* Instinct does what Angder thinks is best */
                    //Probably a bad card

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {

                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 6,
                        mode = AStatusMode.Add,
                        targetPlayer = true
                    },

                    new AStatus()
                    {
                        status = Status.drawLessNextTurn,
                        statusAmount = 1,
                        mode = AStatusMode.Add,
                        targetPlayer = true
                    },


                };
                break;
            case Upgrade.B:
                actions = new()
                {

                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 6,
                        mode = AStatusMode.Add,
                        targetPlayer = true
                    },

                    new AStatus()
                    {
                        status = Status.drawLessNextTurn,
                        statusAmount = 2,
                        mode = AStatusMode.Add,
                        targetPlayer = true
                    },
                };
                break;
        }
        return actions;
    }
}
