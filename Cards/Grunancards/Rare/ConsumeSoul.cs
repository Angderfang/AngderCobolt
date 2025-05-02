using Angder.EchoesOfTheFuture;
using Angder.EchoesOfTheFuture.Features;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class ConsumeSoul : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ConsumeSoul", new()
        {


            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.rare,
                
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ConsumeSoul", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            //description = ModEntry.Instance.Localizations.Localize(["card", "Shatter", "description", upgrade.ToString()]),
            //description = ColorlessLoc.GetDesc(state, upgrade == Upgrade.B ? 3 : 2, (Deck)ModEntry.Instance.AngderDeck.Deck),
            cost = 3,
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
                     hurtAmount = GetDmg(s,4),
                    targetPlayer = false,
                    },
                new AHeal
                    {
                    healAmount = 4,
                    targetPlayer = true,
                    canRunAfterKill = true,
                    },
                };
        break;
            case Upgrade.A:
                actions = new()
                {
                new AShatter
                    {
                     hurtAmount = GetDmg(s,4),
                    targetPlayer = false,
                    },
                new AHeal
                    {
                    healAmount = 6,
                    targetPlayer = true,
                    canRunAfterKill = true,
                    },
                };

                break;
            case Upgrade.B:
                actions = new()
                {
                new AShatter
                    {
                    hurtAmount = GetDmg(s,6),
                    targetPlayer = false,
                    },
                new AHeal
                    {
                    healAmount = 4,
                    targetPlayer = true,
                    canRunAfterKill = true,
                    },
                };
                break;
        }
        return actions;
    }
}
