using Angder.EchoesOfTheFuture;
using Angder.EchoesOfTheFuture.Features;
using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardAlightdusting : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Alightdusting", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Alightdusting", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        bool tablecheck = false;
        if (state.ship.Get(Status.tableFlip) > 0)
            tablecheck = true;

        CardData data = new CardData()
        {
            //exhaust = upgrade == Upgrade.A ? false : true,
            cost = 0,
            flippable = upgrade == Upgrade.A ? true : tablecheck,
            art = ModEntry.Instance.Maid_Dusting.Sprite,

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

        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new CleaveAction()
                    {
                        Length = 4,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = false,
                        DamageAlt = GetDmg(s, 0), // If your going to complain about me having two different damage things. Blame Max. Right click forced me to grab "Getdmg" BEFORE I actually reached the bit of code which uses it.
                        Damage = 0,
                    },

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.B:
                actions = new()
                {
                    new CleaveAction()
                    {
                        Length = 2,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = false,
                        DamageAlt = GetDmg(s, 0),
                        Damage = 0,
                    },
                    new CleaveAction()
                    {
                        Length = 2,
                        Thiscard = this,
                        Direction = left,
                        Ignoresoverdrive = false,
                        DamageAlt = GetDmg(s, 0),
                        Damage = 0,
                    }

                };
                break;
            case Upgrade.A:
                actions = new()
                {

                    new CleaveAction()
                    {
                        Length = 4,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = false,
                        DamageAlt = GetDmg(s, 0),
                        Damage = 0,
                    }

                };
                break;
        }
        return actions;
    }



}