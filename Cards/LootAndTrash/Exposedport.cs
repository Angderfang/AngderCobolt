using Nickel;
using System.Collections.Generic;
using System;
using System.Reflection;
using Angder.Angdermod.Features;

namespace Angder.Angdermod.Cards;

internal sealed class CardExposedport : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Exposedport", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderstrashDeck.Deck,
                upgradesTo = [Upgrade.A, Upgrade.B],
                rarity = Rarity.common,
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Exposedport", "name"]).Localize
        }); ;
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
           
            //description = ModEntry.Instance.Localizations.Localize(["card", "Exposedport", "description", upgrade.ToString(), flipped.ToString()]), // BUT FLIPPABALE!
            retain = upgrade == Upgrade.A ? true : false,
            art = ModEntry.Instance.Angder_RemoteUplink.Sprite,
            cost = 0,
            exhaust = true,
            temporary = true,
            flippable = true,
            
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
                    new AMoveEnemy()
                    {
                        targetPlayer = false,
                        dir = 2,
                    },

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {

                    new AMoveEnemy()
                    {
                        targetPlayer = false,
                        dir = 2
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AMoveEnemy()
                    {
                        targetPlayer = false,
                        dir = 3
                    },
                };
                break;
        }
        return actions;
    }
}
