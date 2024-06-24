using Nickel;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardDistantYelling : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("DistantYelling", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderstrashDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "DistantYelling", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 1,
            retain = upgrade == Upgrade.B ? true : false,
            temporary = true,
            exhaust = true
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
                        status = ModEntry.Instance.Rampage.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                    //I think this might be bad, as well as bottled rage

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.B:
                actions = new()
                {

                    new AStatus()
                    {
                        status = ModEntry.Instance.Rampage.Status,
                        statusAmount = 3,
                        targetPlayer = true
                    },
                    /*
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    */

                };
                break;
            case Upgrade.A:
                actions = new()
                {

                    new AStatus()
                    {
                        status = ModEntry.Instance.Rampage.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                };
                break;
        }
        return actions;
    }
}
