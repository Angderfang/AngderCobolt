using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards.Angdercards;

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

        CardData data = new CardData()
        {
            description = ModEntry.Instance.Localizations.Localize(["card", "FasterCannons", "description", upgrade.ToString()]),
            art = ModEntry.Instance.Angder_ManyBulletMuchwow.Sprite,
            cost = 2,
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {

        List<CardAction> actions = new(); ;
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new AAddCard()
                    {
                        card = new CardAutoblastleft(),
                        destination = CardDestination.Deck,
                        amount = 2,
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
                        amount = 1,
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
                                            //upgrade = Upgrade.A
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