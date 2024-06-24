using Angder.Angdermod;
using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardPunch : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Punch", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Punch", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        
        CardData data = new CardData()
        {
            cost = 2,
            art = ModEntry.Instance.Angder_Punch.Sprite,

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
                        damage = GetDmg(s, 3)
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Rampage.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    //Punch it! Wait no you broke the button!

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 3)
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Rampage.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 5)
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Rampage.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                };
                break;
        }
        return actions;
    }
}