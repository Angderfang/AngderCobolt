using Angder.Angdermod;
using Nickel;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
namespace Angder.Angdermod.Cards;

internal sealed class CardEntrypod : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Entrypod", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Entrypod", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Entrypod.Sprite,
            cost = 1,

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
                    new AAttack()
                    {
                        damage = GetDmg(s, 2)
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Angdermissing.Status,
                        targetPlayer = true,
                        statusAmount = 1
                    },
                    new AEnergy()
                    {
                        changeAmount = 1,
                    }
                };
                //From useless to starter. It's kinda a weird thing that as soon as I did, board ship started looking useless. The two cards clash synergy wise.

                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {

                    new AAttack()
                    {
                        damage = GetDmg(s, 2),
                        piercing = true
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Angdermissing.Status,
                        targetPlayer = true,
                        statusAmount = 1
                    },
                    new AEnergy()
                    {
                        changeAmount = 1,
                    }

                };
                break;
            case Upgrade.B:
                actions = new()
                {

                    new AAttack()
                    {
                        damage = GetDmg(s, 2),
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Angdermissing.Status,
                        targetPlayer = true,
                        statusAmount = 1
                    },

                    new AStatus()
                    {
                        status = Status.energyNextTurn,
                        targetPlayer = true,
                        statusAmount = 2
                    },
                };
                break;
        }
        return actions;
    }
}
