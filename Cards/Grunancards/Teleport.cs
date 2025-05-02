using Angder.EchoesOfTheFuture;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class Teleport : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Teleport", new()
        {


            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.uncommon,
               
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Teleport", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            //description = ModEntry.Instance.Localizations.Localize(["card", "Teleport", "description", upgrade.ToString()]),
            //description = ColorlessLoc.GetDesc(state, upgrade == Upgrade.B ? 3 : 2, (Deck)ModEntry.Instance.AngderDeck.Deck),
            cost = 1,
            exhaust = upgrade == Upgrade.A ? true : false,
            singleUse = upgrade == Upgrade.A ? false : true,
            retain = upgrade == Upgrade.B ? true : false,
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
                    new AMove()
                    {
                       dir = 5,
                       isRandom = true,
                       isTeleport = true,
                       targetPlayer = true,
                    },
                };

        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                    new AMove()
                    {
                       dir = 5,
                       isTeleport = true,
                       isRandom = true,
                       targetPlayer = true,
                    },
                };

                break;
            case Upgrade.B:
                actions = new()
                {
                    new AMove()
                    {
                       dir = 5,
                       isTeleport = true,
                       isRandom = true,
                       targetPlayer = true,
                    },
                };
        break;
        }
        return actions;
    }
}
