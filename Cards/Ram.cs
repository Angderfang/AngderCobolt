using Angder.Angdermod;
using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardRam : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Ram", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Ram", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Ramcard.Sprite,
            cost = upgrade == Upgrade.A ? 2 : 3,
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        
        List<CardAction> actions = new();
        int truehull;
        switch (upgrade)
        {
            case Upgrade.None:

                truehull = (c.otherShip.hull + c.otherShip.Get(Status.shield) + c.otherShip.Get(Status.tempShield)) - s.ship.hull;
                if (truehull > c.otherShip.hull)
                    truehull = c.otherShip.hull;
                actions = new()
                {
                    /*
                    new AHurt()
                    {
                        targetPlayer = false,
                        hurtShieldsFirst = true,
                        hurtAmount = s.ship.hull,
                        omitFromTooltips = true,
                        disabled = true
                    },

                    new AHurt()
                    {
                        targetPlayer = true,
                        hurtShieldsFirst = true,
                        hurtAmount = truehull ,
                        omitFromTooltips = true
                    },
                    */
                    //Am I going to get chewed out for doing this like this?
                    new ARam()
                    {
                        Piercing = false,
                        Truehull = truehull,
                        ModifiedDmg = GetDmg(s, 0)
                    }

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:

                truehull = (c.otherShip.hull + c.otherShip.Get(Status.shield) + c.otherShip.Get(Status.tempShield)) - s.ship.hull;
                if (truehull > c.otherShip.hull)
                    truehull = c.otherShip.hull;
                actions = new()
                {
                    new ARam()
                    {
                        Piercing = false,
                        Truehull = truehull,
                        ModifiedDmg = GetDmg(s, 0)
                    }

                };
                break;
            case Upgrade.B:

                truehull = (c.otherShip.hull + c.otherShip.Get(Status.shield) + c.otherShip.Get(Status.tempShield)) - s.ship.hull;
                if (truehull > c.otherShip.hull)
                    truehull = c.otherShip.hull;
                actions = new()
                {

                    new ARam()
                    {
                        Piercing = true,
                        Truehull = truehull,
                        ModifiedDmg = GetDmg(s, 0)
                    }
                };
                break;
        }
        return actions;
    }
}
//Not going to lie, this card is mostly a meme.