using Angder.EchoesOfTheFuture;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardResetShields : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ResetShields", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ResetShields", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Maid_Chute.Sprite,
            cost = upgrade == Upgrade.B ? 1 : 0,

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
                        status = Status.maxShield,
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
                        status = Status.maxShield,
                        statusAmount = 2,
                        targetPlayer = true
                    },

                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 1,
                        targetPlayer = true,
                        mode = AStatusMode.Set
                    }



                };
                break;
            case Upgrade.B:
                actions = new()
                {

                    new AStatus()
                    {
                        status = Status.maxShield,
                        statusAmount = 4,
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
