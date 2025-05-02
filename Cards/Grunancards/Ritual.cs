using Angder.EchoesOfTheFuture;
using Angder.EchoesOfTheFuture.Cards.Grunancards.Common;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class Ritual : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Ritual", new()
        {
            

            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.uncommon,
               
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Ritual", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Candle.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "Ritual", "description", upgrade.ToString()]),
            cost = upgrade == Upgrade.B ? 1 : 2,
            //infinite = upgrade == Upgrade.A ? true : false,
            exhaust =  true,
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
                        card = new Sigil(),
                        amount = 2,
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
                        card = new Sigil(),
                        amount = 3,
                        destination = CardDestination.Discard,
                    },
                };

                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAddCard
                    {
                        card = new CardMagicShield(),
                        amount = 1,
                        destination = CardDestination.Hand,
                    },
                };
        break;
        }
        return actions;
    }
}
