using Angder.Angdermod;
using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Angder.Angdermod.Cards;

internal sealed class CardAmmoDump : Card, IAngderCard
{

    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("AmmoDump", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "AmmoDump", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        bool tablecheck = false;
        if (state.ship.Get(Status.tableFlip) > 0)
            tablecheck = true;
        
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Gatling.Sprite,
            cost = 2,
            flippable = tablecheck,
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        int right = 1;
        int left = -1;

        if (flipped == true)
        {
            right = -1;
            left = 1;
        }

        //Actually a very powerful card, 6 damage base. 9 on B.upgrade.

        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s,1),
                        Damage = 1,
                        Length = 2,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true

                    },
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s,1),
                        Damage = 1,
                        Length = 2,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true
                    },
                    new AStatus()
                    {
                        status = Status.energyLessNextTurn,
                        statusAmount = 1,
                        targetPlayer = true
                    }

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s,1),
                        Damage = 1,
                        Length = 2,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true

                    },
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s,1),
                        Damage = 1,
                        Length = 2,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true

                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s,1),
                        Damage = 1,
                        Length = 2,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true

                    },
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s,1),
                        Damage = 1,
                        Length = 2,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true
                    },
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s,1),
                        Damage = 1,
                        Length = 2,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true
                    },
                    new AHurt()
                    {
                        hurtAmount = 2,
                        targetPlayer = true
                    }
                };
                break;
        }
        return actions;
    }
}