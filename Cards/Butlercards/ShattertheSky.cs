using Angder.EchoesOfTheFuture;
using Angder.EchoesOfTheFuture.Features;
using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardShatterTheSky : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ShatterTheSky", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ShatterTheSky", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        bool tablecheck = false;
        if (state.ship.Get(Status.tableFlip) > 0)
            tablecheck = true;

        CardData data = new CardData()
        {
            cost = upgrade == Upgrade.B ? 1 : upgrade == Upgrade.A ? 2 : 3,
            exhaust = true,
            flippable = tablecheck,
            art = ModEntry.Instance.Angder_Shatter.Sprite,

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
                        Ignoresoverdrive = true,
                        DamageAlt = GetDmg(s, 1), // If your going to complain about me having two different damage things. Blame Max. Right click forced me to grab "Getdmg" BEFORE I actually reached the bit of code which uses it.
                        Damage = 1,
                    },

                    new CleaveAction()
                    {
                        Length = 4,
                        Thiscard = this,
                        Direction = left,
                        Ignoresoverdrive = true,
                        DamageAlt = GetDmg(s, 1), // If your going to complain about me having two different damage things. Blame Max. Right click forced me to grab "Getdmg" BEFORE I actually reached the bit of code which uses it.
                        Damage = 1,
                    },

                    new AAddCard
                    {
                        card = new OxygenLeak(),
                        amount = 1,
                        destination = CardDestination.Deck
                    },

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new CleaveAction()
                    {
                        Length = 4,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true,
                        DamageAlt = GetDmg(s, 1), // If your going to complain about me having two different damage things. Blame Max. Right click forced me to grab "Getdmg" BEFORE I actually reached the bit of code which uses it.
                        Damage = 1,
                    },
                    new CleaveAction()
                    {
                        Length = 4,
                        Thiscard = this,
                        Direction = left,
                        Ignoresoverdrive = true,
                        DamageAlt = GetDmg(s, 1), // If your going to complain about me having two different damage things. Blame Max. Right click forced me to grab "Getdmg" BEFORE I actually reached the bit of code which uses it.
                        Damage = 1,
                    },

                    new AAddCard
                    {
                        card = new OxygenLeak(),
                        amount = 1,
                        destination = CardDestination.Deck
                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new CleaveAction()
                    {
                        Length = 4,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true,
                        DamageAlt = GetDmg(s, 1), // If your going to complain about me having two different damage things. Blame Max. Right click forced me to grab "Getdmg" BEFORE I actually reached the bit of code which uses it.
                        Damage = 1,
                    },
                    new CleaveAction()
                    {
                        Length = 4,
                        Thiscard = this,
                        Direction = left,
                        Ignoresoverdrive = true,
                        DamageAlt = GetDmg(s, 1), // If your going to complain about me having two different damage things. Blame Max. Right click forced me to grab "Getdmg" BEFORE I actually reached the bit of code which uses it.
                        Damage = 1,
                    },

                    new AAddCard
                    {
                        card = new OxygenLeak(),
                        amount = 2,
                        destination = CardDestination.Deck
                    },
                };
                break;
        }
        return actions;
    }



}