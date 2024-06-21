using Angder.Angdermod;
using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardFasterCannons : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("FasterCannons", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "FasterCannons", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        bool tablecheck = false;
        if (state.ship.Get(Status.tableFlip) > 0)
            tablecheck = true;
        
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_ManyBulletMuchwow.Sprite,
            cost = 2,
            flippable = tablecheck,
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {

        List<CardAction> actions = new();;
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new AAddCard()
                    {
                        card = new CardAutoblastleft(),
                        destination = CardDestination.Deck,
                        amount = 3,
                    },
                    //Base card is risky, A is safe, B is hyper-aggressive.
                };
                /* Remember to always break it up! */
                break;
            case Upgrade.B:
                actions = new()
           
                {
                    new AAddCard()
                    {
                        card = new CardAutoblastleft(){
                    upgrade = Upgrade.B
                    },
                        destination = CardDestination.Deck,
                        amount = 2,
                    },
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAddCard()
                    {
                        card = new CardAutoblastleft()
                        {
                                            upgrade = Upgrade.A
                        },
                        destination = CardDestination.Deck,
                        amount = 3,
                    },
                };
                break;
        }
        return actions;
    }



}