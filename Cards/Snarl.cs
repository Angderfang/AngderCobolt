using Angder.Angdermod;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.Angdermod.Cards;

internal sealed class CardSnarl : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Snarl", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Snarl", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Instinct.Sprite,
            cost = 1,
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
                        status = Status.overdrive,
                        statusAmount = 2,
                        targetPlayer = true
                    },

                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 0,
                        targetPlayer = true,
                        mode = AStatusMode.Set
                    }

                    /* "overdrive is very powerful. */
                    //Isn't it a bit of an antisynergy?
                    /* It doesn't synergise with 4 of Angders cards, assuming no firing regulator */

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {

                    new AStatus()
                    {
                        status = Status.overdrive,
                        statusAmount = 2,
                        targetPlayer = true
                    },


                };
                break;
            case Upgrade.B:
                actions = new()
                {

                    new AStatus()
                    {
                        status = Status.overdrive,
                        statusAmount = 2,
                        targetPlayer = true
                    },

                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 0,
                        targetPlayer = true,
                        mode = AStatusMode.Set
                    }

                };
                break;
        }
        return actions;
    }
}
