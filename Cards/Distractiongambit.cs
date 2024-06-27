using Angder.Angdermod;
using Nickel;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardDistractiongambit : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Distractiongambit", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Distractiongambit", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        RemoteManager.SetRemoteUnending(this, state, true);
        CardData data = new CardData()
        {

            art = ModEntry.Instance.Angder_Deepbreath.Sprite,
            cost = upgrade == Upgrade.A ?  1 : 2,
            exhaust = true,
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
                        status = ModEntry.Instance.Rampage.Status
                    },

                    new AStatus()
                    {
                       statusAmount = GetDisruptAmt(s),
                       status = ModEntry.Instance.Disrupt.Status,
                       xHint = 1,
                       targetPlayer = true,
                    },
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

                    new AStatus()
                    {
                       statusAmount = GetDisruptAmt(s),
                       status = ModEntry.Instance.Disrupt.Status,
                       xHint = 1,
                       targetPlayer = true,
                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AVariableHint
                    {
                        status = ModEntry.Instance.Rampage.Status,
                    },

                    new AStatus()
                    {
                       statusAmount = GetDisruptAmt(s) * 2,
                       status = ModEntry.Instance.Disrupt.Status,
                       xHint = 2,
                       targetPlayer = true,
                    },
                    new AStatus()
                    {
                       statusAmount = 0,
                       status = ModEntry.Instance.Rampage.Status,
                       mode = AStatusMode.Set,
                       targetPlayer = true,
                    },

                };
                break;
        }
        return actions;
    }
    private int GetDisruptAmt(State s)
    {
        int result = 0;
        if (s.route is Combat)
        {
            result = s.ship.Get(ModEntry.Instance.Rampage.Status);
        }

        return result;
    }
}
