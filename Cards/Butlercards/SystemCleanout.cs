using Angder.EchoesOfTheFuture;
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
            art = ModEntry.Instance.Angder_Instinct.Sprite,
            cost = 3,
            exhaust = upgrade == Upgrade.B ? false : true

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
                        status = Status.powerdrive,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 0,
                        targetPlayer = true,
                        mode = AStatusMode.Set
                    },
                    new AExhaustEntireHand()
                    {

                    },

                    new AEndTurn()
                    {

                    },
                    /* */

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {

                    new AStatus()
                    {
                        status = Status.powerdrive,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = Status.overdrive,
                        statusAmount = 3,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 0,
                        targetPlayer = true,
                        mode = AStatusMode.Set
                    },
                    new AExhaustEntireHand()
                    { 

                    },

                    new AEndTurn()
                    {

                    },


                };
                break;
            case Upgrade.B:
                actions = new()
                {

                    new AStatus()
                    {
                        status = Status.powerdrive,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 0,
                        targetPlayer = true,
                        mode = AStatusMode.Set
                    },
                    new AExhaustEntireHand()
                    {

                    },

                    new AEndTurn()
                    {

                    },

                };
                break;
        }
        return actions;
    }
}
