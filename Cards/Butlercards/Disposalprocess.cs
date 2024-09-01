using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.EchoesOfTheFuture.Cards.Butlercards;

internal sealed class CardDisposal : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Disposal", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Disposal", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Maid_Chute.Sprite,
            cost = upgrade == Upgrade.B ? 3 : 2,
            exhaust = true, //upgrade == Upgrade.B ? false : true,
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
                        status = ModEntry.Instance.Disposalprocess.Status,
                        statusAmount = 1,
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
                        status = ModEntry.Instance.Disposalprocess.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AAddCard
                    {
                        card = new TrashFumes(),
                        amount = 2,
                        destination = CardDestination.Hand
                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = ModEntry.Instance.Disposalprocess.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },

                };
                break;
        }
        return actions;
    }
}
