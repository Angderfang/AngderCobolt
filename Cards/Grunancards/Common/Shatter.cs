using Angder.EchoesOfTheFuture;
using Angder.EchoesOfTheFuture.Features;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class Shatter : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Shatter", new()
        {


            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.common,
                
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Shatter", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Decay.Sprite,
            //description = ModEntry.Instance.Localizations.Localize(["card", "Shatter", "description", upgrade.ToString()]),
            //description = ColorlessLoc.GetDesc(state, upgrade == Upgrade.B ? 3 : 2, (Deck)ModEntry.Instance.AngderDeck.Deck),
            cost = 0,
            singleUse = true,
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
                new AShatter
                    {
                    hurtAmount = GetDmg(s,1),
                    targetPlayer = false,
                    },

                    new ADrawCard()
                    {
                       count = 1,
                    },
                };
        break;
            case Upgrade.A:
                actions = new()
                {
                new AShatter
                    {
                    hurtAmount = GetDmg(s,2),
                    targetPlayer = false,
                    },

                    new ADrawCard()
                    {
                       count = 1,
                    },
                };

                break;
            case Upgrade.B:
                actions = new()
                {
                new AShatter
                    {
                    hurtAmount = GetDmg(s,4),
                    targetPlayer = false,
                    },
                };
        break;
        }
        return actions;
    }
}
