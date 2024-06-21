using Angder.Angdermod;
using Nickel;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;


namespace Angder.Angdermod.Cards;

internal sealed class CardBoard : Card, IAngderCard
{

    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Board", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Board", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Airlock.Sprite,
            cost = upgrade == Upgrade.B ? 0: 1,

            /* if we don't set a card specific 'art' (a 'Spr' type) here, the game will give it the deck's 'DefaultCardArt' */
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {

        //The basic Boarding card. Ironically not a starter anymore.

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

                    new AStatus()
                    {
                        status = ModEntry.Instance.Angdermissing.Status,
                        targetPlayer = true,
                        statusAmount = 1
                    },

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {

                    new AStatus()
                    {
                        status = ModEntry.Instance.Rampage.Status,
                        statusAmount = 3,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = ModEntry.Instance.Angdermissing.Status,
                        targetPlayer = true,
                        statusAmount = 1
                    },

                };
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

                    new AStatus()
                    {
                        status = ModEntry.Instance.Angdermissing.Status,
                        targetPlayer = true,
                        statusAmount = 2
                    },
                };
                break;
        }
        return actions;
    }
}
