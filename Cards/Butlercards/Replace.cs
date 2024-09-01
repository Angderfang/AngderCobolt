using Angder.EchoesOfTheFuture.Features;
using Nickel;
//using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards;


internal sealed class CardReplace : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("CodeReplace", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,
                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Replace", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Maid_Chute.Sprite,
            cost = 0,
            exhaust = true,
            retain = true
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
                    browseSource = CardBrowse.Source.Hand,
                    },
                    new ADrawCard()
                    {
                        count = 1,
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
                    browseSource = CardBrowse.Source.Hand,
                    },
                    new ADrawCard()
                    {
                        count = 2,
                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new Dontplaycardsearch
                    {
                        browseAction = new Dontplaycard(),
                        browseSource = CardBrowse.Source.Hand,
                    },
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.drawNextTurn,
                        statusAmount = 3
                    },
                };
                break;
        }
        return actions;
    }
}
