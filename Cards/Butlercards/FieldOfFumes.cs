using Angder.EchoesOfTheFuture.Features;
using Nickel;
//using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards;


internal sealed class CardFieldOfFumes : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("FieldOfFumes", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "FieldOfFumes", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            description = ModEntry.Instance.Localizations.Localize(["card", "FieldOfFumes", "description", upgrade.ToString()]),
            art = ModEntry.Instance.Maid_Field.Sprite,
            cost =  upgrade == Upgrade.A ? 0 : 1,
            //exhaust = true
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        int Exhaustcount = c.exhausted.Count;
        List<CardAction> actions = new();
        switch (upgrade)
        {
            

            case Upgrade.None:
                actions = new()
                {
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.shield,
                        statusAmount = 4
                    },
                    new AAddCard
                    {
                        card = new TrashUnplayable(),
                        amount = 2,
                        destination = CardDestination.Hand,
                    },
                };
        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.shield,
                        statusAmount = 4
                    },
                    new AAddCard
                    {
                        card = new TrashUnplayable(),
                        amount = 2,
                        destination = CardDestination.Hand,
                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.shield,
                        statusAmount = 6
                    },
                    new AAddCard
                    {
                        card = new TrashUnplayable(),
                        amount = 3,
                        destination = CardDestination.Discard,
                    },
                };
                break;
        }
        return actions;
    }
}
