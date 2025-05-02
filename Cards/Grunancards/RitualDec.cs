using Angder.EchoesOfTheFuture;
using Angder.EchoesOfTheFuture.Features;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class RitualDec : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("RitualDec", new()
        {
            

            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.uncommon,
               
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "RitualDec", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Candle.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "RitualDec", "description", upgrade.ToString()], new { damage = GetDmg(state, 2) }),
            cost = upgrade == Upgrade.B ? 1 : 2,
            exhaust = true,
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
                    new AAddCard
                    {
                        card = new Shatter(),
                        amount = 3,
                        destination = CardDestination.Discard,
                    },
                };

        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                    new AAddCard
                    {
                        card = new Shatter(),
                        amount = 3,
                        destination = CardDestination.Discard,
                    },
                    new AShatter
                    {
                        hurtAmount = GetDmg(s, 2),

                    },
                };

                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAddCard
                    {
                        card = new Shatter(),
                        amount = 4,
                        destination = CardDestination.Hand,
                    },
                    new AHurt
                    {
                        hurtAmount = 1,
                        targetPlayer = true
                    },
                };
        break;
        }
        return actions;
    }
}
