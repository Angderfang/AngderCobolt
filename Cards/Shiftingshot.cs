using Angder.Angdermod;
using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardShiftingShot : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ShiftingShot", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ShiftingShot", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        bool tablecheck = false;
        if (state.ship.Get(Status.tableFlip) > 0)
            tablecheck = true;
        
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_ShiftShot.Sprite,
            cost = upgrade == Upgrade.B ? 3 : 2,
            flippable = upgrade == Upgrade.A ? true : tablecheck ? true : false,

        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        int right = 1;

        if (flipped == true)
        {
            right = -1;
        }



        List<CardAction> actions = new();;
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {

                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s, 1),
                        Ignoresoverdrive = false,
                        Damage = 1, //Angderjustcleavethings.AngderCleaveDmg(s, 1, this, false),
                        Length = 2,
                        Thiscard = this,
                        Direction = right
                    },
                    new AMove()
                    {
                        dir = -2,
                        targetPlayer = true
                        
                    }

                    //This shit is like lunge on crack.

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s, 1),
                        Ignoresoverdrive = false,
                        Damage = 1,//Angderjustcleavethings.AngderCleaveDmg(s, 1, this, false),
                        Length = 2,
                        Thiscard = this,
                        Direction = right
                    },
                    new AMove()
                    {
                        dir = -2,
                        targetPlayer = true

                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {

                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s, 2),
                        Ignoresoverdrive = false,
                        Damage = 2, //Angderjustcleavethings.AngderCleaveDmg(s, 2, this, false),
                        Length = 2,
                        Thiscard = this,
                        Direction = right
                    },
                    //Behold, the only cleave that does greater than 1 damage.
                    new AMove()
                    {
                        dir = -3,
                        targetPlayer = true

                    }
                };
                break;
        }
        return actions;
    }
}