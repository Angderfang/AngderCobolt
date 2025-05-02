using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Angder.EchoesOfTheFuture.Cards.Grunancards.Common;


internal sealed class CardMagicShield : Card, IAngderCard
{

    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("MagicShield", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "MagicShield", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {

        CardData data = new CardData()
        {
            //art = ModEntry.Instance.Angder_Gatling.Sprite,
            cost = 2,
            retain = true,
            buoyant = true,
            singleUse = true
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {

        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 4,
                        targetPlayer = true
                    },
                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 4,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 3,
                        targetPlayer = true
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 4,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                };
                break;
        }
        return actions;
    }
}