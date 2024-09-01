using Angder.EchoesOfTheFuture;
using Nickel;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardDeepBreaths : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("DeepBreaths", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "DeepBreaths", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Deepbreath.Sprite,
            cost = upgrade == Upgrade.B ?  2 : 1,
            exhaust = true,
            retain = upgrade == Upgrade.A ? true : false,
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        /* The meat of the card, this is where we define what a card does, and some would say the most fun part of modding Cobalt Core happens here! */
        List<CardAction> actions = new();

        /* Since we want to have different actions for each Upgrade, we use a switch that covers the Upgrade paths we've defined */
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new AVariableHint
                    {
                        status = ModEntry.Instance.Rampage.Status
                    },

                    new ADrawCard()
                    {
                       count = GetRampageAmt(s),
                       xHint = 1
                    },

                    //Actually one of the least useful cards in Angder's deck IMO, and is B ever good? Eh, need to ship it anyway.
                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AVariableHint
                    {
                        status = ModEntry.Instance.Rampage.Status
                    },

                    new ADrawCard()
                    {
                       count = GetRampageAmt(s),
                       xHint = 1
                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AVariableHint
                    {
                        status = ModEntry.Instance.Rampage.Status
                    },

                    new ADrawCard()
                    {
                       count = GetRampageAmt(s),
                       xHint = 1
                    },

                    new AStatus()
                    {
                        status = Status.drawNextTurn,
                        statusAmount = GetRampageAmt(s),
                        mode = AStatusMode.Add,
                        targetPlayer = true,
                        xHint = 1
                    },
                };
                break;
        }
        return actions;
    }
    private int GetRampageAmt(State s)
    {
        int result = 0;
        if (s.route is Combat)
        {
            result = s.ship.Get(ModEntry.Instance.Rampage.Status);
        }

        return result;
    }
}
