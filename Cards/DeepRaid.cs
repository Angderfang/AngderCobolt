using Angder.Angdermod;
using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardDeepraid : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("DeepRaid", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "DeepRaid", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Minimap.Sprite,
            cost = upgrade == Upgrade.A ? 2 : 3,
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
                        status = ModEntry.Instance.Angdermissing.Status,
                        targetPlayer = true,
                        statusAmount = 2
                    },
                    new AStatus()
                    {
                        status = Status.drawNextTurn,
                        targetPlayer = true,
                        statusAmount = 3
                    },

                    new AStatus()
                    {
                        status = Status.energyNextTurn,
                        targetPlayer = true,
                        statusAmount = 4
                    },
                    //Deep raid has gone through a lot of buffs and nerfs. I have a lot to say. But first a word from my sponsor. Raid shadow leg~
                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:

                actions = new()
                {
                    new AStatus()
                    {
                        status = ModEntry.Instance.Angdermissing.Status,
                        targetPlayer = true,
                        statusAmount = 2
                    },
                    new AStatus()
                    {
                        status = Status.drawNextTurn,
                        targetPlayer = true,
                        statusAmount = 3
                    },

                    new AStatus()
                    {
                        status = Status.energyNextTurn,
                        targetPlayer = true,
                        statusAmount = 5
                    },


                };
                break;
            case Upgrade.B:

                actions = new()
                {
                    new AStatus()
                    {
                        status = ModEntry.Instance.Angdermissing.Status,
                        targetPlayer = true,
                        statusAmount = 2
                    },
                    new AStatus()
                    {
                        status = Status.drawNextTurn,
                        targetPlayer = true,
                        statusAmount = 3
                    },

                    new AStatus()
                    {
                        status = Status.energyNextTurn,
                        targetPlayer = true,
                        statusAmount = 3
                    },
                    new AStatus()
                    {
                        status = ModEntry.Instance.Theft.Status,
                        targetPlayer = true,
                        statusAmount = 2
                    },
                };
                break;
        }
        return actions;
    }
}
