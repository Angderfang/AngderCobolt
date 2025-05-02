using Angder.EchoesOfTheFuture.Features;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardMemory : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("CardMemory", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "CardMemory", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            //art = ModEntry.Instance.Maid_Chute.Sprite,
            cost = upgrade == Upgrade.B ? 2 : 1,
            exhaust = upgrade == Upgrade.B ? false : true,
            singleUse = upgrade == Upgrade.B ? true : false,
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
                    new ADummyAction()
                    {

                    },
                    new AStatus()
                    {
                        status = ModEntry.Instance.Memory.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new Refreshnotes()
                    {

                    },

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new ADummyAction()
                    {

                    },
                    new AStatus()
                    {
                        status = ModEntry.Instance.Memory.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                    new Refreshnotes()
                    {

                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new ADummyAction()
                    {

                    },
                    new AStatus()
                    {
                        status = ModEntry.Instance.Memory.Status,
                        statusAmount = 99,
                        targetPlayer = true
                    },
                    new Refreshnotes()
                    {

                    },

                };
                break;
        }
        return actions;
    }
}
