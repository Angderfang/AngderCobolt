using Angder.EchoesOfTheFuture.Features;
using Nickel;
//using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards;


internal sealed class CardQuickClean : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("QuickClean", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "QuickClean", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Maid_Quickclean.Sprite,
            cost = 1,
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
                    new Dontplaycardsearch
                    {
                    browseAction = new Dontplaycard(),
                    browseSource = CardBrowse.Source.DrawPile,
                    filterUUID = uuid,
                    },
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.shield,
                        statusAmount = 1
                    },
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.tempShield,
                        statusAmount = 1
                    },
        };
        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                    new Dontplaycardsearch
                    {
                    browseAction = new Dontplaycard(),
                    browseSource = CardBrowse.Source.DrawPile,
                    filterUUID = uuid,
                    },
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.shield,
                        statusAmount = 2
                    },
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.tempShield,
                        statusAmount = 1
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new Dontplaycardsearch
                    {
                    browseAction = new Dontplaycard(),
                    browseSource = CardBrowse.Source.DrawPile,
                    filterUUID = uuid,
                    },
                    new Dontplaycardsearch
                    {
                    browseAction = new Dontplaycard(),
                    browseSource = CardBrowse.Source.DiscardPile,
                    filterUUID = uuid,
                    },
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.shield,
                        statusAmount = 1
                    },
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.tempShield,
                        statusAmount = 1
                    },
                };
                break;
        }
        return actions;
    }
}
