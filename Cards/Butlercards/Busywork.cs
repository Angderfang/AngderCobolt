using Angder.EchoesOfTheFuture.Features;
using Nickel;
//using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards;


internal sealed class CardBusywork : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Busywork", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
                
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Busywork", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            description = ModEntry.Instance.Localizations.Localize(["card", "Busywork", "description", upgrade.ToString()]),
            //art = ModEntry.Instance.Angder_Enraged.Sprite,
            cost = 1,
            infinite = true,
            retain = upgrade == Upgrade.A ? true : false,
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
                    new AAddCard
                    {
                        card = new TrashFumes(),
                        amount = 1,
                        destination = CardDestination.Hand
                    },
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.shield,
                        statusAmount = 1
                    },
        };
        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                    new AAddCard
                    {
                        card = new TrashFumes(),
                        amount = 1,
                        destination = CardDestination.Hand
                    },
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.shield,
                        statusAmount = 1
                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAddCard
                    {
                        card = new TrashFumes(),
                        amount = 2,
                        destination = CardDestination.Deck
                    },
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.shield,
                        statusAmount = 2
                    },
                };
                break;
        }
        return actions;
    }
}
