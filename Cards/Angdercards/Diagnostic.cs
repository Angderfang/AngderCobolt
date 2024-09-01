using Angder.EchoesOfTheFuture;
using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardDiagnostic : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Diagnostic", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Diagnostic", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        
        CardData data = new CardData()
        {
            description = ModEntry.Instance.Localizations.Localize(["card", "Diagnostic", "description", upgrade.ToString()]),
            art = ModEntry.Instance.Angder_Shield.Sprite,
            cost = 2,
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
                        card = new CardDiagnosticComplete(),
                        destination = CardDestination.Deck,
                        amount = 2,
                    },
                    //Diagnostic? Not fitting for Angder, will ned to rename this one.
                };
                /* Remember to always break it up! */
                break;
            case Upgrade.B:
                actions = new()
           
                {
                    new AAddCard()
                    {
                        card = new CardDiagnosticComplete(){
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
                        card = new CardDiagnosticComplete()
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