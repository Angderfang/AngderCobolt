using Nickel;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardDiagnosticComplete : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("DiagnosticComplete", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.AngderstrashDeck.Deck,

                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]


            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "DiagnosticComplete", "name"]).Localize,
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_ManyBulletMuchwow.Sprite,
            unplayable = true,
            cost = 0,
            exhaust = true,
            temporary = true,
            description = ModEntry.Instance.Localizations.Localize(["card", "DiagnosticComplete", "description", upgrade.ToString()])
        };
        return data;
    }
    public override void OnDraw(State s, Combat c)
    {

        switch (upgrade)
        {
            case Upgrade.None:

                c.Queue(new ADrawCard()
                {
                    count = 1,
                });
                c.Queue(new AStatus()
                {
                    statusAmount = 2,
                    status = Status.shield,
                    targetPlayer = true,
                });
                break;

            case Upgrade.A:
                c.Queue(new ADrawCard()
                {
                    count = 2,
                });
                c.Queue(new AStatus()
                {
                    statusAmount = 2,
                    status = Status.shield,
                    targetPlayer = true,
                });

                break;
            case Upgrade.B:
                c.Queue(new AStatus()
                {
                    statusAmount = 4,
                    status = Status.shield,
                    targetPlayer = true,
                });
                break;
        }
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                new ADrawCard()
                {
                    count = 1,
                },
                    new AStatus()
                {
                    statusAmount = 2,
                    status = Status.shield,
                }
                };
                c.Queue(new ASelfExhaust()
                {
                    CardID = uuid
                });
                break;
            case Upgrade.A:
                actions = new()
                {
                    new ADrawCard()
                {
                    count = 2,
                },
                    new AStatus()
                {
                    statusAmount = 2,
                    status = Status.shield,
                }
                };
                c.Queue(new ASelfExhaust()
                {
                    CardID = uuid
                });
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                {
                    statusAmount = 4,
                    status = Status.shield,
                }
                };
                c.Queue(new ASelfExhaust()
                {
                    CardID = uuid
                });
                break;
        }
        return actions;

    }
}
