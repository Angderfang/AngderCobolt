using Angder.Angdermod;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.Angdermod.Cards;

internal sealed class CardAnticipation : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Anticipation", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Anticipation", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 1,
            art = ModEntry.Instance.Angder_Anticipate.Sprite,
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
                        targetPlayer = true,
                        statusAmount = 1
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Rampage.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    },

                    /* "WAIT? This is just Board but worse?
                    Well, no, if you play this card and board in the same turn, thats 4 rampage. 
                    If you play just two boards, you get two rampage, and a confused Angder. */

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.tempShield,
                        targetPlayer = true,
                        statusAmount = 1
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Rampage.Status,
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
                        status = Status.tempShield,
                        targetPlayer = true,
                        statusAmount = 3
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Rampage.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                };
                break;
        }
        return actions;
    }
}
