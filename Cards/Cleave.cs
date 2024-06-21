using Angder.Angdermod;
using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardCleave : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Cleave", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Cleave", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        bool tablecheck = false;
        if (state.ship.Get(Status.tableFlip) > 0)
            tablecheck = true;

        CardData data = new CardData()
        {
            cost = 1,
            flippable = upgrade == Upgrade.B ? true : tablecheck,
            art = ModEntry.Instance.Angder_CleaveArt.Sprite,

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

        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new CleaveAction()
                    {
                        Length = 2,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true,
                        DamageAlt = GetDmg(s, 1), // If your going to complain about me having two different damage things. Blame Max. Right click forced me to grab "Getdmg" BEFORE I actually reached the bit of code which uses it.
                        Damage = 1,
                    }

                    //I want to remove reliable soooo badly. But it would make this a flat upgrade to Multishot too much of the time.

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.B:
                actions = new()
                {
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s, 1),
                        Ignoresoverdrive = false,
                        Thiscard = this,
                        Damage = 1,
                        Length = 2,
                        Direction = right
                        
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new CleaveAction()
                    {
                        DamageAlt = GetDmg(s, 1),
                        Damage = 1, //Angderjustcleavethings.AngderCleaveDmg(s, 1, this, true),
                        Length = 4,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true
                    }
                };
                break;
        }
        return actions;
    }



}