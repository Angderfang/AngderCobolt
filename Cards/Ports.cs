using Angder.Angdermod;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.Angdermod.Cards;

internal sealed class CardPorts : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Ports", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.rare,
                
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Ports", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        RemoteManager.SetRemoteUnending(this, state, true);
        CardData data = new CardData()
        {
            exhaust = upgrade == Upgrade.B ? false : true,
            cost = 2,
            art = ModEntry.Instance.Angder_RemoteUplink.Sprite,
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
                    new AVariableHint
                    {
                        status = ModEntry.Instance.Angdermissing.Status
                    },

                    new AAddCard()
                    {
                        card = new CardExposedport(),
                        destination = CardDestination.Hand,
                        amount = GetAngdermissingAmt(s),
                        xHint = 1
                    },

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AVariableHint
                    {
                        status = ModEntry.Instance.Angdermissing.Status
                    },

                    new AAddCard()
                    {
                        card = new CardExposedport
                        {
                            upgrade = Upgrade.A
                        },
                        destination = CardDestination.Hand,
                        amount = GetAngdermissingAmt(s),
                        xHint = 1
                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AVariableHint
                    {
                        status = ModEntry.Instance.Angdermissing.Status
                    },

                    new AAddCard()
                    {
                        card = new CardExposedport
                        {
                            upgrade = Upgrade.B
                        },
                        destination = CardDestination.Hand,
                        amount = GetAngdermissingAmt(s),
                        xHint = 1
                    },
                };
                break;
        }
        return actions;
    }
    private int GetAngdermissingAmt(State s)
    {
        int result = 0;
        if (s.route is Combat)
        {
            result = s.ship.Get(ModEntry.Instance.Angdermissing.Status);
        }

        return result;
    }
}
