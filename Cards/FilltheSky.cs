using Angder.Angdermod;
using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardFillTheSky : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("FillTheSky", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "FillTheSky", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        bool tablecheck = false;
        if (state.ship.Get(Status.tableFlip) > 0)
            tablecheck = true;
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Shatter.Sprite,
            cost = upgrade == Upgrade.B ? 1 : 3,
            exhaust = true,
            flippable = tablecheck
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        int damageamount = 1;
        if (s.ship.Get(Status.overdrive) > 0)
        {
            damageamount = damageamount - s.ship.Get(Status.overdrive);
        }
        int right = 1;
        int left = -1;

        if (flipped == true)
        {
            right = -1;
            left = 1;
        }
        
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {

                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s,1),
                        Damage = 1, //Angderjustcleavethings.AngderCleaveDmg(s, 1, this, true),
                        Length = 4,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true
                    },
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s,1),
                        Damage = 1,//Angderjustcleavethings.AngderCleaveDmg(s, 1, this, true),
                        Length = 4,
                        Thiscard = this,
                        Direction = left,
                        Ignoresoverdrive = true
                    }
                    /* Empty the sky (Refered to as fill the sky, cos consistancy is for chumps) is the ultimate cleave card. */
                    //Empty the sky? It's actually called shatter the sky now.
                    /* Seriously, make up your mind, what is this card called? this is worse than unload!*/
                    //I think you mean "gatling laser"
                    /* Oh my fucking god */

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s,1),
                        Damage = 1, //Angderjustcleavethings.AngderCleaveDmg(s, 1, this, true),
                        Length = 4,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true
                    },
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s,1),
                        Damage = 1,//Angderjustcleavethings.AngderCleaveDmg(s, 1, this, true),
                        Length = 4,
                        Thiscard = this,
                        Direction = left,
                        Ignoresoverdrive = true
                    },

                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 3,
                        targetPlayer = true
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {

                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s,1),
                        Damage = 1, //Angderjustcleavethings.AngderCleaveDmg(s, 1, this, true),
                        Length = 4,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true
                    },
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s,1),
                        Damage = 1, //Angderjustcleavethings.AngderCleaveDmg(s, 1, this, true),
                        Length = 4,
                        Thiscard = this,
                        Direction = left,
                        Ignoresoverdrive = true
                    },
                    new AStatus()
                    {
                        status = Status.energyLessNextTurn,
                        statusAmount = 2,
                        targetPlayer = true
                    },

                };
                break;
        }
        return actions;
    }
}